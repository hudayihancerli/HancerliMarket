using HancerliMarket.Webapi.DbOperations.Interface;
using HancerliMarket.DataModels.Models;

namespace HancerliMarket.Webapi.Application.Products
{
    public class UpdateProduct
    {
        private readonly IWebApiDbContext _dbContext;

        public required int Id { get; set; }
        public required ProductModel Model { get; set; }
        public UpdateProduct(IWebApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ProductModel Handle()
        {
            if (Id < 0)
                throw new Exception("Barkod yanlış.");

            var product = _dbContext.Products.FirstOrDefault(x => x.Id == Id);

            if (product is null)
                throw new Exception("ürün detayi bulunamadi.");

            product.Barcode = Model.Barcode == string.Empty ? product.Barcode : Model.Barcode;
            product.Name = Model.Name == string.Empty ? product.Name : Model.Name;
            product.Price = Model.Price == 0 ? product.Price : Model.Price;

            _dbContext.Products.Update(product);

            var result = _dbContext.SaveChanges();

            if (result != 1)
                throw new Exception("ürün detayi bulunamadi.");

            return product;
        }
    }
}
