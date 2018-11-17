#region Copyright & License
/*
    
R2.Socket.Interfaces.cs
Various utility classes and interfaces.

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

External Dependencies: None
*/
#endregion 
using System;
using System.Runtime.InteropServices;

namespace R2.Sockets.Net
{
    public delegate void PacketInHandler(DataPacketV2 dpacket);



//    public delegate void ProcessLog(string level, string message);

    public delegate void DelLogText(string type, string message, Exception ex);

    //    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)] // 14 + ptr
    public struct DataPacketV2
    {
        public ushort Signature; // 65456
        public ushort PktLength; // including header of 14
        public ushort StreamId; // data id
        public int SeqNo;
        public int TimeNo; // time of the day in milliseconds
        public byte[] Data; //         public char MsgType as 1st char

        public static bool IsCorrectSignature(ushort sig)
        {
            return sig == 65456;
        }

        public void Prepare()
        {
            Signature = 65456;
            TimeNo = (int) (DateTime.Now - DateTime.Today).TotalMilliseconds;
            PktLength = (ushort) (Data.Length + 14);
        }

        public override string ToString()
        {
            var dataDateTime = DateTime.Today.AddMilliseconds(TimeNo);
            return $"{PktLength},{StreamId},{SeqNo},{Data.Length},{TimeNo},{dataDateTime:HH:mm:ss.fff}";
        }
    }

    //-----------------------------------------------------------------------------------------------------
    public struct DataFlowStats
    {
        public uint InPktCount;
        public int RejectPktCount;
        public long InDataSize;
        public int LastPktSize;
        public DateTime LastPktTime;
        public string LastSourceIp;
        public DateTime LastTraceTime;
        public long LastTraceSize;

        //-----------------------------------------------------------------------------------------------------
        public void Reset()
        {
            InPktCount = 0;
            RejectPktCount = 0;
            InDataSize = 0;
            LastPktSize = 0;
            LastPktTime = DateTime.Today;
            LastSourceIp = "";
            LastTraceSize = 0;
            LastTraceTime = DateTime.Today;
        }

        public override string ToString()
        {
            return $"{InPktCount},{RejectPktCount},{InDataSize},{LastPktSize},{LastPktTime},{LastSourceIp}";
        }

        public int GetCps()
        {
            var cps = 0;
            if (LastTraceTime > DateTime.Today)
                cps = (int) ((InDataSize - LastTraceSize) / (DateTime.Now - LastTraceTime).TotalSeconds);

            return cps;
        }
    }
}