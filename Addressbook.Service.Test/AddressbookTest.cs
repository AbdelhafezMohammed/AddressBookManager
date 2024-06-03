using Addressbook.Service.Services;
using Moq;
using System.Net;

namespace Addressbook.Service.Test
{
    [TestClass]
    public class AddressbookTest
    {
        [TestMethod]
        public void ValidateIpV4_Success()
        {
            // Arrange
            string ipAddress = "192.168.1.1";
            byte[] ipv4 = { 192, 168, 1, 1 };
            IPAddress address = new IPAddress(ipv4);

            // Act
            var service = new AddressbookService();
            var addressResult = service.ValidateIpAddress(ipAddress);
            // Assert
            Assert.AreEqual(addressResult.ToString(), ipAddress);
        }

        [TestMethod]
        public void ValidateIpV6_Success()
        {
            // Arrange
            string ipAddress = "591f:9930:9294:a1fb:2802:b95f:af8f:bac0";
            byte[] ipv6Bytes =
            {
                 0x59, 0x1f, 0x99, 0x30, 0x92, 0x94, 0xa1, 0xfb,
                    0x28, 0x02, 0xb9, 0x5f, 0xaf, 0x8f, 0xba, 0xc0
            };
            IPAddress address = new IPAddress(ipv6Bytes);

            // Act
            var service = new AddressbookService();
            var addressResult = service.ValidateIpAddress(ipAddress);
            // Assert
            Assert.AreEqual(addressResult.ToString(), ipAddress);
        }

        [TestMethod]
        public void ValidateEmptyIpV4_Success()
        {
            // Arrange
            byte[] ipv4 = { 192, 168, 1, 1 };
            IPAddress address = new IPAddress(ipv4);

            // Act
            var service = new AddressbookService();
            var addressResult = service.ValidateIpAddress("");

            // Assert
            Assert.IsNull(addressResult);
        }

        [TestMethod]
        public void ValidateInvalidIpV4_Success()
        {
            // Arrange
            string invalidIpAddress = "256.256.256.256";

            // Act
            var service = new AddressbookService();
            var addressResult = service.ValidateIpAddress(invalidIpAddress);

            // Assert
            Assert.IsNull(addressResult);
        }

        [TestMethod]
        public void TestIpV4Version_Success()
        {
            // Arrange
            byte[] ipv4 = { 192, 168, 1, 1 };
            IPAddress ipAddress = new IPAddress(ipv4);

            // Act
            var service = new AddressbookService();
            var addressVersion = service.GetIpAddressVersion(ipAddress);
            // Assert
            Assert.AreEqual(4, addressVersion);
        }

        [TestMethod]
        public void TestIpV6_Success()
        {
            // Arrange
            byte[] ipv6Bytes =
            {
                 0x59, 0x1f, 0x99, 0x30, 0x92, 0x94, 0xa1, 0xfb,
                    0x28, 0x02, 0xb9, 0x5f, 0xaf, 0x8f, 0xba, 0xc0
            };
            IPAddress ipAddress = new IPAddress(ipv6Bytes);

            // Act
            var service = new AddressbookService();
            var addressVersion = service.GetIpAddressVersion(ipAddress);
            // Assert
            Assert.AreEqual(6, addressVersion);
        }
    }
}