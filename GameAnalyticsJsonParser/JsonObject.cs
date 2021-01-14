using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameAnalyticsJsonParser
{
    //OUR JSON OBJECT CLASSES
    public class JsonObject
    {
        public Data data { get; set; }
        public string country_code { get; set; }
        public string arrival_ts { get; set; }
        public string category { get; set; }
        public string game_id { get; set; }
        public string public_key { get; set; }
        public string multi_message { get; set; }
        public UserMeta user_meta { get; set; }

        public JsonObject()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            data = new Data();
            country_code = "";
            arrival_ts = "";
            category = "";
            game_id = "";
            public_key = "";
            multi_message = "";
            user_meta = new UserMeta();
        }

        public override string ToString()
        {
            return "{data: " + data.ToString() + " country_code: " + country_code + " arrival_ts: " + arrival_ts + " category: " + category + " game_id: " + game_id + " public_key: " + public_key + " multi_message: " + multi_message + " user_meta: " + user_meta.ToString() + "}";
        }
        public string getTag(int i)
        {
            switch (i)
            {
                case 1: return "build";
                case 2: return "value";
                case 3: return "severity";
                case 4: return "x";
                case 5: return "y";
                case 6: return "z";
                case 7: return "event_id";
                case 8: return "area";
                case 9: return "session_id";
                case 10: return "user_id";
                case 11: return "message";
                case 12: return "amount";
                case 13: return "currency";
                case 14: return "country_code";
                case 15: return "arrival_ts";
                case 16: return "category";
                case 17: return "game_id";
                case 18: return "public_key";
                case 19: return "multi_message";
                case 20: return "install_ts";
                case 21: return "platform";
                case 22: return "device";
                case 23: return "os_major";
                case 24: return "os_minor";
                case 25: return "sdk_version";
                case 26: return "revenue_currency";
                case 27: return "revenue_amount";
                default: return "Invalid Tag ID";
            }
        }
        public string getElement(int i)
        {
            switch (i)
            {
                case 1: return data.build;
                case 2: return data.value;
                case 3: return data.severity;
                case 4: return data.x;
                case 5: return data.y;
                case 6: return data.z;
                case 7: return data.event_id;
                case 8: return data.area;
                case 9: return data.session_id;
                case 10: return data.user_id;
                case 11: return data.message;
                case 12: return data.amount;
                case 13: return data.currency;
                case 14: return country_code;
                case 15: return arrival_ts;
                case 16: return category;
                case 17: return game_id;
                case 18: return public_key;
                case 19: return multi_message;
                case 20: return user_meta.install_ts;
                case 21: return user_meta.platform;
                case 22: return user_meta.device;
                case 23: return user_meta.os_major;
                case 24: return user_meta.os_minor;
                case 25: return user_meta.sdk_version;
                case 26: return user_meta.revenue.getRevenueCurrency();
                case 27: return user_meta.revenue.getRevenueAmount(user_meta.revenue.getRevenueCurrency());
                default: return "Invalid Element ID";
            }
        }

    }
    public class Data
    {
        public string build { get; set; }
        public string value { get; set; }
        public string severity { get; set; }
        public string x { get; set; }
        public string y { get; set; }
        public string z { get; set; }
        public string event_id { get; set; }
        public string area { get; set; }
        public string session_id { get; set; }
        public string user_id { get; set; }
        public string message { get; set; }
        public string amount { get; set; }
        public string currency { get; set; }

        public Data()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            build = "";
            value = "";
            severity = "";
            x = "";
            y = "";
            z = "";
            event_id = "";
            area = "";
            session_id = "";
            user_id = "";
            message = "";
            amount = "";
            currency = "";
        }

        public override string ToString()
        {
            return "{build:" + build + " value:" + value + " severity:" + severity + " x:" + x + " y:" + y + " z:" + z + " event_id:" + event_id + " area:" + area + " session_id:" + session_id + " user_id:" + user_id + " message:" + message + " amount:" + amount + " currency:" + currency + "}";
        }
    }
    public class UserMeta
    {
        public string install_ts { get; set; }
        public string platform { get; set; }
        public string device { get; set; }
        public string os_major { get; set; }
        public string os_minor { get; set; }
        public string sdk_version { get; set; }
        public Revenue revenue { get; set; }

        public UserMeta()
        {
            InitializeComponent();
            revenue = new Revenue();
        }

        private void InitializeComponent()
        {
            install_ts = "";
            platform = "";
            device = "";
            os_major = "";
            os_minor = "";
            sdk_version = "";
        }

        public override string ToString()
        {
            return "{install_ts:" + install_ts + " platform:" + platform + " device:" + device + " os_major:" + os_major + " os_minor:" + os_minor + " sdk_version:" + sdk_version + " revenue:" + revenue.ToString() + "}";
        }
    }
    public class Revenue
    {
        public string USD { get; set; }
        public string EUR { get; set; }
        public string AUD { get; set; }
        public string NZD { get; set; }
        public string DKK { get; set; }
        public string CNY { get; set; }
        public string GBP { get; set; }
        public string CAD { get; set; }
        public string CHF { get; set; }
        public string RUB { get; set; }
        public string MXN { get; set; }
        public string NOK { get; set; }
        public string SEK { get; set; }
        public string INR { get; set; }
        public string SAR { get; set; }
        public string JPY { get; set; }
        public string IDR { get; set; }
        public string ILS { get; set; }
        public string ZAR { get; set; }
        public string AED { get; set; }
        public string HKD { get; set; }
        public string SGD { get; set; }
        public string TRY { get; set; }
        public string TWD { get; set; }

        public Revenue()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            USD = "";
            EUR = "";
            AUD = "";
            NZD = "";
            DKK = "";
            CNY = "";
            GBP = "";
            CAD = "";
            CHF = "";
            RUB = "";
            MXN = "";
            NOK = "";
            SEK = "";
            INR = "";
            SAR = "";
            JPY = "";
            IDR = "";
            ILS = "";
            ZAR = "";
            AED = "";
            HKD = "";
            SGD = "";
            TRY = "";
            TWD = "";
        }        

        public string getRevenueCurrency()
        {
            if (USD != null) return "USD";
            else if (EUR != null) return "EUR";
            else if (AUD != null) return "AUD";
            else if (NZD != null) return "NZD";
            else if (DKK != null) return "DKK";
            else if (CNY != null) return "CNY";
            else if (GBP != null) return "GBP";
            else if (CAD != null) return "CAD";
            else if (CHF != null) return "CHF";
            else if (RUB != null) return "RUB";
            else if (MXN != null) return "MXN";
            else if (NOK != null) return "NOK";
            else if (SEK != null) return "SEK";
            else if (INR != null) return "INR";
            else if (SAR != null) return "SAR";
            else if (JPY != null) return "JPY";
            else if (IDR != null) return "IDR";
            else if (ILS != null) return "ILS";
            else if (ZAR != null) return "ZAR";
            else if (AED != null) return "AED";
            else if (HKD != null) return "HKD";
            else if (SGD != null) return "SGD";
            else if (TRY != null) return "TRY";
            else if (TWD != null) return "TWD";
            return "";
        }

        public string getRevenueAmount()
        {
            if (USD != null) return USD;
            else if (EUR != null) return EUR;
            else if (AUD != null) return AUD;
            else if (NZD != null) return NZD;
            else if (DKK != null) return DKK;
            else if (CNY != null) return CNY;
            else if (GBP != null) return GBP;
            else if (CAD != null) return CAD;
            else if (CHF != null) return CHF;
            else if (RUB != null) return RUB;
            else if (MXN != null) return MXN;
            else if (NOK != null) return NOK;
            else if (SEK != null) return SEK;
            else if (INR != null) return INR;
            else if (SAR != null) return SAR;
            else if (JPY != null) return JPY;
            else if (IDR != null) return IDR;
            else if (ILS != null) return ILS;
            else if (ZAR != null) return ZAR;
            else if (AED != null) return AED;
            else if (HKD != null) return HKD;
            else if (SGD != null) return SGD;
            else if (TRY != null) return TRY;
            else if (TWD != null) return TWD;
            return "";
        }

        public string getRevenueAmount(string cur)
        {
            switch (cur)
            {
                case "USD": return USD;
                case "EUR": return EUR;
                case "AUD": return AUD;
                case "NZD": return NZD;
                case "DKK": return DKK;
                case "CNY": return CNY;
                case "GBP": return GBP;
                case "CAD": return CAD;
                case "CHF": return CHF;
                case "RUB": return RUB;
                case "MXN": return MXN;
                case "NOK": return NOK;
                case "SEK": return SEK;
                case "INR": return INR;
                case "SAR": return SAR;
                case "JPY": return JPY;
                case "IDR": return IDR;
                case "ILS": return ILS;
                case "ZAR": return ZAR;
                case "AED": return AED;
                case "HKD": return HKD;
                case "SGD": return SGD;
                case "TRY": return TRY;
                case "TWD": return TWD;
                default: return "";
            }
        }
    }
}
