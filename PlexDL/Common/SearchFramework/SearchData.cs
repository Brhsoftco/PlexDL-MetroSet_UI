﻿using PlexDL.Common.Enums;
using System.Data;

namespace PlexDL.Common.SearchFramework
{
    public class SearchData
    {
        public SearchData()
        {
            // for no arguments
        }

        public SearchData(string column, string term, DataTable table)
        {
            SearchColumn = column;
            SearchTerm = term;
            SearchTable = table;
        }

        public SearchData(string column, string term, DataTable table, SearchRule rule)
        {
            SearchColumn = column;
            SearchTerm = term;
            SearchTable = table;
            SearchRule = rule;
        }

        public string SearchColumn { get; set; } = "";
        public string SearchTerm { get; set; } = "";
        public SearchRule SearchRule { get; set; } = SearchRule.ContainsKey;
        public DataTable SearchTable { get; set; }
    }
}