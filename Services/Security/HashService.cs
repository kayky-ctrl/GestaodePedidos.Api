using System.Security.Cryptography;
using System.Text;

namespace ShopGestProjeto.Api.Services.Security
{
    public class HashService : IHashService 
    {
        public string GerarHash(string valor)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(valor);
            var hashBytes = sha256.ComputeHash(bytes);

            return Convert.ToBase64String(hashBytes);
        }
    }
}
