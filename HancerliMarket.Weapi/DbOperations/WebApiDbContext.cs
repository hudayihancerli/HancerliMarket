using HancerliMarket.DataModels.Models;
using HancerliMarket.Webapi.DbOperations.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Configuration;

namespace HancerliMarket.Webapi.DbOperations
{
    public class WebApiDbContext : DbContext, IWebApiDbContext
    {
        public IConfiguration _configuration;
        public WebApiDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(_configuration.GetValue<string>("ConnectionStrings:default") ?? throw new ArgumentNullException("connection null."));
        }

       public DbSet<ProductModel> Products { get; set; }
       public DbSet<BasketModel> Baskets { get; set; }
       public DbSet<UserModel> Users { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
