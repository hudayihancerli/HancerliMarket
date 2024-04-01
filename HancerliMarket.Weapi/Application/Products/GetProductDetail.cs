using HancerliMarket.Webapi.DbOperations.Interface;
using HancerliMarket.DataModels.Models;

namespace HancerliMarket.Webapi.Application.Products
{
    public class GetProductDetail
    {
        private readonly IWebApiDbContext _dbContext;

        public required string Barcode { get; set; }
        public GetProductDetail(IWebApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ProductModel Handle()
        {
            if (Barcode == string.Empty)
                throw new Exception("Barkod yanlış.");

            var products = _dbContext.Products.FirstOrDefault(x => x.Barcode == Barcode);

            if (products is null)
                throw new Exception("ürün detayi bulunamadi.");

            return products;
        }
    }
}
