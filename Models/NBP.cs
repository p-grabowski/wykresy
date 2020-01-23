using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace Wykresy.Models
{
    public class NBP
    {
        public NBPRates[] GetRates(string url)
        {
            string json = "";
            WebRequest request = WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response = request.GetResponse();

            string respResult = ((HttpWebResponse)response).StatusDescription;

            using (Stream dataStream = response.GetResponseStream())
            {

                StreamReader reader = new StreamReader(dataStream);

                json = reader.ReadToEnd();

            }
            response.Close();
            return NBPRates.GetRatesClassFromJson(json);
        }

        public class Rate
        {
            public string currency { get; set; }
            public string code { get; set; }
            public double bid { get; set; }
            public double ask { get; set; }
            public double mid { get; set; }
        }

        public class NBPRates
        {
            public string table { get; set; }
            public string no { get; set; }
            public string tradingDate { get; set; }
            public string effectiveDate { get; set; }
            public List<Rate> rates { get; set; }

            public static NBPRates[] GetRatesClassFromJson(string json)
            {
                NBPRates[] rates = JsonConvert.DeserializeObject<NBPRates[]>(json);
                return rates;
            }
        }

    }
}