
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AccApi.Repository.View_Models
{
    public class ExchangeRate
    {
        [JsonExtensionData]
        public Dictionary<string, object> DataItems { get; set; }
    }

}
