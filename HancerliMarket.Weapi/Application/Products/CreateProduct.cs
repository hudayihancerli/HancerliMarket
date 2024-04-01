using HancerliMarket.DataModels.Models;
using HancerliMarket.Webapi.DbOperations.Interface;

namespace HancerliMarket.Webapi.Application.Products
{
    public class CreateProduct
    {
        private readonly IWebApiDbContext _dbContext;

        public required ProductModel Product { get; set; }
        public CreateProduct(IWebApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ProductModel Handle()
        {
            if (Product.Barcode == string.Empty)
                throw new Exception("Barkod yanlış.");

            var products = _dbContext.Products.FirstOrDefault(x => x.Barcode == Product.Barcode);

            if (products is not null)
                throw new Exception("ürün daha önce kaydedilmiş.");

            _dbContext.Products.Add(Product);

            var result = _dbContext.SaveChanges();

            if (result < 1)
                throw new Exception("Kaydedilemedi.");

            return products;
        }
    }
}
