using Addressbook.Application.Enums;
using Addressbook.Domain.Models;
using Addressbook.Domain.Repositories;
using Addressbook.Models.Dtos;
using Addressbook.Service.Services;
using AutoMapper;
using FluentValidation;
using System.Net;

namespace Addressbook.Application.Handlers
{
    /// <summary>
    /// Handles the operations related to the address book.
    /// </summary>
    public class AddressbookHandler : IAddressbookHandler
    {
        private readonly IAddressBookRepository _addressBookRepository;
        private readonly IAddressbookService _addressbookService;
        private readonly IMapper _mapper;
        private readonly IValidator<IpAddressBookDto> _addressbookValidator;

        public AddressbookHandler(IAddressBookRepository addressBookRepository,
            IAddressbookService addressbookService,
            IMapper mapper,
            IValidator<IpAddressBookDto> addressbookValidator)
        {
            _addressBookRepository = addressBookRepository;
            _addressbookService = addressbookService;
            _mapper = mapper;
            _addressbookValidator = addressbookValidator;
        }

        /// <summary>
        /// Adds an IP address to the address book.
        /// </summary>
        /// <param name="ipAddressBookDto">The IP address book DTO.</param>
        public async Task AddIPAddressAsync(IpAddressBookDto ipAddressBookDto)
        {
            var validationResult = await _addressbookValidator.ValidateAsync(ipAddressBookDto);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            if (await IsIpExist(ipAddressBookDto))
            {
                throw new Exception("IP already exists");
            }

            IPAddress? ipAddress = _addressbookService.ValidateIpAddress(ipAddressBookDto.IP);

            if (ipAddress is null)
            {
                throw new InvalidDataException("Invalid IP address");
            }

            var ipAddressVersion = _addressbookService.GetIpAddressVersion(ipAddress);

            if (ipAddressVersion is null)
            {
                throw new InvalidDataException("Invalid IP address version");
            }

            ipAddressBookDto.Version = ipAddressVersion.Value;

            IpAddressBook? ipAddressBook;
            try
            {
                ipAddressBook = _mapper.Map<IpAddressBook>(ipAddressBookDto);
                await _addressBookRepository.AddAsync(ipAddressBook);
                await _addressBookRepository.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Removes an IP address from the address book by the IP address string.
        /// </summary>
        /// <param name="ip">The IP address string.</param>
        public async Task RemoveByIpAddressAsync(string ip)
        {
            var ipAddressBook = await _addressBookRepository.FindByIPAsync(ip);
            if (ipAddressBook is null)
            {
                throw new Exception("IP address not found");
            }

            _addressBookRepository.Remove(ipAddressBook);
        }

        /// <summary>
        /// Removes an IP address from the address book by Id.
        /// </summary>
        /// <param name="ipAddressBookDto">The IP address book DTO.</param>
        public async Task RemoveAsync(IpAddressBookDto ipAddressBookDto)
        {
            await _addressBookRepository.RemoveByIdAsync(ipAddressBookDto.Id);
        }

        /// <summary>
        /// Validate if IP address exists.
        /// </summary>
        /// <param name="ipAddressBookDto">The IP address book DTO.</param>
        /// <returns>True if found, otherwise False.</returns>
        private async Task<bool> IsIpExist(IpAddressBookDto ipAddressBookDto)
        {
            return await _addressBookRepository.FindByIPAsync(ipAddressBookDto.IP) is not null;
        }

        /// <summary>
        /// Finds an IP address in the address book by IP.
        /// </summary>
        /// <param name="ipAddressBookDto">The IP address book DTO.</param>
        /// <returns>The IP address book DTO if found, otherwise null.</returns>
        public async Task<IpAddressBookDto?> FindIpAddressAsync(IpAddressBookDto ipAddressBookDto)
        {
            var ipAddressBook = await _addressBookRepository.FindByIPAsync(ipAddressBookDto.IP);
            return _mapper.Map<IpAddressBookDto>(ipAddressBook);
        }

        /// <summary>
        /// Gets all IP addresses from the address book.
        /// </summary>
        /// <returns>The list of IP address book DTOs.</returns>
        public async Task<IEnumerable<IpAddressBookDto>?> GetAllIps()
        {
            var ipAddressList = await _addressBookRepository.GetAsync();
            return _mapper.Map<IEnumerable<IpAddressBookDto>>(ipAddressList);
        }

        /// <summary>
        /// Gets all IPv4 IP addresses from the address book.
        /// </summary>
        /// <returns>The list of IPv4 IP address book DTOs.</returns>
        public async Task<IEnumerable<IpAddressBookDto>?> GetIpV4Address()
        {
            var ipAddressList = await _addressBookRepository.GetIpV4Address();
            return _mapper.Map<IEnumerable<IpAddressBookDto>>(ipAddressList);
        }

        /// <summary>
        /// Gets IP addresses from the address book by the IP address version.
        /// </summary>
        /// <param name="version">The IP address version.</param>
        /// <returns>The list of IP address book DTOs.</returns>
        public async Task<IEnumerable<IpAddressBookDto>?> GetIpAddressByVersion(int version)
        {
            int[] allowedVersions = { 4, 6 };
            if (!allowedVersions.Contains(version))
            {
                throw new InvalidDataException("Invalid IP address version");
            }

            var ipAddress = await _addressBookRepository.GetIpsByVersionAsync(version);
            return _mapper.Map<IEnumerable<IpAddressBookDto>>(ipAddress);
        }

        /// <summary>
        /// Gets the ordered IP addresses from the address book.
        /// </summary>
        /// <param name="order">The order by enum value.</param>
        /// <returns>The list of ordered IP address book DTOs.</returns>
        public async Task<IEnumerable<IpAddressBookDto>> GetOrderedIpsAsync(OrderByEnum order)
        {
            var orderedIpAddress = await _addressBookRepository.GetOrderedIpsAsync(order.ToString());
            return _mapper.Map<IEnumerable<IpAddressBookDto>>(orderedIpAddress);
        }
    }
}
