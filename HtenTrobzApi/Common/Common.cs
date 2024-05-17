using HtenTrobzApi.Models;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace HtenTrobzApi
{
    public static class Common
    {
        public static string GetConnectionString(string dbName)
        {
            return new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString(dbName) ?? "";
        }

        public static byte[] MD5Hash(string input)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }

            return Encoding.ASCII.GetBytes(sb.ToString());
        }

        public static bool ValidateUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return false;
            }

            using (var db = new HtenContext())
            {
                var member = db.H00members.Where(o => o.MemberId == username && o.MemberType == "U").FirstOrDefault();

                if (member == null || member.CheckPass == null || !member.CheckPass.SequenceEqual(Common.MD5Hash(password)))
                    return false;
                else
                    return true;
            }
        }

        public static string GetUserId(this ClaimsPrincipal user)
        {
            return ((ClaimsIdentity)user.Identity).Claims.First(c => c.Type == ClaimTypes.Email).Value;
        }
    }
}
