using HancerliMarket.DataModels.Models;
using HancerliMarket.Webapi.DbOperations.Interface;

namespace HancerliMarket.Webapi.Application.User
{
    public class GetUserDetail
    {
        private readonly IWebApiDbContext _dbContext;

        public required string Username { get; set; }
        public GetUserDetail(IWebApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public UserModel Handle()
        {
            if (Username == string.Empty)
                throw new Exception("kullanici adi veya sifre yanlis.");

            var user = _dbContext.Users.FirstOrDefault(x => x.Username == Username);

            if (user is null)
                throw new Exception("kullanici detayi bulunamadi.");

            return user;
        }
    }
}
