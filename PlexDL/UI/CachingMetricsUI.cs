﻿using PlexDL.Common.Caching;
using System;
using PlexDL.Common.Logging;
using System.Windows.Forms;

namespace PlexDL.UI
{
    public partial class CachingMetricsUI : Form
    {
        public CachingMetrics Metrics { get; set; } = null;

        public CachingMetricsUI()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void CachingMetricsUI_Load(object sender, EventArgs e)
        {
            try
            {
                //SERVER LISTS
                lblNumberServerListsValue.Text = Metrics.SERVER_LISTS.ToString();
                lblSizeServerListsValue.Text = Metrics.ServerListsSize();

                //THUMBNAIL IMAGES
                lblNumberThumbsCachedValue.Text = Metrics.THUMBNAIL_IMAGES.ToString();
                lblSizeThumbsCachedValue.Text = Metrics.ThumbSize();

                //API XML DOCUMENTS
                lblNumberXmlCachedValue.Text = Metrics.API_DOCUMENTS.ToString();
                lblSizeXmlCachedValue.Text = Metrics.ApiXmlSize();

                //TOTAL CACHED
                lblTotalAmountCachedValue.Text = Metrics.TOTAL_CACHED.ToString();
                lblTotalSizeCachedValue.Text = Metrics.TotalCacheSize();
            }
            catch (Exception ex)
            {
                //log then inform user before exiting
                LoggingHelpers.RecordException(ex.Message, "CacheMetricsLoadError");
                MessageBox.Show("There was an error whilst loading caching metrics:\n\n" + ex.ToString(), "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
    }
}