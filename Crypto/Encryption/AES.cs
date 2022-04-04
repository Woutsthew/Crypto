using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Crypto.Encryption
{
    public class AES : IEncryptor
    {
        private readonly AesCryptoServiceProvider provider = new AesCryptoServiceProvider();

        private string Key => Convert.ToBase64String(provider.Key);

        private string IV => Convert.ToBase64String(provider.IV);

        public string Key_IV => Key + "\\" + IV;

        public AES(KeySizeBit keysize = KeySizeBit.Small) { GenerateKey_IV(keysize); }

        public AES(string key, string iv) { SetKey(key, iv); }

        public AES(byte[] key, byte[] iv) { SetKey(key, iv); }

        public void SetKey(string key, string iv)
        {
            SetKey(Convert.FromBase64String(key),
                Convert.FromBase64String(iv));
        }

        public void SetKey(byte[] key, byte[] iv)
        {
            provider.KeySize = GetKeySize(key);

            provider.Key = key;
            provider.IV = iv;
        }

        public byte[] Encrypt(byte[] plainText)
        {
            return Encoding.UTF8.GetBytes(Encrypt(Encoding.UTF8.GetString(plainText)));
        }

        public string Encrypt(string plainText)
        {
            using (var msEncrypt = new MemoryStream())
            {
                using (var csEncrypt = new CryptoStream(msEncrypt, provider.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                }
                return Convert.ToBase64String(msEncrypt.ToArray());
            }
        }

        public byte[] Decrypt(byte[] cipherText)
        {
            return Encoding.UTF8.GetBytes(Decrypt(Encoding.UTF8.GetString(cipherText)));
        }

        public string Decrypt(string cipherText)
        {
            using (var msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
            {
                using (var csDecrypt = new CryptoStream(msDecrypt, provider.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    using (var srDecrypt = new StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }

        public void GenerateKey_IV(KeySizeBit keysize = KeySizeBit.Small)
        {
            provider.KeySize = (int)keysize;
            provider.GenerateKey();
            provider.GenerateIV();
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
            Small = 128,
            Medium = 192,
            Big = 256,
        }
    }
}