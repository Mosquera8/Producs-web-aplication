using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProductsWeb.Services.Entities
{
    public class ProductDto
    {
        public int id { get; set; }

        public int categoriaId { get; set; }

        public string? name { get; set; }

        public int price { get; set; }

        public int units { get; set; }


        //public CategorieDto categorie { get; private set; }

        private ProductDto()
        {

        }

        public static ProductDto Build(int id, int categoriaId, string name, int price, int units)
        {
            return new ProductDto
            {
                id = id,
                categoriaId = categoriaId,
                name = name,
                price = price,
                units = units
            };
        }
    }
}
