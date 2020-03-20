﻿using PlexDL.UI;
using System;
using System.IO;

namespace PlexDL.Common.Logging
{
    public static class LoggingHelpers
    {
        private static int logIncrementer = 0;

        public static void AddToLog(string logEntry)
        {
            try
            {
                logIncrementer++;
                string[] headers =
                {
                    "ID", "DateTime", "Entry"
                };
                string[] logEntryToAdd =
                {
                    logIncrementer.ToString(), DateTime.Now.ToString(), logEntry
                };
                if (GlobalStaticVars.Settings.Logging.EnableGenericLogDel)
                    LogDelWriter("PlexDL.logdel", headers, logEntryToAdd);
            }
            catch
            {
                //ignore the error
                return;
            }
        }

        public static void RecordException(string message, string type)
        {
            try
            {
                ////Options weren't too great performance-wise, so I ended up using a stack-walk.
                ////If there's minimal errors happening at once, this shouldn't be a problem, otherwise disable
                ////The in-app setting to prevent this method from firing.
                if (GlobalStaticVars.Settings.Logging.EnableExceptionLogDel)
                {
                    var stackTrace = new System.Diagnostics.StackTrace();
                    var function = stackTrace.GetFrame(1).GetMethod().Name;
                    string[] headers =
                    {
                        "DateTime", "ExceptionMessage", "OccurredIn", "ExceptionType"
                    };
                    string[] LogEntry =
                    {
                        DateTime.Now.ToString(), message, function, type
                    };
                    LogDelWriter("ExceptionLog.logdel", headers, LogEntry);
                }
            }
            catch
            {
                //ignore the error
                return;
            }
        }

        public static void RecordTransaction(string uri, string statusCode)
        {
            try
            {
                if (GlobalStaticVars.Settings.Logging.EnableXMLTransactionLogDel)
                {
                    string[] headers =
                    {
                        "DateTime", "Uri", "StatusCode"
                    };
                    string[] LogEntry =
                    {
                        DateTime.Now.ToString(), uri, statusCode
                    };
                    LogDelWriter("TransactionLog.logdel", headers, LogEntry);
                }
            }
            catch
            {
                //ignore the error
                return;
            }
        }

        public static void LogDelWriter(string fileName, string[] headers, string[] logEntry)
        {
            try
            {
                if (!Directory.Exists("Logs"))
                    Directory.CreateDirectory("Logs");

                var logdelLine = "";
                var fqFile = @"Logs\" + fileName;

                foreach (var l in logEntry)
                    logdelLine += l + "!";

                logdelLine = logdelLine.TrimEnd('!');

                var headersString = "###";
                foreach (var h in headers)
                    headersString += h + "!";

                headersString = headersString.TrimEnd('!');

                if (File.Exists(fqFile))
                {
                    var existing = File.ReadAllText(fqFile);
                    if (string.IsNullOrEmpty(existing))
                    {
                        var contentToWrite = headersString + "\n" + logdelLine;
                        File.WriteAllText(fqFile, contentToWrite);
                    }
                    else
                    {
                        var contentToWrite = existing + "\n" + logdelLine;
                        File.WriteAllText(fqFile, contentToWrite);
                    }
                }
                else
                {
                    var contentToWrite = headersString + "\n" + logdelLine;
                    File.WriteAllText(fqFile, contentToWrite);
                }
            }
            catch
            {
                //ignore the error
                return;
            }
        }
    }
}