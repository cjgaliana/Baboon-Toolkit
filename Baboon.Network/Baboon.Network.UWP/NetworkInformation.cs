using System;
using Windows.Networking.Connectivity;

namespace Baboon.Network.UWP
{
    public class NetworkInformation : INetworkInformation
    {
        public NetworkInformation()
        {
            Windows.Networking.Connectivity.NetworkInformation.NetworkStatusChanged += OnNetworkStatusChanged;
        }

        public NetworkStatus Status => GetNetworkStatus();

        public NetworkType NetworkType => GetNetworkType();

        public event EventHandler<NetworkStatusChangedEventArgs> NetworkStatusChanged;

        private void OnNetworkStatusChanged(object sender)
        {
            var handle = NetworkStatusChanged;
            handle?.Invoke(this, new NetworkStatusChangedEventArgs(Status, NetworkType));
        }

        private NetworkStatus GetNetworkStatus()
        {
            var internetConnectionProfile =
                Windows.Networking.Connectivity.NetworkInformation.GetInternetConnectionProfile();
            var connectionLevel = internetConnectionProfile?.GetNetworkConnectivityLevel();
            switch (connectionLevel)
            {
                case NetworkConnectivityLevel.None:
                    return NetworkStatus.Offline;

                case NetworkConnectivityLevel.LocalAccess:
                    return NetworkStatus.Online;

                case NetworkConnectivityLevel.ConstrainedInternetAccess:
                    return NetworkStatus.Constrained;

                case NetworkConnectivityLevel.InternetAccess:
                    return NetworkStatus.Online;

                case null:
                default:
                    return NetworkStatus.Unknow;
            }
        }

        private NetworkType GetNetworkType()
        {
            var profile = Windows.Networking.Connectivity.NetworkInformation.GetInternetConnectionProfile();
            if (profile == null)
            {
                return NetworkType.Unknow;
            }
            if (profile.IsWlanConnectionProfile)
            {
                var info = profile.WlanConnectionProfileDetails;
                var name = info.GetConnectedSsid();
                return NetworkType.WiFi;
            }
            if (profile.IsWwanConnectionProfile)
            {
                var info = profile.WwanConnectionProfileDetails;
                var name = info.AccessPointName;
                var speed = info.GetCurrentDataClass() /*.ToString()*/;
                return NetworkType.OTA;
            }

            return NetworkType.Unknow;
        }
    }
}