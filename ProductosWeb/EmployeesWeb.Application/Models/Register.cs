using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ProductsWeb.Application.Models
{
    public class Register
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
