using HancerliMarket.DataModels.Models;
using HancerliMarket.Webapi.DbOperations.Interface;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Org.BouncyCastle.Crypto;

namespace HancerliMarket.Webapi.Application.User
{
    public class Register
    {
        private readonly IWebApiDbContext _dbContext;

        public required UserModel Model { get; set; }

        public Register(IWebApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public UserModel Handle()
        {

            if (Model.Username is null || Model.Email is null || Model.Password is null)
                throw new InvalidOperationException("Lutfen Kullanici adi, eposta ve sifrenizi bos birakmayiniz.");

            if (Model.Username.Length < 2 || Model.Password.Length < 8)
                throw new InvalidOperationException("Lutfen Kullanici adinızın uzunluğu 2, sifrenizi uzunlugu 8 karakterden buyuk olsun.");

            var user = _dbContext.Users.FirstOrDefault(user => user.Username == Model.Username || user.Email == Model.Email);

            if (user is not null)
                throw new InvalidOperationException("Kullanici zaten mevcut.");

            Model.Password = BCrypt.Net.BCrypt.HashPassword(Model.Password);

            Model.Roles = Model.Roles == string.Empty ? "User" : Model.Roles;

            _dbContext.Users.Add(Model);

            var resultdb = _dbContext.SaveChanges();

            if (resultdb == 1)
            {
                return user;
            }

            throw new ArgumentException("Kullancı kayıt edilemedi");
        }
    }
}
