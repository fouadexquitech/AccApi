
using System;
using System.Collections.Generic;
using System.Net;
using AccApi.Repository.Interfaces;
using AccApi.Repository.View_Models;
using Nancy.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AccApi.Repository.Managers
{
    public class CurrencyConverterRepository: ICurrencyConverterRepository
    {
        public double GetCurrencyExchange(String localCurrency, String foreignCurrency)
        {
            //var url = $"https://api.apilayer.com/exchangerates_data/convert?to={localCurrency}& from={foreignCurrency}&amount=1&apikey=4zN5nYjguyVQhynDgczfYxYpActZD8zx";

            var url = $"https://v6.exchangerate-api.com/v6/cf4d499450146caf5d37ad3c/pair/{foreignCurrency}/{localCurrency}";
            var webClient = new WebClient();
            string jsonData;

            double conversionRate = 0;
            try
            {
                jsonData = webClient.DownloadString(url);

                //var jsonObject = JsonConvert.DeserializeObject<ExchangeRate>(jsonData);

                var jo = JObject.Parse(jsonData);
                var id = jo["conversion_rate"].ToString();

                //string exch = "0";             
                //foreach (var v in jsonObject)
                //{
                //    exch = v.Value;
                //}

                conversionRate = Double.Parse(id);
            }
            catch (Exception) { }

            return conversionRate;
        }


        ////https://free.currconv.com
        //private readonly String BASE_URI = "https://free.currconv.com";
        //private readonly String API_VERSION = "v7";

        //public double GetCurrencyExchange(String localCurrency, String foreignCurrency)
        //{
        //    var code = $"{foreignCurrency}_{localCurrency}";
        //    var newRate = FetchSerializedData(code);
        //    return newRate;
        //}

        //private double FetchSerializedData(String code)
        //{
        //    var url = $"{BASE_URI}/api/{API_VERSION}/convert?q={code}&compact=ultra&apiKey=7726dd1cebe5aeb063da";
        //    var webClient = new WebClient();
        //    string jsonData ;

        //    double conversionRate = 0;
        //    try
        //    {
        //        jsonData = webClient.DownloadString(url);

        //        var jsonObject = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonData);

        //        string exch="0";
        //        foreach (var v in jsonObject)
        //        {
        //             exch =v.Value;
        //        }

        //        conversionRate = Double.Parse(exch);
        //    }
        //    catch (Exception) { }

        //    return conversionRate;
        //}

    }
}
