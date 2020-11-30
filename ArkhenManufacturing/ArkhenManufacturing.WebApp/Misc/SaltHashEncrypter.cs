using System.Security.Cryptography;
using System.Text;

namespace ArkhenManufacturing.WebApp.Misc
{
    public class SaltHashEncrypter : IEncrypter
    {
        public string Encrypt(string target) {
            // Sourced from: https://stackoverflow.com/a/212526
            byte[] data = Encoding.ASCII.GetBytes(target);
            data = new SHA256Managed().ComputeHash(data);
            return Encoding.ASCII.GetString(data);
        }
    }
}
