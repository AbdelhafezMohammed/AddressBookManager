using Addressbook.Domain.Functions;
using Addressbook.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Addressbook.Domain
{
    public class AddressbookContext : DbContext
    {
        public AddressbookContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<IpAddressBook> Addressbook { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //TODO: https://learn.microsoft.com/en-us/dotnet/csharp/linq/how-to-extend-linq
            //TODO: https://learn.microsoft.com/en-us/ef/core/querying/user-defined-function-mapping
            modelBuilder.HasDbFunction(() => IpCustomSqlFunction.IsValidIpV4Address(default))
                    .HasName("IsValidIpV4Address");

            modelBuilder.Entity<IpAddressBook>().HasData(
                new IpAddressBook
                {
                    Id = 1,
                    IP = "60.26.175.237",
                    Version = 4,
                },
                new IpAddressBook
                {
                    Id = 2,
                    IP = "251.91.56.101",
                    Version = 4,
                },
                new IpAddressBook
                {
                    Id = 3,
                    IP = "50.36.104.132",
                    Version = 4,
                },
                  new IpAddressBook
                  {
                      Id = 4,
                      IP = "ec23:9304:d868:0790:aa8a:db53:cdbf:8e29",
                      Version = 6,
                  },
                new IpAddressBook
                {
                    Id = 5,
                    IP = "591f:9930:9294:a1fb:2802:b95f:af8f:bac0",
                    Version = 6,
                },
                new IpAddressBook
                {
                    Id = 6,
                    IP = "7c6b:8d93:c304:08ae:7a8c:aae6:6af7:ff60",
                    Version = 6,
                });
        }
    }
}
