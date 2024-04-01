using HancerliMarket.DataModels.Models;
using HancerliMarket.Webapi.DbOperations.Interface;

namespace HancerliMarket.Webapi.Application.Baskets
{
    public class CreateBasket
    {
        private readonly IWebApiDbContext _dbContext;

        public required BasketModel Basket { get; set; }
        public CreateBasket(IWebApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<BasketModel> Handle()
        {
            _dbContext.Baskets.Add(Basket);

            var result = _dbContext.SaveChanges();

            if (result != 1)
                throw new Exception("Kaydedilemedi.");

            var basketList = _dbContext.Baskets.ToList();

            return basketList;
        }
    }
}
