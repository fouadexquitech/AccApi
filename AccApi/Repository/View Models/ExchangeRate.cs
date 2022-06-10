
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AccApi.Repository.View_Models
{
    public class ExchangeRate
    {

        //public Dictionary<string, object> DataItems { get; set; }
        [JsonExtensionData]
        public double result { get; set; }
    }



}
