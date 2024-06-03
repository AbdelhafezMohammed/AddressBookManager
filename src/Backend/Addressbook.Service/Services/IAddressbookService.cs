using System.Net;

namespace Addressbook.Service.Services
{
    public interface IAddressbookService
    {
        int? GetIpAddressVersion(IPAddress ipAddress);
        IPAddress? ValidateIpAddress(string ipAddress);
    }
}
