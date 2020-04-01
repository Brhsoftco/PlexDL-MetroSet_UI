﻿using System.Data;

namespace PlexDL.Common.Globals
{
    public static class RowGet
    {
        public static DataRow GetDataRowTbl(DataTable table, int index)
        {
            return table.Rows[index];
        }

        public static DataRow GetDataRowContent(int index, bool directTable)
        {
            return !directTable ? GetDataRowTbl(GlobalTables.ReturnCorrectTable(), index) : GetDataRowTbl(Flags.IsFiltered ? GlobalTables.FilteredTable : GlobalTables.TitlesTable, index);
        }

        public static DataRow GetDataRowSeries(int index)
        {
            return GetDataRowTbl(GlobalTables.SeriesTable, index);
        }

        public static DataRow GetDataRowEpisodes(int index)
        {
            return GetDataRowTbl(GlobalTables.EpisodesTable, index);
        }

        public static DataRow GetDataRowLibrary(int index)
        {
            return GetDataRowTbl(GlobalTables.SectionsTable, index);
        }
    }
}