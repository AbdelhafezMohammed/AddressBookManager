using Addressbook.Domain.Models;

namespace Addressbook.Domain.Repositories
{
    public interface IAddressBookRepository
    {
        Task<IEnumerable<IpAddressBook>> GetAsync();
        Task AddAsync(IpAddressBook ipAddressBook);
        Task<IpAddressBook?> FindByIPAsync(string ip);
        void Remove(IpAddressBook ipAddressBook);
        Task RemoveByIdAsync(int id);
        Task<IEnumerable<IpAddressBook>> GetIpV4Address();
        Task<IEnumerable<IpAddressBook>> GetIpsByVersionAsync(int version);
        Task<IEnumerable<IpAddressBook>> GetOrderedIpsAsync(string order);
        Task SaveChangesAsync();
    }
}
