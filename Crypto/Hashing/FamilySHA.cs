using System.Security.Cryptography;
using System.Text;

namespace Crypto.Hashing
{
    public static class FamilySHA
    {
        public static string SHA256(string rString)
        {
            return SHA(rString, new SHA256Managed());
        }

        public static string SHA(string rString, HashAlgorithm managed)
        {
            return SHA(Encoding.UTF8.GetBytes(rString), managed);
        }

        public static string SHA(byte[] rString, HashAlgorithm managed)
        {
            var hash = new StringBuilder();

            byte[] crypto = managed.ComputeHash(rString);

            foreach (byte theByte in crypto)
                hash.Append(theByte.ToString("x2"));

            return hash.ToString();
        }
    }
}
