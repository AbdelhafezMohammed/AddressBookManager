using System.Text.Json.Serialization;

namespace Addressbook.Models.Dtos
{
    public class IpAddressBookDto
    {
        public int Id { get; set; }
        public string IP { get; set; }

        [JsonIgnore]
        public int Version { get; set; }
    }
}
