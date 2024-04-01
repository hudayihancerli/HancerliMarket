using HancerliMarket.DataModels.Models;
using HancerliMarket.Webapi.DbOperations.Interface;
using System.Security.Claims;
namespace HancerliMarket.Webapi.TokenOperations;

public class RefreshTokenOperation
{

    private readonly IWebApiDbContext _dbContext;
    private readonly IConfiguration _configuration;

    public required string refreshToken { get; set; }
    public RefreshTokenOperation(IWebApiDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
    }

    public TokenModel Handle()
    {
        var user = _dbContext.Users.FirstOrDefault(x => x.RefreshToken == refreshToken && x.RefreshTokenExiprationDate >= DateTime.Now);

        if (user == null)
            throw new InvalidOperationException("valid refresh token bulunamadi");

        TokenHandler handler = new(_configuration)
        {
            Model = user,
            Claims = new List<Claim>
                {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, user.Roles)
                }
        };

        TokenModel tokenModel = handler.CreateAccessToken();

        user.RefreshToken = tokenModel.Refreshtoken;
        user.RefreshTokenExiprationDate = tokenModel.Expiretion.AddMinutes(360);

        _dbContext.SaveChanges();

        return tokenModel;
    }
}
