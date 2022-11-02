using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProductsWeb.Services.Entities
{
    public class CategorieDto
    {
        public int id { get; set; }
        public string? name { get; set; }
        public int totalProducts { get; set; }

        public List<string> productDto { get; set; } = new List<String>();

        private CategorieDto()
        {

        }

        public static CategorieDto Build(int id, string name, int totalProducts)
        {
            return new CategorieDto
            {
                id = id,
                name = name,                
                totalProducts = totalProducts
            };
        }
    }
}
