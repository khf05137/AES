using System;
using System.IO;
using System.Security.Cryptography;

namespace khf05137.Security.Cryptography
{
    public static class AES128
    {
        public static Stream EncryptStream(Stream outStream, byte[] key)
        {
            if (key.Length != 16) throw new ArgumentException();

            using (var am = new AesManaged())
            {
                am.BlockSize = 128;
                am.KeySize = 128;
                am.Mode = CipherMode.CBC;
                am.Padding = PaddingMode.PKCS7;

                am.Key = key;
                am.GenerateIV();

                outStream.Write(am.IV, 0, 16);

                var crypter = am.CreateEncryptor(am.Key, am.IV);
                return new CryptoStream(outStream, crypter, CryptoStreamMode.Write);
            }
        }

        public static Stream EncryptStream(Stream outStream, string password)
        {
            using (var am = new AesManaged())
            {
                am.BlockSize = 128;
                am.KeySize = 128;
                am.Mode = CipherMode.CBC;
                am.Padding = PaddingMode.PKCS7;

                var deriveBytes = new Rfc2898DeriveBytes(password, 16);
                am.Key = deriveBytes.GetBytes(16);
                am.GenerateIV();

                outStream.Write(deriveBytes.Salt, 0, 16);  // !!!
                outStream.Write(am.IV, 0, 16);

                var crypter = am.CreateEncryptor(am.Key, am.IV);
                return new CryptoStream(outStream, crypter, CryptoStreamMode.Write);
            }
        }

        public static Stream DecryptStream(Stream inStream, byte[] key)
        {
            using (var am = new AesManaged())
            {
                am.BlockSize = 128;
                am.KeySize = 128;
                am.Mode = CipherMode.CBC;
                am.Padding = PaddingMode.PKCS7;

                byte[] IV = new byte[16];
                inStream.Read(IV, 0, 16);

                am.Key = key;
                am.IV = IV;

                var crypter = am.CreateDecryptor(am.Key, am.IV);
                return new CryptoStream(inStream, crypter, CryptoStreamMode.Read);
            }
        }

        public static Stream DecryptStream(Stream inStream, string password)
        {
            using (var am = new AesManaged())
            {
                am.BlockSize = 128;
                am.KeySize = 128;
                am.Mode = CipherMode.CBC;
                am.Padding = PaddingMode.PKCS7;

                byte[] salt = new byte[16];
                inStream.Read(salt, 0, 16);  // !!!

                byte[] IV = new byte[16];
                inStream.Read(IV, 0, 16);

                var deriveBytes = new Rfc2898DeriveBytes(password, salt);
                am.Key = deriveBytes.GetBytes(16);
                am.IV = IV;

                var crypter = am.CreateDecryptor(am.Key, am.IV);
                return new CryptoStream(inStream, crypter, CryptoStreamMode.Read);
            }
        }
    }
}
