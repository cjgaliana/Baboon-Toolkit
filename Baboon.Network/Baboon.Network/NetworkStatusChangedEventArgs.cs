using System;

namespace Baboon.Network
{
    public class NetworkStatusChangedEventArgs : EventArgs
    {
        public NetworkStatusChangedEventArgs(NetworkStatus status, NetworkType type)
        {
            Status = status;
            Type = type;
        }

        public NetworkStatus Status { get; }
        public NetworkType Type { get; }
    }
}