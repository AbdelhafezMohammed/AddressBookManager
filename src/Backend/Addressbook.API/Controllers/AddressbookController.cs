using Addressbook.Application.Enums;
using Addressbook.Application.Handlers;
using Addressbook.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Addressbook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressbookController : ControllerBase
    {
        [HttpPost("add", Name = "AddIpAddress")]
        public async Task<IActionResult> Create([FromServices] IAddressbookHandler addressbookHandler,
            IpAddressBookDto ipAddressBookDto)
        {
            try
            {
                await addressbookHandler.AddIPAddressAsync(ipAddressBookDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
                throw;
            }
            return Ok();
        }

        [HttpDelete("delete", Name = "DeleteIpAddress")]
        public async Task<IActionResult> Delete([FromServices] IAddressbookHandler addressbookHandler,
             IpAddressBookDto ipAddressBookDto)
        {
            try
            {
                await addressbookHandler.RemoveAsync(ipAddressBookDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
                throw;
            }
            return Ok();
        }

        [HttpDelete("deleteByIp", Name = "DeleteByIpAddress")]
        public async Task<IActionResult> DeleteByIpAddress([FromServices] IAddressbookHandler addressbookHandler,
             string ip)
        {
            try
            {
                await addressbookHandler.RemoveByIpAddressAsync(ip);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
                throw;
            }
            return Ok();
        }

        [HttpGet("find", Name = "FindIpAddress")]
        public async Task<IActionResult> FindIpAddress([FromServices] IAddressbookHandler addressbookHandler,
           IpAddressBookDto ipAddressBookDto)
        {
            IpAddressBookDto? addressBook;
            try
            {
                addressBook = await addressbookHandler.FindIpAddressAsync(ipAddressBookDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
                throw;
            }
            return Ok(addressBook);
        }

        [HttpGet("GetAll",Name = "GetAllIpAddress")]
        public async Task<IActionResult> GetAll([FromServices] IAddressbookHandler addressbookHandler)
        {
            IEnumerable<IpAddressBookDto>? ipAddressBookDto;
            try
            {
                ipAddressBookDto = await addressbookHandler.GetAllIps();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
                throw;
            }
            return Ok(ipAddressBookDto);
        }

        [HttpGet("getIpV4Address", Name = "GetIpV4Address")]
        public async Task<IActionResult> GetIpV4Address([FromServices] IAddressbookHandler addressbookHandler)
        {
            IEnumerable<IpAddressBookDto>? ipAddressBookDto;
            try
            {
                ipAddressBookDto = await addressbookHandler.GetIpV4Address();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
                throw;
            }
            return Ok(ipAddressBookDto);
        }

        [HttpGet("getIpByVersion", Name = "GetIpAddressByVersion")]
        public async Task<IActionResult> GetIpAddressByVersion([FromServices] IAddressbookHandler addressbookHandler,
            int version)
        {
            IEnumerable<IpAddressBookDto>? ipAddressBookDto;
            try
            {
                ipAddressBookDto = await addressbookHandler.GetIpAddressByVersion(version);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
                throw;
            }
            return Ok(ipAddressBookDto);
        }

        [HttpGet("getSortedIps", Name = "GetIpAddressSorted")]
        public async Task<IActionResult> GetIpAddressSortedBy([FromServices] IAddressbookHandler addressbookHandler,
        OrderByEnum orderBy)
        {
            IEnumerable<IpAddressBookDto>? sortedIpAddressBookDto;
            try
            {
                sortedIpAddressBookDto = await addressbookHandler.GetOrderedIpsAsync(orderBy);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
                throw;
            }
            return Ok(sortedIpAddressBookDto);
        }
    }
}
