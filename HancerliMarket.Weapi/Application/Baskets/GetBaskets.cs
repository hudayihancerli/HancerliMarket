using HancerliMarket.DataModels.Models;
using HancerliMarket.Webapi.DbOperations.Interface;

namespace HancerliMarket.Webapi.Application.Baskets
{
    public class GetBaskets
    {
        private readonly IWebApiDbContext _dbContext;

        public GetBaskets(IWebApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<BasketModel> Handle()
        {
            var products = _dbContext.Baskets.ToList();

            if (products is null)
                throw new Exception("ürünler bulunamadı.");

            return products;
        }
    }
}
