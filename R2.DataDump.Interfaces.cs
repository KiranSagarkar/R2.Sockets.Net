#region Copyright & License
/*
    
R2.DataDump.Interfaces.cs
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
    public enum ExchId // shorter list of indian exch, segments
    {

        NCF = 12,
        NFO = 15,
        NSE = 17,

    }

    public enum DataType
    {
        ExchMsg = 0,
        ExchFeed = 1,
    }

    public class DataRecordType
    {
        public byte[] DataIdBytes;
        public char ExchChar;
        public ushort RDataId;
        public DataType RDataTypeId;
        public ExchId RExchId;

        public DataRecordType(ExchId exchId, DataType dataTypeId)
        {
            RExchId = exchId;
            RDataTypeId = dataTypeId;
            RDataId = (ushort) ((byte) RExchId * 10 + (byte) RDataTypeId);
            DataIdBytes = BitConverter.GetBytes(RDataId);
            var exch = RExchId.ToString();
            ExchChar = exch[0];
            if (RExchId == ExchId.NFO)
                ExchChar = 'F';
            else if (RExchId == ExchId.NCF)
                ExchChar = 'U';
        }

        public void SetRecordType(string exchName, string dataType)
        {
            Enum.TryParse(exchName, out RExchId);
            Enum.TryParse(dataType, out RDataTypeId);
            RDataId = (ushort) ((byte) RExchId * 10 + (byte) RDataTypeId);
            DataIdBytes = BitConverter.GetBytes(RDataId);
        }

        public string GetDataName()
        {
            return RExchId + "." + RDataTypeId;
        }

        public ushort GetDataId()
        {
            return RDataId;
        }

        public static byte[] GetDataIdBytes(byte exchId, DataType dataTypeId)
        {
            var dataId = (ushort) (exchId * 10 + (byte) dataTypeId);
            return BitConverter.GetBytes(dataId);
        }
    }


    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)] // 7 + ptr + ptr
    public struct DumpPacket
    {
        public int TimeNo; // time of the day in milliseconds
        public byte RecTypeLen; // 
        public ushort DataLen; // 
        public byte[] RecType; // record type
        public byte[] Data; //  


        public void Prepare()
        {
            TimeNo = (int) (DateTime.Now - DateTime.Today).TotalMilliseconds;
        }

        public override string ToString()
        {
            var dataDateTime = DateTime.Today.AddMilliseconds(TimeNo);
            return $"{dataDateTime:HH:mm:ss.fff},{RecTypeLen},{RecType},{DataLen}";
        }
    }

    public delegate void ProcessLog(string level, string message);

    public delegate void DumpPacketHandler(DumpPacket dumpPacket);

    public delegate void RecordReceivedEventHandler<H, T>(H header, T record);

    public delegate void DataReceivedEventHandler(ushort dataId, byte[] data);

    public delegate void DelLogText2Parent(string type, string message, Exception ex);
}