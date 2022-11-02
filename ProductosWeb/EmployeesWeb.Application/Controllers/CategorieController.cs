using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProductsWeb.Application.Config;
using ProductsWeb.Application.Models;
using ProductsWeb.Services;
using ProductsWeb.Services.Entities;

namespace ProductsWeb.Application.Controllers
{
    public class CategorieController : Controller
    {
        private static List<Categorie> _categoriesList;
        private static List<Product> _productsList;
        private readonly CategoriesService categoriesService;
        private readonly ProductsService productsService;
        private readonly ApiConfiguration _apiConfiguration;

        public CategorieController(IOptions<ApiConfiguration> apiConfiguration)
        {
            _apiConfiguration = apiConfiguration.Value;
#pragma warning disable CS8604
            categoriesService = new CategoriesService(_apiConfiguration.ApiCategoriesUrl);

            productsService = new ProductsService(_apiConfiguration.ApiCategoriesUrl);
#pragma warning restore CS8604
        }

        // GET: EmployeeController && Pager
        // Cambiar _pager
        public async Task<ActionResult> Index()
        {
            ViewData["IsUserLogged"] = HttpContext.Session.GetString("IsUserLogged"); //prueba
            IList<CategorieDto> categories = await categoriesService.GetCategories();
            _categoriesList = categories.Select(CategorieDto => MapperToCategorie(CategorieDto)).ToList();
            return View(_categoriesList);

        }

        // GET: EmployeesController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            IList<ProductDto> pr = await productsService.GetProductsByCategory(id);
            _productsList = pr.Select(ProductDto => MapperToProduct(ProductDto)).ToList();
            
            return View(_productsList);
        }

        // GET: EmployeesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Categorie categorie)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var employeeAdded = await categoriesService.AddCategorie(MapperToCategorieDto(categorie));
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeesController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var employeeFound = await categoriesService.GetCategoriesById(id);

            if (employeeFound == null)
            {
                return NotFound();
            }

            var employee = MapperToCategorie(employeeFound);

            return View(employee);
        }

        // POST: EmployeesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Categorie categorie)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var employeeModified = await categoriesService.UpdateCategorie(MapperToCategorieDto(categorie));

                    return RedirectToAction(nameof(Index));
                }
                return View(categorie);
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeesController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var categoryDto = await categoriesService.DeleteCategorie(id);
            var employee = MapperToCategorie(categoryDto);

            ViewData["id"] = id;

            if (categoryDto == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: EmployeesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Categorie categorie)
        {
           
                var categorieDto = await categoriesService.DeleteCategorie(categorie.id);

                MapperToCategorie(categorieDto);

            

                return RedirectToAction(nameof(Index));
            
        }

        private static Categorie MapperToCategorie(CategorieDto categoriesDto)
        {
            return new Categorie
            {
                id = categoriesDto.id,
                name = categoriesDto.name,
                totalProducts = categoriesDto.totalProducts,
                
            };
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

        private static CategorieDto MapperToCategorieDto(Categorie categorie)
        {
#pragma warning disable CS8604
            return CategorieDto.Build(
              id: categorie.id,
              name: categorie.name,
              totalProducts: categorie.totalProducts
            );
#pragma warning restore CS8604
        }
    }
}