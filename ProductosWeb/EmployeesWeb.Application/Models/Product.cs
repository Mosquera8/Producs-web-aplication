using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsWeb.Application.Models
{
    public class Product
    {

        public Product()
        {
        }

        [Key]
        public int id { get; set; }

        [ForeignKey("categoriaId")]
        public int categoriaId { get; set; }
        public virtual Categorie categorie { get; private set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string name { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio")]
        public int price { get; set; }

        [Required(ErrorMessage = "Las unidades son obligatorias")]
        public int units { get; set; }

        [NotMapped]
        public bool IsDeleted { get; set; }
    }
}