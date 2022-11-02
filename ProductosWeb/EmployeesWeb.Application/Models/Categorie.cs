using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsWeb.Application.Models
{    
    public class Categorie
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50)]
        public string name { get; set; }

        [Required]
        [DisplayName("Total de productos")]
        public int totalProducts { get; set; }

        public virtual List<Product> products {get; set;} = new List<Product>(); //Lista de productos
    }
}
