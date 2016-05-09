using System;

namespace Baboon.Network
{
    public interface INetworkInformation
    {
        NetworkStatus Status { get; }
        NetworkType NetworkType { get; }

        event EventHandler<NetworkStatusChangedEventArgs> NetworkStatusChanged;
    }
}