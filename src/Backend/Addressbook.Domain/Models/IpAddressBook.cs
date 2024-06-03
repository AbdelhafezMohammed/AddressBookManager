using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Addressbook.Domain.Models
{
    public class IpAddressBook
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string IP { get; set; }
        public int Version { get; set; }
        public DateTime Timestamp { get; set; }
        public string? Device { get; set; }
        public string? MacAddress { get; set; }
        public string? Location { get; set; }
    }
}
