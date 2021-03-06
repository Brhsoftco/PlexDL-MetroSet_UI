﻿namespace PlexDL.Common.Globals
{
    public static class Flags
    {
        public static bool IsHttps { get; set; } = false;
        public static bool IsDebug { get; set; } = false;
        public static bool IsConnected { get; set; } = false;
        public static bool IsInitialFill { get; set; } = true;
        public static bool IsLibraryFilled { get; set; } = false;
        public static bool IsFiltered { get; set; } = false;
        public static bool IsHttpDownloadQueueCancelled { get; set; } = false;
        public static bool IsDownloadRunning { get; set; } = false;
        public static bool IsDownloadPaused { get; set; } = false;
        public static bool IsEngineRunning { get; set; } = false;
        public static bool IsMsgAlreadyShown { get; set; } = false;
        public static bool IsDownloadAll { get; set; } = false;
    }
}