namespace HancerliMarket.DataModels.Models;

public class TokenModel
{
    public string AccesToken { get; set; } = string.Empty;
    public DateTime Expiretion { get; set; } = DateTime.Now;
    public string Refreshtoken { get; set; } = string.Empty;
}
