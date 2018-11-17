#region Copyright & License
/*
    
R2.Chunk.cs
Class to merge multiple small data packets into one bigger packet(chunk). 

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
using System.IO;

namespace R2.Sockets.Net
{
    public class Chunk
    {
        private readonly int _chunkSize;
        private readonly int _maxDelay;
        private readonly object _thisLock = new object();
        private MemoryStream _chunkStream;
        private DateTime _lastSendTime;

        public BinaryWriter CWriter;

        public Chunk(int chunkSize, int maxDelay)
        {
            _chunkSize = chunkSize;
            _maxDelay = maxDelay;

            _chunkStream = new MemoryStream(_chunkSize + 512);
            CWriter = new BinaryWriter(_chunkStream);
        }

        public bool IsReady2Send()
        {
            long streamLen;
            lock (_thisLock)
            {
                CWriter.Flush();
                streamLen = _chunkStream.Length;
            }

            return streamLen >= _chunkSize || DateTime.Now >= _lastSendTime.AddMilliseconds(_maxDelay);
        }

        public byte[] GetDataAndClear()
        {
            byte[] chunkBytes;
            lock (_thisLock)
            {
                _chunkStream.Flush();
                chunkBytes = _chunkStream.ToArray();
                _chunkStream = new MemoryStream(_chunkStream.Capacity);
                CWriter = new BinaryWriter(_chunkStream);
            }

            _lastSendTime = DateTime.Now;
            return chunkBytes;
        }

        public void Write2Chunk(byte[] recType, byte[] data)
        {
            lock (_thisLock)
            {
                var recTypeLen = (byte) recType.Length;
                var dataLen = (ushort) data.Length;
                CWriter.Write(recTypeLen);
                CWriter.Write(dataLen);
                CWriter.Write(recType);
                CWriter.Write(data);
            }
        }

        public void WriteData2Chunk(byte[] data)
        {
            lock (_thisLock)
            {
                CWriter.Write(data);
            }
        }
    }
}