using HancerliMarket.DataModels.Models;
using Microsoft.EntityFrameworkCore;

namespace HancerliMarket.Webapi.DbOperations.Interface
{
    public interface IWebApiDbContext
    {
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<BasketModel> Baskets { get; set; }
        public DbSet<UserModel> Users { get; set; }

        public int SaveChanges();
        
    }
}