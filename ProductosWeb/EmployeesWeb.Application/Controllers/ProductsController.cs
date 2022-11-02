using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProductsWeb.Application.Config;
using ProductsWeb.Application.Models;
using ProductsWeb.Services;
using ProductsWeb.Services.Entities;
using System.Xml.Linq;

namespace ProductsWeb.Application.Controllers
{
    public class ProductsController : Controller
    {
        private static List<Categorie> _categoriesList;
        private static List<Product> _productsList;
        private readonly CategoriesService categoriesService;
        private readonly ProductsService productsService;
        private readonly ApiConfiguration _apiConfiguration;

        public ProductsController(IOptions<ApiConfiguration> apiConfiguration)
        {
            _apiConfiguration = apiConfiguration.Value;
#pragma warning disable CS8604

            productsService = new ProductsService(_apiConfiguration.ApiProdcutsUrl);
#pragma warning restore CS8604
        }
        // GET: ProductsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ProductsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var employeeAdded = await productsService.AddCategorie(MapperToProductDto(product));
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductsController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit(Product product)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var employeeModified = await productsService.AddCategorie(MapperToProductDto(product));

        //            return RedirectToAction(nameof(Index));
        //        }
        //        return View(categorie);
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: ProductsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        private static Product MapperToProduct(ProductDto productDto)
        {
            return new Product
            {
                id = productDto.id,
                categoriaId = productDto.categoriaId,
                name = productDto.name,
                price = productDto.price,
                units = productDto.units
            };
        }

        private static ProductDto MapperToProductDto(Product product)
        {
#pragma warning disable CS8604
            return ProductDto.Build(
              id: product.id,
              categoriaId: product.categoriaId,
              name: product.name,
              price: product.price,
              units: product.units              
            );
#pragma warning restore CS8604
        }
    }
}
