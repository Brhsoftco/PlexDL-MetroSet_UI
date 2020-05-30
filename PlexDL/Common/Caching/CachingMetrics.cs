﻿using PlexDL.Common.Logging;
using System;
using System.IO;

namespace PlexDL.Common.Caching
{
    public class CachingMetrics
    {
        public int SERVER_LISTS { get; set; }
        public long SERVER_LISTS_SIZE { get; set; }
        public int API_DOCUMENTS { get; set; }
        public long API_DOCUMENTS_SIZE { get; set; }
        public int THUMBNAIL_IMAGES { get; set; }
        public long THUMBNAIL_IMAGES_SIZE { get; set; }
        public int TOTAL_CACHED { get; set; }
        public long TOTAL_CACHED_SIZE { get; set; }

        public string TotalCacheSize()
        {
            return Methods.FormatBytes(TOTAL_CACHED_SIZE);
        }

        public string ThumbSize()
        {
            return Methods.FormatBytes(THUMBNAIL_IMAGES_SIZE);
        }

        public string ServerListsSize()
        {
            return Methods.FormatBytes(SERVER_LISTS_SIZE);
        }

        public string ApiXmlSize()
        {
            return Methods.FormatBytes(API_DOCUMENTS_SIZE);
        }

        public static CachingMetrics FromLatest()
        {
            var servers = ServerLists();
            var apixml = ApiXml();
            var thumbs = Thumbs();
            var total = AllCached();
            return new CachingMetrics
            {
                SERVER_LISTS = servers.Length,
                SERVER_LISTS_SIZE = SizeOfFiles(servers),
                API_DOCUMENTS = apixml.Length,
                API_DOCUMENTS_SIZE = SizeOfFiles(apixml),
                THUMBNAIL_IMAGES = thumbs.Length,
                THUMBNAIL_IMAGES_SIZE = SizeOfFiles(thumbs),
                TOTAL_CACHED = total.Length,
                TOTAL_CACHED_SIZE = SizeOfFiles(total)
            };
        }

        public static long SizeOfFiles(string[] files)
        {
            long size = 0;
            try
            {
                foreach (var f in files)
                    if (File.Exists(f))
                    {
                        var fi = new FileInfo(f);
                        size += fi.Length;
                    }
            }
            catch (Exception ex)
            {
                //log it and then continue as normal
                LoggingHelpers.RecordException(ex.Message, "CacheSizeCalcError");
            }

            return size;
        }

        public static string[] AllCached()
        {
            string[] files =
            {
            };
            if (Directory.Exists(CachingFileDir.RootCacheDirectory))
                files = Directory.GetFiles(CachingFileDir.RootCacheDirectory, "*", SearchOption.AllDirectories);
            return files;
        }

        public static string[] ServerLists()
        {
            string[] files =
            {
            };
            if (Directory.Exists(CachingFileDir.RootCacheDirectory))
                files = Directory.GetFiles(CachingFileDir.RootCacheDirectory, "*" + CachingFileExt.ServerListExt, SearchOption.AllDirectories);
            return files;
        }

        public static string[] Thumbs()
        {
            string[] files =
            {
            };
            if (Directory.Exists(CachingFileDir.RootCacheDirectory))
                files = Directory.GetFiles(CachingFileDir.RootCacheDirectory, "*" + CachingFileExt.ThumbExt, SearchOption.AllDirectories);
            return files;
        }

        public static string[] ApiXml()
        {
            string[] files =
            {
            };
            if (Directory.Exists(CachingFileDir.RootCacheDirectory))
                files = Directory.GetFiles(CachingFileDir.RootCacheDirectory, "*" + CachingFileExt.ApiXmlExt, SearchOption.AllDirectories);
            return files;
        }
    }
}