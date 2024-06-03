using Addressbook.Application.Enums;
using Addressbook.Domain.Models;
using Addressbook.Models.Dtos;

namespace Addressbook.Application.Handlers
{
    public interface IAddressbookHandler
    {
        Task AddIPAddressAsync(IpAddressBookDto ipAddressBookDto);
        Task RemoveAsync(IpAddressBookDto ipAddressBookDto);
        Task RemoveByIpAddressAsync(string ip);
        Task<IpAddressBookDto?> FindIpAddressAsync(IpAddressBookDto ipAddressBookDto);
        Task<IEnumerable<IpAddressBookDto>?> GetAllIps();
        Task<IEnumerable<IpAddressBookDto>?> GetIpV4Address();
        Task<IEnumerable<IpAddressBookDto>?> GetIpAddressByVersion(int version);
        Task<IEnumerable<IpAddressBookDto>> GetOrderedIpsAsync(OrderByEnum order);
    }
}
