﻿
using System;
using System.Collections.Generic;
using System.Net;
using AccApi.Repository.Interfaces;
using AccApi.Repository.View_Models;
using Nancy.Json;
using Newtonsoft.Json;

namespace AccApi.Repository.Managers
{
    public class CurrencyConverterRepository: ICurrencyConverterRepository
    {
        private readonly String BASE_URI = "https://free.currconv.com";
        private readonly String API_VERSION = "v7";

        //public FreeCurrencyConverterService() { }

        public double GetCurrencyExchange(String localCurrency, String foreignCurrency)
        {
            var code = $"{foreignCurrency}_{localCurrency}";
            var newRate = FetchSerializedData(code);
            return newRate;
        }

        private double FetchSerializedData(String code)
        {
            var url = $"{BASE_URI}/api/{API_VERSION}/convert?q={code}&compact=ultra&apiKey=7726dd1cebe5aeb063da";
            var webClient = new WebClient();
            string jsonData ;

            double conversionRate = 0;
            try
            {
                jsonData = webClient.DownloadString(url);

                var jsonObject = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonData);

                //var jsonObject = new JavaScriptSerializer().Deserialize<Dictionary<string, Dictionary<string, decimal>>>(jsonData);
                //var jsonObject = new JavaScriptSerializer().Deserialize<ExchangeRate>(jsonData);
                //var result = jsonObject[code];
                string exch="0";
                foreach (var v in jsonObject)
                {
                     exch =v.Value;
                }

                conversionRate = Double.Parse(exch);
            }
            catch (Exception) { }

            return conversionRate;
        }

    }
}
