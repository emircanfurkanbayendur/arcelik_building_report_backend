using BuildingReport.Business.Abstract;
using System.Security.Cryptography;
using System.Text;

namespace BuildingReport.Business.Concrete
{

    public class Hash : IHashService
    {

        public  byte[] HashPassword(string password)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            SHA256 sha256 = SHA256.Create();
            byte[] hashBytes = sha256.ComputeHash(passwordBytes);
            return hashBytes;

        }
        

    }
}
