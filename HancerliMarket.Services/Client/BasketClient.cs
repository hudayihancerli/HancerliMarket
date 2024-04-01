using HancerliMarket.DataModels;
using HancerliMarket.DataModels.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Web;

namespace HancerliMarket.Services.Client
{
    public class BasketClient
    {
        private HttpClient _httpClient { get; set; }
        private IConfiguration _configuration { get; set; }

        public BasketClient(HttpClient httpClient, IConfiguration configuration)
        {
            _configuration = configuration;

            var apiUrl = _configuration.GetSection("apiUrl").Value.ToString();
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(apiUrl),
            };
        }
        public async Task<BaseModel> CreateProduct(BasketModel product, string jwt)
        {
            var returnModel = new BaseModel();

            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
                var query = HttpUtility.UrlDecode($"/api/Basket");

                var response = await _httpClient.PostAsJsonAsync(query, product);

                if (!response.IsSuccessStatusCode)
                    throw new ArgumentNullException($"{nameof(response)} is null.");

                var content = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(content))
                    throw new ArgumentNullException($"{nameof(content)} is null.");

                var result = await response.Content.ReadFromJsonAsync<List<BasketModel>>();

                if (result is null)
                    throw new ArgumentNullException($"{nameof(result)} is null.");

                returnModel.Message = "Ürün kaydedildi.";
                returnModel.Status = true;
                returnModel.Data =  JsonConvert.SerializeObject(result);

                return returnModel;
            }
            catch (Exception ex)
            {
                returnModel.Message = $"Ürün Kayıtı Başarısız. Hata: {ex.Message}";
                returnModel.Status = false;
                returnModel.Data = null!;

                return returnModel;
            }
        }

        public async Task<BaseModel> GetAllProduct(string jwt)
        {
            var returnModel = new BaseModel();

            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
                string query = HttpUtility.UrlDecode($"/api/Basket");

                var response = await _httpClient.GetAsync(query);

                if (!response.IsSuccessStatusCode)
                    throw new ArgumentNullException($"{nameof(response)} is null.");

                var content = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(content))
                    throw new ArgumentNullException($"{nameof(content)} is null.");

                var result = await response.Content.ReadFromJsonAsync<List<BasketModel>>();

                if (result is null)
                    throw new ArgumentNullException($"{nameof(result)} is null.");

                returnModel.Message = "Ürünler Getirildi.";
                returnModel.Status = true;
                returnModel.Data = JsonConvert.SerializeObject(result);

                return returnModel;
            }
            catch (Exception ex)
            {
                returnModel.Message = $"Ürünler getirilemedi. Hata: {ex.Message}";
                returnModel.Status = false;
                returnModel.Data = null!;

                return returnModel;
            }
        }

        public async Task<BaseModel> DeleteProduct(int id, string jwt)
        {
            var returnModel = new BaseModel();

            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

                var query = HttpUtility.UrlDecode($"/api/Basket?id={id}");

                var response = await _httpClient.DeleteAsync(query);

                if (!response.IsSuccessStatusCode)
                    throw new ArgumentNullException($"{nameof(response)} is null.");

                returnModel.Status = true;
                returnModel.Message = "Ürün silindi";
                returnModel.Data = null!;
                return returnModel;
            }
            catch (Exception ex)
            {
                returnModel.Message = $"Ürün silinemedi. Hata: {ex.Message}";
                returnModel.Status = false;
                returnModel.Data = null!;
                return returnModel;
            }

        }

        public async Task<BaseModel> RemoveProduct(string jwt)
        {
            var returnModel = new BaseModel();
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

                var query = HttpUtility.UrlDecode($"/api/Basket/all");

                var response = await _httpClient.DeleteAsync(query);

                if (!response.IsSuccessStatusCode)
                {
                    throw new ArgumentNullException($"{nameof(response)} is null.");
                }

                returnModel.Status = true;
                returnModel.Message = "Ürünler silindi";
                returnModel.Data = null!;
                return returnModel;
            }
            catch (Exception ex)
            {
                returnModel.Message = $"Ürünler silinemedi. Hata: {ex.Message}";
                returnModel.Status = false;
                returnModel.Data = null!;
                return returnModel;
            }
        }
    }
}
