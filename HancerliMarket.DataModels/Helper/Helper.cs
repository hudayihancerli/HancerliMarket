using Newtonsoft.Json.Linq;
using System.Text;

namespace HancerliMarket.DataModels.Helper
{
    public static class Helper
    {
        public static string Base64UrlDecode(string input)
        {
            var output = input.Replace('-', '+').Replace('_', '/');
            switch (output.Length % 4)
            {
                case 0: break;
                case 2: output += "=="; break;
                case 3: output += "="; break;
                default: throw new ArgumentException("Input string was not a valid Base64Url encoded string.", "input");
            }
            return Encoding.UTF8.GetString(Convert.FromBase64String(output));
        }

        public static bool isLogin(string userExist)
        {
            if (string.IsNullOrEmpty(userExist))
            {
                return false;
            }
            var parts = userExist.Split('.');
            if (parts.Length != 3)
            {
                // JWT geçersiz.
                return false;
            }

            var payload = Helper.Base64UrlDecode(parts[1]);
            var expClaim = JObject.Parse(payload)["exp"];
            if (expClaim == null)
            {
                // JWT geçersiz.
                return false;

            }

            var expDate = DateTime.UnixEpoch.AddSeconds(long.Parse(expClaim.ToString()));

            if (expDate < DateTime.UtcNow)
            {
                // JWT süresi dolmuştur.
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
