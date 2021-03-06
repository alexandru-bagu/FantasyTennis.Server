﻿namespace FTServer.Contracts.Security
{
    public interface IMessageSerialService
    {
        ushort ComputeSend(byte[] buffer, int offset);
        bool ValidateReceive(ushort serial);
        void UpdateKeys(int sendKey, int receiveKey);
    }
}
