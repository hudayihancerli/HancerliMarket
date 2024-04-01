using HancerliMarket.Webapi.DbOperations.Interface;

namespace HancerliMarket.Webapi.Application.Baskets
{
    public class DeleteBasket
    {
        private readonly IWebApiDbContext _dbContext;

        public required int Id { get; set; }
        public DeleteBasket(IWebApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            if (Id < 0)
                throw new Exception("Barkod yanlış.");

            var baskets = _dbContext.Baskets.FirstOrDefault(x => x.Id == Id);

            if (baskets is null)
                throw new Exception("Ürün bulunamadi.");

            _dbContext.Baskets.Remove(baskets);

            var result = _dbContext.SaveChanges();

            if (result != 1)
                throw new Exception("Ürün silinemedi.");
        }
    }
}
