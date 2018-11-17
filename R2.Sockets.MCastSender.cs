#region Copyright & License
/*
    
R2.Sockets.MCastReceiver.cs
User interface and code for sending data using Multicast protocol.

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
    public partial class FormMCastSenderN : Form
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();


        private bool _connected;
        private IPEndPoint _endPoint;

        private Socket _mcastSocket;

        private MCastSettings _settings;
        public DelLogText DelProcessLogText;
        public DataFlowStats FlowStats;

        public IniData IniDataMain;

        public string SectionName = "SenderMCast";

        //-----------------------------------------------------------------------------------------------------
        public FormMCastSenderN(string connectionName, ushort dataId)
        {
            InitializeComponent();

            _settings = new MCastSettings
            {
                ConnectionName = connectionName,
                DataId = dataId
            };
            SectionName = "SenderMCast " + connectionName;
            FlowStats = new DataFlowStats();
            _connected = false;
        }

        private void FormMCastReceiver_Load(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;
            Text = Application.ProductName + " MCastSend." + _settings.ConnectionName + "," + _settings.DataId;

            ReadSettings();
        }

        //-----------------------------------------------------------------------------------------------------
        private void chkActiveMCast_CheckedChanged(object sender, EventArgs e)
        {
            if (chkActiveMCast.Checked)
            {
                EnableControls(false);
                SaveSettings();
                FlowStats.Reset();
                StartMCast();
            }
            else
            {
                _connected = false;
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
        }

        //-----------------------------------------------------------------------------------------------------
        private void Controls2Settings()
        {
            _settings.MCastGroup = txtMCastGroup.Text.Trim();
            _settings.ServerPort = Convert.ToInt32(textBoxPort.Text);
            _settings.LanCardIpMcast = txtLANIPMCast.Text.Trim();
        }

        //-----------------------------------------------------------------------------------------------------
        private void Settings2Controls()
        {
            txtMCastGroup.Text = _settings.MCastGroup;
            textBoxPort.Text = _settings.ServerPort.ToString();
            txtLANIPMCast.Text = _settings.LanCardIpMcast;
        }

        private void StartMCast()
        {
            try
            {
                var localEp = new IPEndPoint(IPAddress.Any, _settings.ServerPort);

                _mcastSocket = new Socket(AddressFamily.InterNetwork,
                    SocketType.Dgram,
                    ProtocolType.Udp)
                {
                    EnableBroadcast = true,
                    ExclusiveAddressUse = false, // MulticastLoopback = true,

                    Ttl = 3
                };

                var localIpAddr = IPAddress.Any; // Parse("127.0.0.1");  // accept
                _mcastSocket.Bind(localEp);

                // Define a MulticastOption object specifying the multicast group 
                // address and the local IP address.
                // The multicast group address is the same as the address used by the listener.
                var mcastAddress = IPAddress.Parse(_settings.MCastGroup);
                var mcastOption = new MulticastOption(mcastAddress, localIpAddr);

                _mcastSocket.SetSocketOption(SocketOptionLevel.IP,
                    SocketOptionName.AddMembership,
                    mcastOption);

                _endPoint = new IPEndPoint(mcastAddress, _settings.ServerPort);

                _connected = true;
            }
            catch (Exception e)
            {
                logger.Error(e);
                DelProcessLogText?.Invoke("E", e.Message, e);
            }
        }

        public void StopMCast()
        {
            _connected = false;
            _mcastSocket?.Close();
        }

        //-----------------------------------------------------------------------------------------------------
        public void SendDataPartofPacket(DataPacketV2 dataPacketV2)
        {
            FlowStats.InPktCount++;
            FlowStats.LastPktSize = dataPacketV2.PktLength - 14;
            FlowStats.LastPktTime = DateTime.Now;
            FlowStats.LastSourceIp = "";

            if (_connected)
            {
                _mcastSocket.SendTo(dataPacketV2.Data, _endPoint); // dataPacketV2.PktLength - 14
                if (checkBoxLog.Checked)
                    logger.Info(dataPacketV2.ToString());
                FlowStats.InDataSize += FlowStats.LastPktSize;
            }
            else
            {
                logger.Error(dataPacketV2.ToString());
                FlowStats.LastSourceIp = "Not Active";
            }

            if (checkBoxShow.Checked || FlowStats.InPktCount % 500 == 0)
                UpdateStats();
        }

        public void SendRaw(byte[] data, int dataLength)
        {
            FlowStats.InPktCount++;
            FlowStats.LastPktSize = dataLength;
            FlowStats.LastPktTime = DateTime.Now;
            FlowStats.LastSourceIp = "";

            if (_connected)
            {
                _mcastSocket.SendTo(data, _endPoint);
                FlowStats.InDataSize += FlowStats.LastPktSize;
            }
            else
            {
                FlowStats.LastSourceIp = "Not Active";
            }

            if (checkBoxShow.Checked || FlowStats.InPktCount % 1000 == 0)
                UpdateStats();
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
                    lblLastPktTime.Text = FlowStats.LastPktTime.ToString("h:mm:ss.ff t");
                    lblSourceIP.Text = FlowStats.LastSourceIp;
                };
                Invoke(i);
            }
            else
            {
                lblInDataSize.Text = FlowStats.InDataSize.ToString();
                lblInPktCount.Text = FlowStats.InPktCount.ToString();
                lblPktSize.Text = FlowStats.LastPktSize.ToString();
                lblLastPktTime.Text = FlowStats.LastPktTime.ToString("h:mm:ss.ff t");
                lblSourceIP.Text = FlowStats.LastSourceIp;
            }
        }

        //-----------------------------------------------------------------------------------------------------
        private void buttonReset_Click(object sender, EventArgs e)
        {
            FlowStats.Reset();
        }

        private void checkBoxShow_CheckedChanged(object sender, EventArgs e)
        {
            UpdateStats();
        }

        private void checkBoxLog_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void FormMCastReceiver_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
            SaveSettings();
        }

        public void CloseAll()
        {
            SaveSettings();
            StopMCast();
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
            }
        }

        public void ReadSettings()
        {
            if (IniDataMain == null)
                return;

            //int.TryParse(IniDataMain[SectionName]["DataId"], out int dataId);
            int.TryParse(IniDataMain[SectionName]["ServerPort"], out var serverport);
            _settings.ServerPort = serverport;

            _settings.MCastGroup = IniDataMain[SectionName]["MCastGroup"];
            _settings.LanCardIpMcast = IniDataMain[SectionName]["LanCardIpMcast"];
            _settings.UseIpFilter = IniDataMain[SectionName]["UseIpFilter"] == "Y";
            _settings.ValidSourceIp = IniDataMain[SectionName]["ValidSourceIp"];

//            _settings.DataId = (short)dataId;
            Settings2Controls();
        }
    }
}