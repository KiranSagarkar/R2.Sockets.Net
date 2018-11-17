#region Copyright & License
/*
    
R2.Sockets.MCastReceiver.cs
User interface and code for receiving data using Multicast protocol.

Copyright(C) Reliable Software Systems Pvt. Ltd. www.reliable.co.in <2018>  Author: Kiran Sagarkar

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.If not, see<https://www.gnu.org/licenses/>.

External Dependencies: 

1) IniParser MIT License
https://github.com/rickyah/ini-parser 
2) NLog BSD License
https://nlog-project.org/

*/
#endregion 

using System;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using IniParser.Model;
using NLog;


namespace R2.Sockets.Net
{
    //-----------------------------------------------------------------------------------------------------
    public partial class FormMCastReceiverN : Form
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private IPAddress _mcastAddress;
        private MulticastOption _mcastOption;
        private UdpClient _mcastSocket; // Socket

        private int _seqNo;

        private MCastSettings _settings;

        public DelLogText DelProcessLogText;
        public DataFlowStats FlowStats;


        public IniData IniDataMain;

        public string SectionName;
        public int TraceCount = 1000;

        //-----------------------------------------------------------------------------------------------------
        public FormMCastReceiverN(string connectionName, ushort dataId)
        {
            InitializeComponent();


            _settings = new MCastSettings();
            _settings.ConnectionName = connectionName;
            _settings.DataId = dataId;
            SectionName = "ReceiverMCast - " + connectionName;
            FlowStats = new DataFlowStats();
        }

        public event DataReceivedEventHandler DataReceived;

        private void FormMCastReceiver_Load(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;
            Text = Application.ProductName + " MCastRecv." + _settings.ConnectionName + "," + _settings.DataId;

            ReadSettings();
        }

        //-----------------------------------------------------------------------------------------------------
        private void chkActiveMCast_CheckedChanged(object sender, EventArgs e)
        {
            if (chkActiveMCast.Checked)
            {
                EnableControls(false);
                SaveSettings();
//                FlowStats.Reset();
                StartMCast();
                backgroundWorker.RunWorkerAsync();
            }
            else
            {
                backgroundWorker.CancelAsync();
                StopMCast();
                EnableControls(true);
            }
        }

        //-----------------------------------------------------------------------------------------------------
        private void EnableControls(bool enable)
        {
            txtMCastGroup.Enabled = enable;
            textBoxPort.Enabled = enable;
            txtLANIPMCast.Enabled = enable;
            txtSourceIPMCast.Enabled = enable;
            chkUseIpFilter.Enabled = enable;
        }

        //-----------------------------------------------------------------------------------------------------
        private void Controls2Settings()
        {
            _settings.MCastGroup = txtMCastGroup.Text.Trim();
            _settings.ServerPort = Convert.ToInt32(textBoxPort.Text);
            _settings.LanCardIpMcast = txtLANIPMCast.Text.Trim();
//            _settings.Loopback = chkLoopBackMCast.Enabled;
            _settings.ValidSourceIp = txtSourceIPMCast.Text.Trim();
            _settings.UseIpFilter = chkUseIpFilter.Checked;
        }

        //-----------------------------------------------------------------------------------------------------
        private void Settings2Controls()
        {
            txtMCastGroup.Text = _settings.MCastGroup;
            textBoxPort.Text = _settings.ServerPort.ToString();
            txtLANIPMCast.Text = _settings.LanCardIpMcast;
            txtSourceIPMCast.Text = _settings.ValidSourceIp;
            chkUseIpFilter.Checked = _settings.UseIpFilter;
        }


        //-----------------------------------------------------------------------------------------------------
        private void StartMCast()
        {
            try
            {
                _mcastSocket = new UdpClient();

                _mcastSocket.ExclusiveAddressUse = false;
                var localEp = new IPEndPoint(IPAddress.Any, _settings.ServerPort);

                _mcastSocket.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                _mcastSocket.ExclusiveAddressUse = false;
                _mcastSocket.Ttl = 10;
                _mcastSocket.Client.Bind(localEp);

                var multicastaddress = IPAddress.Parse(_settings.MCastGroup);
                _mcastSocket.JoinMulticastGroup(multicastaddress);
            }
            catch (Exception e)
            {
                logger.Error(e);
                DelProcessLogText?.Invoke("E", e.Message, e);
            }
        }


        public void StopMCast()
        {
            _mcastSocket?.Close();
        }


        //-----------------------------------------------------------------------------------------------------
        private void UpdateStats()
        {
            if (lblInDataSize.InvokeRequired)
            {
                MethodInvoker i = () =>
                {
                    lblInDataSize.Text = FlowStats.InDataSize.ToString();
                    lblInPktCount.Text = FlowStats.InPktCount.ToString();
                    lblPktSize.Text = FlowStats.LastPktSize.ToString();
                    lblLastPktTime.Text = FlowStats.LastPktTime.ToString("h:mm:ss.fff t");
                    lblSourceIP.Text = FlowStats.LastSourceIp;
                };
                Invoke(i);
            }
            else
            {
                lblInDataSize.Text = FlowStats.InDataSize.ToString();
                lblInPktCount.Text = FlowStats.InPktCount.ToString();
                lblPktSize.Text = FlowStats.LastPktSize.ToString();
                lblLastPktTime.Text = FlowStats.LastPktTime.ToString("h:mm:ss.fff t");
                lblSourceIP.Text = FlowStats.LastSourceIp;
            }
        }
        //-----------------------------------------------------------------------------------------------------


        //-----------------------------------------------------------------------------------------------------
        private void buttonReset_Click(object sender, EventArgs e)
        {
            FlowStats.Reset();
        }

        private void checkBoxShow_CheckedChanged(object sender, EventArgs e)
        {
            UpdateStats();
        }

        private void FormMCastReceiver_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
            SaveSettings();
        }

        private void FormMCastReceiverN_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseAll();
        }

        public void CloseAll()
        {
            StopMCast();
            SaveSettings();
            DelProcessLogText = null;
        }

        public void SaveSettings()
        {
            Controls2Settings();

            if (IniDataMain != null)
            {
//                IniDataMain[SectionName]["DataId"] = _settings.DataId.ToString();
                IniDataMain[SectionName]["ServerPort"] = _settings.ServerPort.ToString();
                IniDataMain[SectionName]["MCastGroup"] = _settings.MCastGroup;
                IniDataMain[SectionName]["LanCardIpMcast"] = _settings.LanCardIpMcast;
                IniDataMain[SectionName]["UseIpFilter"] = _settings.UseIpFilter ? "Y" : "N";
                IniDataMain[SectionName]["ValidSourceIp"] = _settings.ValidSourceIp;
                IniDataMain[SectionName]["SaveDump"] = _settings.SaveDump ? "Y" : "N";
            }
        }

        public void ReadSettings()
        {
            if (IniDataMain == null)
                return;

            int dataId, serverport;
//            int.TryParse(IniDataMain[SectionName]["DataId"], out dataId);
            int.TryParse(IniDataMain[SectionName]["ServerPort"], out serverport);
            _settings.ServerPort = serverport;

            _settings.MCastGroup = IniDataMain[SectionName]["MCastGroup"];
            _settings.LanCardIpMcast = IniDataMain[SectionName]["LanCardIpMcast"];
            _settings.UseIpFilter = IniDataMain[SectionName]["UseIpFilter"] == "Y";
            _settings.ValidSourceIp = IniDataMain[SectionName]["ValidSourceIp"];
            _settings.SaveDump = IniDataMain[SectionName]["SaveDump"] == "Y";

            //            _settings.DataId = (short)dataId;
            Settings2Controls();
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var localEp = new IPEndPoint(IPAddress.Any, 0); //_settings.ServerPort);


            try
            {
                while (true)
                {
                    if (backgroundWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }

                    var data = _mcastSocket.Receive(ref localEp);
                    mcast_OnDataIn(data);
                }
            }

            catch (Exception ex)
            {
                logger.Error(ex);
                DelProcessLogText?.Invoke("E", ex.Message, ex);
            }
        }

        //-----------------------------------------------------------------------------------------------------
        private void mcast_OnDataIn(byte[] data)
        {
            /*
                        if ((_settings.UseIpFilter) && (e.SourceAddress != _settings.ValidSourceIp))
                        {
                            FlowStats.RejectPktCount++;
                            return;
                        }
            */
            if (data.Length == 0)
                return;

            FlowStats.InPktCount++;
            FlowStats.InDataSize += data.Length;
//            FlowStats.LastSourceIp = e.SourceAddress;
            FlowStats.LastPktSize = data.Length;
            FlowStats.LastPktTime = DateTime.Now;
            try
            {
                DataReceived?.Invoke(_settings.DataId, data); // FlowStats.InPktCount
            }
            catch (Exception e)
            {
                logger.Error(e);
                DelProcessLogText?.Invoke("E", _settings.DataId + "," + e.Message, e);
            }

            if (checkBoxShow.Checked || FlowStats.InPktCount % TraceCount == 0)
                UpdateStats();
            if (FlowStats.InPktCount % TraceCount == 0)
                DelProcessLogText?.Invoke("Stats", _settings.DataId + "," + FlowStats, null);
        }
    }

    //-----------------------------------------------------------------------------------------------------
    public struct MCastSettings
    {
        //-----------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Name of the connection will be shown for futherreference by calling program also this name will appear in Settings
        ///     file.
        /// </summary>
        public string ConnectionName;


        //-----------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Address No for futher reference by calling program.
        ///     useful in multiple connections.
        /// </summary>
        public ushort DataId;

        //-----------------------------------------------------------------------------------------------------
        /// <summary>
        ///     The name of the local host or user-assigned IP interface through which connections
        ///     are initiated or accepted.
        /// </summary>
        public string MCastGroup;

        ///
        //-----------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Port of remote host to receive data from
        /// </summary>
        public int ServerPort;

        public string LanCardIpMcast;

        public bool Loopback;

        //-----------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A Valid Source IP for filteration of data.
        ///     if valid source is set then only data which comes form valid source is processed into InBuffer.
        /// </summary>
        public string ValidSourceIp;

        //-----------------------------------------------------------------------------------------------------
        /// <summary>
        ///     to apply Ip filtering
        /// </summary>
        public bool UseIpFilter;

        public bool SaveDump;
    }
}