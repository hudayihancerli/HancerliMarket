using HancerliMarket.Webapi.DbOperations.Interface;
using HancerliMarket.DataModels.Models;

namespace HancerliMarket.Webapi.Application.Products
{
    public class GetProducts
    {
        private readonly IWebApiDbContext _dbContext;

        public GetProducts(IWebApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<ProductModel> Handle()
        {
            var products = _dbContext.Products.ToList();

            if (products is null)
                throw new Exception("ürünler bulunamadı.");

            return products;
        }
    }
}
