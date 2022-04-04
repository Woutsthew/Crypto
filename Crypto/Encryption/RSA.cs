using System;
using System.Security.Cryptography;
using System.Text;

namespace Crypto.Encryption
{
    public class RSA : IEncryptor
    {
        private RSACryptoServiceProvider provider;

        private byte[] publicKey;

        private byte[] privateKey;

        public string PublicKey => Convert.ToBase64String(publicKey);

        public RSA(KeySizeBit keysize = KeySizeBit.Small) { GenerateKeys(keysize); }

        public RSA(KeyType type, string Key) { SetKey(type, Key); }

        public RSA(KeyType type, byte[] Key) { SetKey(type, Key); }

        public RSA(string publicKey, string privateKey) { SetKeys(publicKey, privateKey); }

        public RSA(byte[] publicKey, byte[] privateKey) { SetKeys(publicKey, privateKey); }

        public void SetKey(KeyType type, string Key) { SetKey(type, Convert.FromBase64String(Key)); }

        public void SetKey(KeyType type, byte[] Key)
        {
            if (type == KeyType.Public)
                SetKeys(Key);
            else if (type == KeyType.Private)
                SetKeys(null, Key);
        }

        public void SetKeys(string publicKey, string privateKey)
        {
            SetKeys(Convert.FromBase64String(publicKey),
                Convert.FromBase64String(privateKey));
        }

        public void SetKeys(byte[] publicKey, byte[] privateKey = null)
        {
            int keysize = privateKey != null ? GetKeySize(privateKey) : GetKeySize(publicKey);

            provider = new RSACryptoServiceProvider(keysize);

            provider.PersistKeyInCsp = false;

            this.publicKey = publicKey;
            this.privateKey = privateKey;
        }

        public string Encrypt(string plainText)
        {
            return Convert.ToBase64String(Encrypt(Encoding.UTF8.GetBytes(plainText)));
        }

        public byte[] Encrypt(byte[] plainText)
        {
            if (publicKey == null)
                throw new CryptographicException("Провайдер RSA не имеет публичного ключа.");

            provider.ImportCspBlob(publicKey);

            return provider.Encrypt(plainText, false);
        }

        public string Decrypt(string cipherText)
        {
            return Encoding.UTF8.GetString(Decrypt(Convert.FromBase64String(cipherText)));
        }

        public byte[] Decrypt(byte[] cipherText)
        {
            if (privateKey == null)
                throw new CryptographicException("Провайдер RSA не имеет приватного ключа.");

            provider.ImportCspBlob(privateKey);

            return provider.Decrypt(cipherText, false);
        }

        public void GenerateKeys(KeySizeBit keysize = KeySizeBit.Small)
        {
            provider = new RSACryptoServiceProvider((int)keysize);

            provider.PersistKeyInCsp = false;

            publicKey = provider.ExportCspBlob(false);
            privateKey = provider.ExportCspBlob(true);
        }

        public int GetKeySize(string key)
        {
            return GetKeySize(Convert.FromBase64String(key));
        }

        public int GetKeySize(byte[] key)
        {
            return key.Length * 8;
        }

        public enum KeySizeBit
        {
            Small = 1024,
            Medium = 1536,
            Big = 2048
        }
    }
}
