using System.Net;

namespace Addressbook.Service.Services
{
    public class AddressbookService : IAddressbookService
    {
        public int? GetIpAddressVersion(IPAddress ipAddress)
        {
            int? IpِAddressVersion = null;
            if (ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                IpِAddressVersion = 4;
            }
            else if (ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
            {
                IpِAddressVersion = 6;
            }
            return IpِAddressVersion;
        }

        public IPAddress? ValidateIpAddress(string ipAddress)
        {

            return IPAddress.TryParse(ipAddress, out IPAddress? addressResult) ? addressResult : null;
        }

    }
}
