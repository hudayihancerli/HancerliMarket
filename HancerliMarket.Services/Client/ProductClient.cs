using HancerliMarket.DataModels.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Web;

namespace HancerliMarket.Services.Client
{
    public class ProductClient
    {
        private HttpClient _httpClient { get; set; }
        private IConfiguration _configuration { get; set; }

        public ProductClient(HttpClient httpClient, IConfiguration configuration)
        {
            _configuration = configuration;

            var apiUrl = _configuration.GetSection("apiUrl").Value.ToString();
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(apiUrl),
            };
        }

        public async Task<BaseModel> CreateProduct(ProductModel product, string jwt)
        {
            var returnModel = new BaseModel();

            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

                var query = HttpUtility.UrlDecode($"/api/Product");

                var result = await _httpClient.PostAsJsonAsync(query, product);

                returnModel.Message = "Ürün kaydedildi.";
                returnModel.Status = true;
                returnModel.Data = JsonConvert.SerializeObject(result);

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

                string url = "/api/Product";
                var result = await _httpClient.GetFromJsonAsync<List<ProductModel>>(url);

                returnModel.Message = "Ürünler getirişdi.";
                returnModel.Status = true;
                returnModel.Data = JsonConvert.SerializeObject(result);

                return returnModel;
            }
            catch (Exception ex)
            {
                returnModel.Message = $"Ürün getirme işlemi başarısız. Hata: {ex.Message}";
                returnModel.Status = false;
                returnModel.Data = null!;

                return returnModel;
            }
        }

        public async Task<BaseModel> GetDetailProduct(string barcode, string jwt)
        {
            var returnModel = new BaseModel();

            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
                var query = HttpUtility.UrlDecode($"/api/Product/detail?barcode={barcode}");

                var response = await _httpClient.GetAsync(query);

                if (!response.IsSuccessStatusCode)
                    throw new ArgumentNullException($"{nameof(response)} is null.");

                var content = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(content))
                    throw new ArgumentNullException($"{nameof(content)} is null.");

                var result = await response.Content.ReadFromJsonAsync<ProductModel>();

                if (result is null)
                    throw new ArgumentNullException($"{nameof(result)} is null.");


                returnModel.Message = "Ürün kaydedildi.";
                returnModel.Status = true;
                returnModel.Data = JsonConvert.SerializeObject(result);

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

        public async Task<BaseModel> DeleteProduct(string barcode, string jwt)
        {
            var returnModel = new BaseModel();

            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

                var query = HttpUtility.UrlDecode($"/api/Product?barcode={barcode}");
                var result = await _httpClient.DeleteAsync(query);

                returnModel.Message = "Ürün silindi.";
                returnModel.Status = true;
                returnModel.Data = JsonConvert.SerializeObject(result);

                return returnModel;
            }
            catch (Exception ex)
            {
                returnModel.Message = $"Ürün silme işlemi Başarısız. Hata: {ex.Message}";
                returnModel.Status = false;
                returnModel.Data = null!;

                return returnModel;
            }
        }

        public async Task<BaseModel> UpdateProduct(int id, ProductModel product, string jwt)
        {
            var returnModel = new BaseModel();

            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

                var query = HttpUtility.UrlDecode($"/api/Product?id={id}");

                var response = await _httpClient.PatchAsJsonAsync<ProductModel>(query, product);

                if (response is null)
                    throw new ArgumentNullException($"{nameof(response)} is null.");
                var content = await response.Content.ReadAsStringAsync();
                var result =  JsonConvert.DeserializeObject<ProductModel>(content);
                if (result is null)
                    throw new ArgumentNullException($"{nameof(result)} is null.");

                returnModel.Message = "Ürün kaydedildi.";
                returnModel.Status = true;
                returnModel.Data = JsonConvert.SerializeObject(result);

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
    }
}
