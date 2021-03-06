﻿using PlexDL.Common.Structures.Plex;
using PlexDL.UI.Forms;
using System.Windows.Forms;
using UIHelpers;

#pragma warning disable 1591

// ReSharper disable InconsistentNaming

namespace PlexDL.UI
{
    public static class UIUtils
    {
        public static void RunMetadataWindow(PlexObject metadata, bool appRun = false)
        {
            var form = new Metadata();
            if (metadata != null)
            {
                form.StreamingContent = metadata;

                if (appRun)
                    Application.Run(form);
                else
                    form.ShowDialog();
            }
            else
            {
                UIMessages.Error(@"Invalid PlexMovie Metadata File; the decoded data was null.",
                    @"Validation Error");
            }
        }

        public static void RunPlexDlHome(bool appRun = false)
        {
            var form = new Home();

            if (appRun)
                Application.Run(form);
            else
                form.ShowDialog();
        }

        public static void RunTestingWindow(bool appRun = false)
        {
            var form = new TestForm();

            if (appRun)
                Application.Run(form);
            else
                form.ShowDialog();
        }

        public static void RunTranslator(bool appRun = false)
        {
            var form = new Translator();

            if (appRun)
                Application.Run(form);
            else
                form.ShowDialog();
        }
    }
}