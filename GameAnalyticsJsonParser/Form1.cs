using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO; //for Path, Directory, and FileStream
using System.IO.Compression; //for GZipStream and CompressionMode
using System.Web.Script.Serialization; //for JavaScriptSerializer, also need to add Reference to System.Web.Extensions
using System.Threading; //for ThreadStart and Thread

using Microsoft.Office.Interop;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices; //for Marshal
using System.Diagnostics; //for Process

namespace GameAnalyticsJsonParser
{
    public partial class frmMain : Form
    {
        FolderBrowserDialog myFolderBrowserDialog;
        StreamReader myStreamReader;
        StreamWriter myStreamWriter;
        public string strSourceFolder = "";
        public string strOutputFolder = "";
        public const int intNumElements = 27;
        public const int intHourOffset = 478;
        public const int intDailyNamePrefixLength = 15;
        ThreadStart myBuildJsonThreadStart;
        Thread myBuildJsonThread;

        //Using a thread so we can still have access to our main form
        public void CallToBuildJsonThread()
        {            
            bool boolCsv = rBtnCsv.Checked;
            string strExtension = ".csv";

            //these have to be created no matter what, I can not conditionally create them (or don't know how to)
            Excel.Application myExcelApp;
            Excel.Workbooks myWorkbooks;
            Excel.Workbook myWorkbook;
            Excel.Sheets mySheets;
            Excel.Worksheet myWorksheet;
            //Excel.Range myRange;
            myExcelApp = null;
            myWorkbooks = null;
            myWorkbook = null;
            mySheets = null;
            myWorksheet = null;

            if (!boolCsv)
            {
                strExtension = ".xlsx";
                myExcelApp = new Excel.Application();
                //myExcelApp.Visible = true;
                myExcelApp.EnableEvents = true;
                myExcelApp.DisplayAlerts = false;
                myWorkbooks = myExcelApp.Workbooks;                
            }
            bool boolNewDailyFile = false;
            string strDailyFileName = "", strHourlyFileName;
            int i, j, intHour, intJsonLineCount = 0, intNumPrevLines = 0;  //should counter be intNumDaysMade
            FileInfo myFileInfo;
            string[] strGzFiles = Directory.GetFiles(strSourceFolder, "*.gz");
            string[] strJsonFiles = new string[strGzFiles.Length];
            string strJsonLine, strCsvLine="";


            //PrintMessage("Unzipping all .gz files into .json files.");
            for (i = 0; i < strGzFiles.Length; i++)
            {
                myFileInfo = new FileInfo(strGzFiles[i]);
                using (FileStream originalFileStream = myFileInfo.OpenRead())
                {
                    //get rid of the .gz and get just the filename (no folder path) and prepend the directory we want to dump the .json files in
                    strJsonFiles[i] = strSourceFolder + "\\" + Path.GetFileName(strGzFiles[i].Remove(strGzFiles[i].Length - myFileInfo.Extension.Length));
                    
                    //strJsonFiles[i] = strGzFiles[i].Remove(strGzFiles[i].Length - myFileInfo.Extension.Length);                    
                    //strJsonFiles[i] = Path.GetFileName(strJsonFiles[i]);
                    //strJsonFiles[i] = strSourceFolder + "\\" + strJsonFiles[i];

                    //this part actually decompress the .gz file and creates the .json file
                    using (FileStream decompressedFileStream = File.Create(strJsonFiles[i]))                    
                        using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))                   
                            decompressionStream.CopyTo(decompressedFileStream);
                }

                //figure out daily name, and if we have a new one.
                strHourlyFileName = Path.GetFileNameWithoutExtension(strJsonFiles[i]);
                intHour = Convert.ToInt32(strHourlyFileName.Substring(strHourlyFileName.Length - 3)) - intHourOffset;                
                if (strDailyFileName != strHourlyFileName.Substring(0, intDailyNamePrefixLength))
                {
                    //close the FileStreamWriter (or excel workbook) only if we have at least one previous file written to, which happens when intJsonLineCount > 0
                    if (intJsonLineCount > 0 && !boolNewDailyFile)
                    {
                        PrintMessage("Finished building " + strDailyFileName + strExtension);
                        if (boolCsv)
                        {
                            myStreamWriter.Close();
                        }
                        else if (!boolCsv)
                        {
                            //since we have DisplayAlerts set to false, will auto overwrite
                            myWorkbook.SaveAs(strOutputFolder + "\\" + strDailyFileName + strExtension, Excel.XlFileFormat.xlOpenXMLWorkbook);
                            myWorkbook.Close();

                            //Below to force overwrite
                            //myWorkbook.SaveAs(strExcelFileName, Excel.XlFileFormat.xlOpenXMLWorkbook, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange, Excel.XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                            /*Workaround to force overwrite - delete's file if exists and then saves new
                            if (File.Exists(strExcelFileName))
                                File.Delete(strExcelFileName);
                            myWorkbook.Close(true, strExcelFileName);
                            */
                        }
                    }
                    boolNewDailyFile = true;              
                    strDailyFileName = strHourlyFileName.Substring(0, intDailyNamePrefixLength);
                    intNumPrevLines = 0;
                    PrintMessage(strDailyFileName + strExtension + " created");
                }
                
                //Daily file has been initialized - build it


                //create Daily File StreamWriter
                if (boolNewDailyFile)
                {
                    if (boolCsv)
                    {
                        myStreamWriter = new StreamWriter(strOutputFolder + "\\" + strDailyFileName + strExtension);
                    }
                    else
                    {
                        myWorkbook = myWorkbooks.Add();
                        mySheets = myWorkbook.Worksheets;
                        myWorksheet = (Excel.Worksheet)mySheets.get_Item("Sheet1");
                        myWorksheet.Name = strDailyFileName;
                    }
                }

                //we need to read the hourly Json file
                myStreamReader = new StreamReader(strJsonFiles[i]);

                //JavaScriptSerializer used to deserialize the Json string
                JavaScriptSerializer mySerializer = new JavaScriptSerializer();
                intJsonLineCount = 1; //we are on our first line of the Json file read

                JsonObject myJsonObject;

                while ((strJsonLine = myStreamReader.ReadLine()) != null)
                {
                    myJsonObject = mySerializer.Deserialize<JsonObject>(strJsonLine);

                    //if we are starting a new daily file AND we are on our first line of the Json file (we are on the first line of the first day)
                    //then create a header row
                    if (intJsonLineCount == 1 && boolNewDailyFile)
                    {
                        //we don't need to do this if extension is .xlsx, but irrelevant since we'd have to perform bool check.
                        strCsvLine = "";                        
                        for (j = 1; j <= intNumElements; j++)
                        {
                            if (boolCsv)
                            {
                                strCsvLine += myJsonObject.getTag(j);
                                //don't need this since we are manually appending hour
                                //if (j < intNumElements)
                                strCsvLine += ",";
                            }
                            else
                            {
                                myWorksheet.Cells[1, j] = myJsonObject.getTag(j);                                
                            }
                        }
                        if (boolCsv)
                        {
                            strCsvLine += "hour";
                            myStreamWriter.WriteLine(strCsvLine);
                        }
                        else
                        {                            
                            myWorksheet.Cells[1, intNumElements+1] = "hour";                            
                        }                        
                        boolNewDailyFile = false;
                    }
                    intNumPrevLines++;

                    try
                    {
                        //build the data row                        
                        //again we don't need to do this if extension is .xlsx, but irrelevant since we'd have to perform bool check.
                        strCsvLine = "";
                        for (j = 1; j <= intNumElements; j++)
                        {
                            if (boolCsv)
                            {
                                if (myJsonObject.getElement(j) != null)
                                {
                                    strCsvLine += myJsonObject.getElement(j).Replace(',', ' ');
                                }
                                //don't need this since we are manually appending hour
                                //if (j < intNumElements)                            
                                strCsvLine += ",";
                            }
                            else if (!boolCsv && myJsonObject.getElement(j) != null)
                            {
                                myWorksheet.Cells[intNumPrevLines + 1, j] = myJsonObject.getElement(j);
                            }
                        }
                        if (boolCsv)
                        {
                            strCsvLine += intHour.ToString();
                            myStreamWriter.WriteLine(strCsvLine);
                        }
                        else
                        {
                            myWorksheet.Cells[intNumPrevLines + 1, intNumElements + 1] = intHour;
                        }
                    }
                    catch (Exception ex)
                    {
                        PrintMessage("Exception in " + Path.GetFileName(strJsonFiles[i]) + " on line " + intJsonLineCount + ": " + ex.ToString());
                    }
                    
                    intNumPrevLines++;
                    intJsonLineCount++;
                }

                //we are done reading the curret Json file (current hour of current day)
                PrintMessage("Finished unzipping, parsing, and copying " + myFileInfo.Name);                
                myStreamReader.Close();

            } //for (i = 0; i < strGzFiles.Length; i++)

            //we have finished reading all the Json files, build completed
            PrintMessage("Finished building " + strDailyFileName);
            if (boolCsv)
            {
                myStreamWriter.Close();
            }
            else
            {
                myWorkbook.SaveAs(strOutputFolder + "\\" + strDailyFileName + strExtension, Excel.XlFileFormat.xlOpenXMLWorkbook);
                myWorkbook.Close();
                myWorkbooks.Close();
                myExcelApp.Quit();
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            //Marshal.ReleaseComObject(myRange);
            try
            {
                Marshal.ReleaseComObject(myWorksheet);
                Marshal.ReleaseComObject(mySheets);
                Marshal.ReleaseComObject(myWorkbook);
                Marshal.ReleaseComObject(myWorkbooks);
                Marshal.ReleaseComObject(myExcelApp);

                //Marshal.FinalReleaseComObject(myRange);
                Marshal.FinalReleaseComObject(myWorksheet);
                Marshal.FinalReleaseComObject(mySheets);
                Marshal.FinalReleaseComObject(myWorkbook);
                Marshal.FinalReleaseComObject(myWorkbooks);
                Marshal.FinalReleaseComObject(myExcelApp);
    
                //myRange = null;
                myWorksheet = null;
                mySheets = null;
                myWorkbook = null;
                myWorkbooks = null;
                myExcelApp = null;
            }
            catch (Exception ex)
            {                
                if (!boolCsv)
                    PrintMessage("Exception releasing excel variables " + ex.ToString());
            }

            //Besides thorough COM cleanup, sometimes excel process still gets stuck so implement this routine.
            //kill EXCEL com processes                    
            Process[] myProcesses = Process.GetProcessesByName("EXCEL");
            foreach (Process myProcess in myProcesses)
            {
                //User excel process always have window name, COM process do not.
                if (myProcess.MainWindowTitle.Length == 0)
                    myProcess.Kill();
            }
            
            PrintMessage("Build Completed.");            
            myBuildJsonThread.Abort();
            myBuildJsonThread.Join();
            
        } //end of CallToBuildJsonThread

        public frmMain()
        {
            InitializeComponent();
            lstStatus.Text = "";
            myFolderBrowserDialog = new FolderBrowserDialog();
            txtSourceFolder.Text = @"C:\Users\yiannis\Documents\JsonFiles\json gz";
            txtOutputFolder.Text = @"C:\Users\yiannis\Documents\JsonFiles\output";
        }

        private void btnSourceFolder_Click(object sender, EventArgs e)
        {
            if (myFolderBrowserDialog.ShowDialog() == DialogResult.OK)   //get a folder
            {
                strSourceFolder = myFolderBrowserDialog.SelectedPath;
                txtSourceFolder.Text = strSourceFolder;
            }
        }
        private void txtSourceFolder_TextChanged(object sender, EventArgs e)
        {
            strSourceFolder = txtSourceFolder.Text;
        }        

        private void btnOutputFolder_Click(object sender, EventArgs e)
        {
            if (myFolderBrowserDialog.ShowDialog() == DialogResult.OK)   //get a folder
            {
                strOutputFolder = myFolderBrowserDialog.SelectedPath;
                txtOutputFolder.Text = strOutputFolder;
            }
        }
        private void txtOutputFolder_TextChanged(object sender, EventArgs e)
        {
            strOutputFolder = txtOutputFolder.Text;
        }

        private void btnBuildOutput_Click(object sender, EventArgs e)
        {            
            strSourceFolder = txtSourceFolder.Text;
            strOutputFolder = txtOutputFolder.Text;

            myBuildJsonThreadStart = new ThreadStart(CallToBuildJsonThread);
            myBuildJsonThread = new Thread(myBuildJsonThreadStart);

            //Check if our folders are valid
            if (!Directory.Exists(strSourceFolder))
            {
                lstStatus.Items.Add("Invalid Source Folder specified - please select a valid Source Folder.  Build Aborted!");
                lstStatus.Update();
                return;
            }
            if (!Directory.Exists(strOutputFolder))
            {
                lstStatus.Items.Add("Invalid Output Folder specified - please select a valid Output Folder.  Build Aborted!");
                lstStatus.Update();
                return;
            }
            
            //We have checked for valid folders, call main build thread, so we can still have access to our form
            if (myBuildJsonThread.IsAlive)
            {
                PrintMessage("Currently building, please wait for previous build to finish before starting new.");
                return;
            }
            myBuildJsonThread.Start();
 
        } //end of btnBuildOutput_Click

        //Use invoke for Threading
        delegate void PrintMessageCallback(string strMsg);
        //will print strMsg in lstStatus prefixed with current time and call lstStatus.Update()
        private void PrintMessage(string strMsg)
        {
            if (lstStatus.InvokeRequired)
            {
                PrintMessageCallback myPrintMessageCallback = new PrintMessageCallback(PrintMessage);
                Invoke(myPrintMessageCallback, new object[] { strMsg });
            }
            else
            {
                lstStatus.Items.Add(DateTime.Now.ToString("h:mm:ss") + ":  " + strMsg);
                lstStatus.Update();
            }
        }

        //will print s in lstStatus prefixed with current time and called lstStatus.Update()
        /*public void PrintMessage(string strMsg)
        {            
            lstStatus.Items.Add(DateTime.Now.ToString("h:mm:ss") + ":  " + strMsg);
            lstStatus.Update();
        }*/
        
    }
}
