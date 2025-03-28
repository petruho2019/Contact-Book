using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contacts.Models
{
    [Table("ContactBook")]
    public class ContactBook
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateTime DateOfCreate { get; set; }
        public int UserId { get; set; }

        public required User User;
        public List<Contact>? Contacts { get; set; } 
    }
}
