using HancerliMarket.DataModels.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Json;
using System.Web;

namespace HancerliMarket.Services.Client
{
    public class UserClient
    {
        private HttpClient _httpClient { get; set; }
        private IConfiguration _configuration { get; set; }

        public UserClient(HttpClient httpClient, IConfiguration configuration)
        {
            _configuration = configuration;

            var apiUrl = _configuration.GetSection("apiUrl").Value.ToString();
            _httpClient = new HttpClient { BaseAddress = new Uri(apiUrl) };
        }

        public async Task<UserModel> Register(UserModel user)
        {
            var query = HttpUtility.UrlDecode($"/api/Users/Register");

            var result = await _httpClient.PostAsJsonAsync(query, user);

            return user;

        }

            public async Task<TokenModel> Login(UserModel user)
            {
                var query = HttpUtility.UrlDecode($"/api/Users/Login");

                var result = await _httpClient.PostAsJsonAsync<UserModel>(query, user);

                return await result.Content.ReadAsAsync<TokenModel>();
            }
    }
}
