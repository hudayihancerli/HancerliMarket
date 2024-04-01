using HancerliMarket.DataModels.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HancerliMarket.Webapi.TokenOperations;

public class TokenHandler
{
    private readonly IConfiguration _configuration;

    public required UserModel Model { get; set; }
    public required List<Claim> Claims { get; set; }
    public TokenHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public TokenModel CreateAccessToken()
    {
        TokenModel tokenModel = new();

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Token:SecurityKey").Value!));

        SigningCredentials singningCredentials = new(key, SecurityAlgorithms.HmacSha256);

        tokenModel.Expiretion = DateTime.Now.AddHours(4);

        JwtSecurityToken securityToken = new(
                claims: Claims,
                issuer: _configuration["Token:Issuer"],
                audience: _configuration["Token:Audience"],
                expires: tokenModel.Expiretion,
                notBefore: DateTime.Now,
                signingCredentials: singningCredentials
            );

        tokenModel.AccesToken = new JwtSecurityTokenHandler().WriteToken(securityToken);

        tokenModel.Refreshtoken = CreateRefreshToken();

        //var cookieOptions = new CookieOptions
        //{
        //    HttpOnly = true,
        //    Expires = tokenModel.Expiretion
        //};

        //_httpResponse.Cookies.Append("refreshToken", tokenModel.Refreshtoken, cookieOptions);

        return tokenModel;
    }

    public string CreateRefreshToken()
    {
        return Guid.NewGuid().ToString();
    }

}
