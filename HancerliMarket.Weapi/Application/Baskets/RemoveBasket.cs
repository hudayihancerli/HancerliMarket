
using HancerliMarket.Webapi.DbOperations;
using HancerliMarket.Webapi.DbOperations.Interface;

namespace HancerliMarket.Webapi.Application.Baskets
{
    public class RemoveBasket
    {
        private readonly IWebApiDbContext _dbContext;

        public RemoveBasket(IWebApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var test = _dbContext.Baskets.ToList();

            foreach (var item in test)
            {
                _dbContext.Baskets.Remove(item);
            }

            var result = _dbContext.SaveChanges();

        }
    }
}
