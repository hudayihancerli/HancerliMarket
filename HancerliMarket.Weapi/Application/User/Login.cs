using HancerliMarket.DataModels.Models;
using HancerliMarket.Webapi.DbOperations.Interface;
using HancerliMarket.Webapi.TokenOperations;
using System.Security.Claims;

namespace HancerliMarket.Webapi.Application.User
{
    public class Login
    {

        private readonly IWebApiDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public required UserModel Model { get; set; }

        public Login(IWebApiDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public TokenModel Handle()
        {
            if (Model is null)
                throw new Exception("kullanici adi veya sifre yanlis.");

            if (Model.Username is null || Model.Email is null || Model.Password is null)
                throw new InvalidOperationException("Lutfen Kullanici adi, eposta veya sifrenizi bos birakmayiniz.");

            var user = _dbContext.Users.FirstOrDefault(x => x.Email == Model.Email || x.Username == Model.Username);

            if (user is null)
                throw new Exception("kullanici adi veya sifre yanlis.");

            if (!BCrypt.Net.BCrypt.Verify(Model.Password, user.Password))
                throw new Exception("kullanici adi veya sifre yanlis.");


            TokenHandler tokenHandler = new(_configuration)
            {
                Model = Model,
                Claims =
                [
                        new Claim(ClaimTypes.Name, Model.Username),
                        new Claim(ClaimTypes.Email, Model.Email),
                        new Claim(ClaimTypes.Role, user.Roles)
                ]
            };

            TokenModel tokenModel = tokenHandler.CreateAccessToken();

            user.RefreshToken = tokenModel.Refreshtoken;
            user.RefreshTokenExiprationDate = tokenModel.Expiretion.AddHours(24);

            _dbContext.SaveChanges();

            return tokenModel;
        }
    }
}