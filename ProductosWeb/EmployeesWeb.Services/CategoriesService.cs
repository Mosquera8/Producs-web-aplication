using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ProductsWeb.Services.Entities;
using System.Net.Http.Headers;
using System.Text;

namespace ProductsWeb.Services
{
    public class CategoriesService
    {
        private readonly HttpClient _httpClient;

        public CategoriesService(string baseUrl)
        {

            _httpClient = new HttpClient();

            SetupHttpConnection(_httpClient, baseUrl);
        }

        private static void SetupHttpConnection(HttpClient httpClient, string baseUrl)
        {
            //Passing service base url
            httpClient.BaseAddress = new Uri(baseUrl);

            httpClient.DefaultRequestHeaders.Clear();

            //Define request data format
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("categories/json"));
        }

        public async Task<List<CategorieDto>> GetCategories()
        {
            var categoriesList = new List<CategorieDto>();

            // Sending request to find web api REST service resource to Get All Users using HttpClient
            HttpResponseMessage response = await _httpClient.GetAsync("/categorias");

            // Checking the response is successful or not which is sent using HttpClient
            if (response.IsSuccessStatusCode)
            {
                // Storing the content response recieved from web api
                var responseContent = response.Content.ReadAsStringAsync().Result;

                //Deserializing the response recieved from web api and storing into the Employee list
                categoriesList = JsonConvert.DeserializeObject<List<CategorieDto>>(responseContent);
            }
#pragma warning disable CS8603
            return categoriesList;
        }

        public async Task<CategorieDto> GetCategoriesById(int id)
        {
            CategorieDto? categorie = null;

            // Sending request to find web api REST service resource to Get All Users using HttpClient
            HttpResponseMessage response = await _httpClient.GetAsync($"/categorias/{id}");

            // Checking the response is successful or not which is sent using HttpClient
            if (response.IsSuccessStatusCode)
            {
                // Storing the content response recieved from web api
                var responseContent = response.Content.ReadAsStringAsync().Result;

                //Deserializing the response recieved from web api and storing into the Employee list
                categorie = JsonConvert.DeserializeObject<CategorieDto>(responseContent);
            }
#pragma warning disable CS8603
            return categorie;
        }

        public async Task<CategorieDto> AddCategorie(CategorieDto categorie)
        {
            CategorieDto? categoriesDtoResponse = null;

            StringContent content = new StringContent(JsonConvert.SerializeObject(categorie), Encoding.UTF8, "application/json");

            // Sending request to find web api REST service resource to Add an User using HttpClient
            HttpResponseMessage response = await _httpClient.PostAsync($"/categorias", content);

            // Checking the response is successful or not which is sent using HttpClient
            if (response.IsSuccessStatusCode)
            {
                // Storing the content response recieved from web api
                var responseContent = response.Content.ReadAsStringAsync().Result;

                //Deserializing the response recieved from web api and storing into the Employee list
                categoriesDtoResponse = JsonConvert.DeserializeObject<CategorieDto>(responseContent);
            }
#pragma warning disable CS8603
            return categoriesDtoResponse;
        }

        public async Task<CategorieDto> UpdateCategorie(CategorieDto categorie)
        {
            CategorieDto? categorieDtoResponse = null;

            StringContent content = new StringContent(JsonConvert.SerializeObject(categorie), Encoding.UTF8, "application/json");

            // Sending request to find web api REST service resource to Add an User using HttpClient
            HttpResponseMessage response = await _httpClient.PutAsync($"/categorias/{categorie.id}", content);

            // Checking the response is successful or not which is sent using HttpClient
            if (response.IsSuccessStatusCode)
            {
                // Storing the content response recieved from web api
                var responseContent = response.Content.ReadAsStringAsync().Result;

                //Deserializing the response recieved from web api
                categorieDtoResponse = JsonConvert.DeserializeObject<CategorieDto>(responseContent);
            }
#pragma warning disable CS8603
            return categorieDtoResponse;
        }

        public async Task<CategorieDto> DeleteCategorie(int id)
        {
            CategorieDto? categorieDtoResponse = null;

            // Sending request to find web api REST service resource to Delete the User using HttpClient
            HttpResponseMessage response = await _httpClient.DeleteAsync($"/categorias/{id}");

            // Checking the response is successful or not which is sent using HttpClient
            if (response.IsSuccessStatusCode)
            {
                // Storing the content response recieved from web api
                var responseContent = response.Content.ReadAsStringAsync().Result;

                // Deserializing the response recieved from web api
                categorieDtoResponse = JsonConvert.DeserializeObject<CategorieDto>(responseContent);
            }
#pragma warning disable CS8603
            return categorieDtoResponse;
        }
    }
}