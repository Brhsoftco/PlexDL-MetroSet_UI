﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PlexDL.Common;
using PlexDL.Common.API;
using PlexDL.Common.Caching;
using PlexDL.Common.Caching.Handlers;
using PlexDL.Common.Globals;
using PlexDL.Common.Logging;
using PlexDL.Common.Renderers.DGVRenderers;
using PlexDL.Common.Structures;
using PlexDL.PlexAPI;
using PlexDL.WaitWindow;

namespace PlexDL.UI
{
    public partial class ServerManager : Form
    {
        public ServerManager()
        {
            InitializeComponent();
        }

        public Server SelectedServer { get; set; }

        private void itmAuthenticate_Click(object sender, EventArgs e)
        {
            try
            {
                //check if there's a connection before trying to update the authentication token
                if (wininet.CheckForInternetConnection())
                    using (var frm = new Authenticate())
                    {
                        var existingInfo = new ConnectionInfo
                        {
                            PlexAccountToken = GlobalStaticVars.Settings.ConnectionInfo.PlexAccountToken
                        };
                        frm.ConnectionInfo = existingInfo;
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            GlobalStaticVars.User.authenticationToken = frm.ConnectionInfo.PlexAccountToken;
                            GlobalStaticVars.Settings.ConnectionInfo.PlexAccountToken = frm.ConnectionInfo.PlexAccountToken;
                            itmLoad.Enabled = true;
                            dgvServers.DataSource = null;
                            MessageBox.Show(@"Token applied successfully. You can now load servers and relays from Plex.tv", @"Message",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                else
                // trying to connect on no connection will not end well; alert the user.
                    MessageBox.Show(@"No internet connection. Please connect to a network before attempting to authenticate.", @"Network Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "ConnectionError");
                MessageBox.Show("Connection Error:\n\n" + ex, @"Connection Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void RenderServersView(List<Server> servers)
        {
            WaitWindow.WaitWindow.Show(WorkerRenderServersView, "Updating Servers", servers);
        }

        private void WorkerRenderServersView(object sender, WaitWindowEventArgs e)
        {
            var servers = (List<Server>)e.Arguments[0];
            ServerViewRenderer.RenderView(servers, dgvServers);
        }

        private void GetServerListWorker(object sender, WaitWindowEventArgs e)
        {
            Helpers.CacheStructureBuilder();
            if (ServerCaching.ServerInCache(GlobalStaticVars.Settings.ConnectionInfo.PlexAccountToken) &&
                GlobalStaticVars.Settings.CacheSettings.Mode.EnableServerCaching)
            {
                e.Result = ServerCaching.ServerFromCache(GlobalStaticVars.Settings.ConnectionInfo.PlexAccountToken);
            }
            else
            {
                var result = GlobalStaticVars.Plex.GetServers(GlobalStaticVars.User);
                if (GlobalStaticVars.Settings.CacheSettings.Mode.EnableServerCaching)
                    ServerCaching.ServerToCache(result, GlobalStaticVars.User.authenticationToken);
                e.Result = result;
            }
        }

        private void GetRelaysListWorker(object sender, WaitWindowEventArgs e)
        {
            var result = Relays.GetServerRelays(GlobalStaticVars.User.authenticationToken);
            e.Result = result;
        }

        private void itmServers_Click(object sender, EventArgs e)
        {
            try
            {
                //check if there's a connection before trying to contact Plex.tv
                if (wininet.CheckForInternetConnection())
                {
                    if (GlobalStaticVars.User.authenticationToken != "")
                    {
                        var serversResult = WaitWindow.WaitWindow.Show(GetServerListWorker, "Fetching Servers");
                        var servers = (List<Server>)serversResult;
                        if (servers.Count == 0)
                        {
                            var msg =
                            MessageBox.Show(@"No servers found for current account token. Please update your token or try a direct connection.",
                                @"Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            GlobalStaticVars.PlexServers = servers;
                            RenderServersView(servers);
                            itmConnect.Enabled = true;
                        }
                    }
                }
                else
                {
                    // trying to connect on no connection will not end well; alert the user.
                    MessageBox.Show(@"No internet connection. Please connect to a network before attempting to load servers.", @"Network Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Server retrieval error\n\n" + ex, @"Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoggingHelpers.RecordException(ex.Message, "ServerGetError");
            }
        }

        private void itmRelays_Click(object sender, EventArgs e)
        {
            try
            {
                //check if there's a connection before trying to contact Plex.tv
                if (wininet.CheckForInternetConnection())
                {
                    if (GlobalStaticVars.User.authenticationToken != "")
                    {
                        var serversResult = WaitWindow.WaitWindow.Show(GetRelaysListWorker, "Fetching Relays");
                        var servers = (List<Server>)serversResult;
                        if (servers.Count == 0)
                        {
                            var msg =
                            MessageBox.Show(@"No relays found for current account token. Please update your token or try a direct connection.",
                                @"Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            GlobalStaticVars.PlexServers = servers;
                            RenderServersView(servers);
                            itmConnect.Enabled = true;
                        }
                    }
                }
                else
                {
                    // trying to connect on no connection will not end well; alert the user.
                    MessageBox.Show(@"No internet connection. Please connect to a network before attempting to load relays.", @"Network Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Relay retrieval error\n\n" + ex, @"Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoggingHelpers.RecordException(ex.Message, "RelayGetError");
            }
        }

        private void itmConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (wininet.CheckForInternetConnection())
                {
                    if (dgvServers.SelectedRows.Count == 1)
                    {
                        var index = dgvServers.CurrentCell.RowIndex;
                        var s = GlobalStaticVars.PlexServers[index];
                        SelectedServer = s;
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                }
                else
                {
                    // trying to connect on no connection will not end well; alert the user.
                    MessageBox.Show(@"No internet connection. Please connect to a network before attempting to start a Plex server connection.",
                        @"Network Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, @"ConnectionError");
                MessageBox.Show("Server connection attempt failed\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RunDirectConnect()
        {
            var servers = new List<Server>();
            using (var frmDir = new DirectConnect())
            {
                frmDir.ConnectionInfo.PlexAccountToken = GlobalStaticVars.User.authenticationToken;
                if (frmDir.ShowDialog() == DialogResult.OK)
                {
                    GlobalStaticVars.Settings.ConnectionInfo = frmDir.ConnectionInfo;
                    GlobalStaticVars.User.authenticationToken = frmDir.ConnectionInfo.PlexAccountToken;
                    var s = new Server
                    {
                        accessToken = GlobalStaticVars.User.authenticationToken,
                        address = GlobalStaticVars.Settings.ConnectionInfo.PlexAddress,
                        port = GlobalStaticVars.Settings.ConnectionInfo.PlexPort,
                        name = "DirectConnect"
                    };
                    servers.Add(s);
                    GlobalStaticVars.PlexServers = servers;
                    SelectedServer = s;
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
        }

        private void dgvServers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvServers.SelectedRows.Count == 1)
                itmConnect.Enabled = true;
            else
                itmConnect.Enabled = false;
        }

        private void ServerManager_Load(object sender, EventArgs e)
        {
            try
            {
                //check if there's an internet connection first; it may have been disconnected while the window was closed.
                if (wininet.CheckForInternetConnection())
                {
                    // this check must be done before checking the count, because if it is null, we'll get an error for trying to access "Count" on a null object.
                    if (GlobalStaticVars.PlexServers != null)
                        if (GlobalStaticVars.PlexServers.Count > 0 && GlobalStaticVars.Svr != null)
                        {
                            RenderServersView(GlobalStaticVars.PlexServers);
                            var svrIndex = GlobalStaticVars.PlexServers.IndexOf(GlobalStaticVars.Svr);
                            dgvServers.Rows[svrIndex].Selected = true;
                            itmConnect.Enabled = true;
                        }
                }
                else
                {
                    // trying to connect on no connection will not end well; close this window if there's no connection and alert the user.
                    MessageBox.Show(@"No internet connection. Please connect to a network before attempting to start a Plex server connection.",
                        @"Network Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Close();
                }
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, @"ServerManagerLoadError");
                MessageBox.Show("Server manager failed to load necessary data\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void itmDirectConnection_Click(object sender, EventArgs e)
        {
            RunDirectConnect();
        }
    }
}