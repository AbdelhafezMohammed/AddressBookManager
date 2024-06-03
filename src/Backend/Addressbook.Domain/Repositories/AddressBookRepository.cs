using Addressbook.Domain.Functions;
using Addressbook.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Addressbook.Domain.Repositories
{
    public class AddressBookRepository : IAddressBookRepository
    {
        private readonly AddressbookContext _context;
        public AddressBookRepository(AddressbookContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(IpAddressBook ipAddressBook)
        {
            await _context.Addressbook.AddAsync(ipAddressBook);
        }

        public async Task<IpAddressBook?> FindByIPAsync(string ip)
        {
            return await _context
                .Addressbook
                .FirstOrDefaultAsync(addressbook => addressbook.IP == ip);
        }

        public async Task<IEnumerable<IpAddressBook>> GetAsync()
        {
            return await _context.Addressbook.ToListAsync();
        }

        public async Task<IEnumerable<IpAddressBook>> GetIpsByVersionAsync(int version)
        {
            return await _context
                .Addressbook
                .Where(addressbook => addressbook.Version == version)
                .ToListAsync();
        }

        public async Task<IEnumerable<IpAddressBook>> GetIpV4Address()
        {
            return await _context
                .Addressbook
                .Where(addressbook => IpCustomSqlFunction.IsValidIpV4Address(addressbook.IP))
                .ToListAsync();
        }

        public void Remove(IpAddressBook ipAddressBook)
        {
            _context.Addressbook.Remove(ipAddressBook);
        }

        public async Task RemoveByIdAsync(int id)
        {
            await _context
                .Addressbook
                .Where(addressbook => addressbook.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<IEnumerable<IpAddressBook>> GetOrderedIpsAsync(string order)
        {
            var query = order.Equals("asc", StringComparison.OrdinalIgnoreCase)
                ? _context.Addressbook.OrderBy(addressbook => addressbook.IP)
                : _context.Addressbook.OrderByDescending(addressbook => addressbook.IP);

            return await query.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
