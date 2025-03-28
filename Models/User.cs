using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using Newtonsoft.Json;

namespace Contacts.Models
{
    [Table(name: "User")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Length(minimumLength: 4, maximumLength: 20, ErrorMessage = "Username must be with a minimum length of '4' and maximum length of '20'.")]
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Range(1, 120)]
        [Required(ErrorMessage = "Age is required")]
        public int Age { get; set; }

        [DataType(DataType.Password)]
        [Length(minimumLength: 4, maximumLength: 20)]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        public List<ContactBook> ContactBooks { get; set; }
        public User(string username,int age, string email, string password)
        {
            Username = username; Email = email; Password = password; Age = age;
        }

        public User()
        {
            
        }

        public override string? ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
