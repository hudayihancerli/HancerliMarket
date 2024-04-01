using HancerliMarket.Webapi.DbOperations.Interface;

namespace HancerliMarket.Webapi.Application.Products
{
    public class DeleteProduct
    {
        private readonly IWebApiDbContext _dbContext;

        public required string Barcode { get; set; }
        public DeleteProduct(IWebApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            if (Barcode == string.Empty)
                throw new Exception("Barkod yanlış.");

            var product = _dbContext.Products.FirstOrDefault(x => x.Barcode == Barcode);

            if (product is null)
                throw new Exception("Ürün bulunamadi.");

            _dbContext.Products.Remove(product);

            var result = _dbContext.SaveChanges();

            if (result != 1)
                throw new Exception("Ürün silinemedi.");
        }
    }
}
