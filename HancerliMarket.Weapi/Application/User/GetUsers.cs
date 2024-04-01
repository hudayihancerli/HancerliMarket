using HancerliMarket.DataModels.Models;
using HancerliMarket.Webapi.DbOperations.Interface;

namespace HancerliMarket.Webapi.Application.User
{
    public class GetUsers
    {
        private readonly IWebApiDbContext _dbContext;

        public GetUsers(IWebApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<UserModel> Handle()
        {

            var userList = _dbContext.Users.ToList();

            if (userList.Count <= 0)
                throw new Exception("Kullanıcı listesi boş.");

            return userList;
        }
    }
}
