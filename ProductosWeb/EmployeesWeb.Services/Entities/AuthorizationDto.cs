using Newtonsoft.Json;

namespace ProductsWeb.Services.Entities
{
    public class AuthorizationDto
    {
        [JsonProperty("access_token")]
        public string? AccessToken { get; set; }
    }
}