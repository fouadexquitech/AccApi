﻿

namespace AccApi.Repository.View_Models.Request
{
    public class SearchInput
    {
        public string[] BOQDiv { get; set; }
        public string[] RESDiv { get; set; }
        public string BOQItem { get; set; }
        public string[] RESType { get; set; }
        public string BOQDesc { get; set; }
        public string RESDesc { get; set; }
        public int Package { get; set; }
        public string RESPackage { get; set; }
        public string FromRow { get; set; }
        public string ToRow { get; set; }
        public string SheetDesc { get; set; }
        public string itemO { get; set; }
        public string[] boqLevel2 { get; set; }
        //public string boqLevel3 { get; set; }
        public string[] boqLevel3 { get; set; }
        public string[] boqLevel4 { get; set; }
        public string obTradeDesc { get; set; }
        public int isItemsAssigned { get; set; }
        public string[] boqResourceSeq { get; set; }
        public int isRessourcesAssigned { get; set; }
        public string boqStatus { get; set; }

    }
}
