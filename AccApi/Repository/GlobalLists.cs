using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AccApi.Repository
{
    public class GlobalLists
    {
        private readonly IServiceProvider _provider;  
        private string _costDbconnectionString;
        private string _timeSheetDbconnectionString;
        public IConfiguration _configuration { get; }


        public GlobalLists(IServiceProvider provider, IConfiguration configuration)
        {
            _provider = provider;   
            _configuration = configuration;
        }


        public void SetAccDbConnectionString(string str)
        {
            _costDbconnectionString = str;                
        }

        public void SetTimeSheetDbConnectionString(string str)
        {
            _timeSheetDbconnectionString = str;
        }


        public string GetAccDbconnectionString() {
            return _costDbconnectionString;
        }
        public string GetTimeSheetDbconnectionString()
        {
            return _timeSheetDbconnectionString;
        }
    }
}
