using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using ProductsWeb.Services.Entities;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsWeb.Services
{
    public class RegisterService
    {
        private readonly RestClient _restClient;
        private string BaseUrl { get; }
        public RegisterService(string baseUrl)
        {
            BaseUrl = baseUrl;
            _restClient = new RestClient();
        }
        public async Task<AuthorizationDto> ValidUserRegister(LoginDto login)
        {
            // Assign the URL
            var targetUrl = new Uri($"{BaseUrl}/auth/register");

            // Assign the Method Type
            var request = new RestRequest(targetUrl, Method.Post);

            // Assign the Body
            var content = SerializeContentToJsonString(login);
            // Revisar Parametro aplication
            request.AddParameter("application/json", content, ParameterType.RequestBody);

            // Execute the Call
            RestResponse response = await _restClient.ExecuteAsync(request);

            // If there was a communication error the content is empty
            // otherwise the content could be success or error response
            var responseData = response.Content ?? String.Empty;

            // Checking the response is successful or not which is sent using HttpClient
            if (!response.IsSuccessful || !response.IsSuccessStatusCode)
            {
                if (response.ErrorException is not null)
                {
                    throw response.ErrorException;
                }

                throw new ApplicationException($"Error: {responseData}");
            }

            // Deserializing the response recieved from web api and storing into the Employee list
            AuthorizationDto authorizationDto = JsonConvert.DeserializeObject<AuthorizationDto>(responseData);

            return authorizationDto;
        }

        private string SerializeContentToJsonString(object content)
        {
            return JsonConvert.SerializeObject(content, new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }
    }
}
