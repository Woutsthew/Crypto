
namespace Crypto.Encryption
{
    public interface IEncryptor
    {
        string Encrypt(string plainText);

        byte[] Encrypt(byte[] plainText);

        string Decrypt(string cipherText);

        byte[] Decrypt(byte[] cipherText);
    }

    public enum KeyType
    {
        Public, Private
    }
}
