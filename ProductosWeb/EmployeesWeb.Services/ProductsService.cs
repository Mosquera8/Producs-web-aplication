using Newtonsoft.Json;
using ProductsWeb.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace ProductsWeb.Services
{
    public class ProductsService
    {
        private readonly HttpClient _httpClient;
        public ProductsService(string baseUrl)
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


        public async Task<ProductDto> AddCategorie(ProductDto product)
        {
            ProductDto? productsDtoResponse = null;

            StringContent content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

            // Sending request to find web api REST service resource to Add an User using HttpClient

            HttpResponseMessage findCategory = await _httpClient.GetAsync($"/categorias/{product.categoriaId}");

            HttpResponseMessage response = await _httpClient.PostAsync($"/productos", content);

            

            // Checking the response is successful or not which is sent using HttpClient
            if (response.IsSuccessStatusCode && findCategory != null)
            {
                // Storing the content response recieved from web api
                var responseContent = response.Content.ReadAsStringAsync().Result;

                //Deserializing the response recieved from web api and storing into the Employee list
                productsDtoResponse = JsonConvert.DeserializeObject<ProductDto>(responseContent);
            }else
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

#pragma warning disable CS8603
            return productsDtoResponse;
        }


        public async Task<List<ProductDto>> GetProductsByCategory(int id)
        {
            List<ProductDto>? product = null;

            // Sending request to find web api REST service resource to Get All Users using HttpClient
            HttpResponseMessage response = await _httpClient.GetAsync($"/productos/?categoriaId={id}");

            // Checking the response is successful or not which is sent using HttpClient
            if (response.IsSuccessStatusCode)
            {
                // Storing the content response recieved from web api
                var responseContent = response.Content.ReadAsStringAsync().Result;

                //Deserializing the response recieved from web api and storing into the Employee list               
                product = JsonConvert.DeserializeObject<List<ProductDto>>(responseContent);

               
            }
            return product;
#pragma warning disable CS8603
            
        }
    }
}
