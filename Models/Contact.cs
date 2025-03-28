using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contacts.Models
{
    [Table("Contact")]
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public DateTime DateOfCreate { get; set; }
        public int ContactBookId { get; set; }
        public ContactBook? ContactBook { get; set; }
    }
}
