using System.IO;
using System.Security.Cryptography;

namespace khf05137.Security.Cryptography
{
    public class AESEncryptoStream : Stream
    {
        CryptoStream stream;
        ICryptoTransform crypter;

        public AESEncryptoStream(Stream outStream, string password)
        {
            using (var am = new AesManaged())
            {
                am.BlockSize = 128;
                am.KeySize = 128;
                am.Mode = CipherMode.CBC;
                am.Padding = PaddingMode.PKCS7;

                var deriveBytes = new Rfc2898DeriveBytes(password, 16);
                byte[] Key = deriveBytes.GetBytes(16);

                am.Key = Key;
                am.GenerateIV();

                outStream.Write(deriveBytes.Salt, 0, 16);
                outStream.Write(am.IV, 0, 16);

                this.crypter = am.CreateEncryptor(am.Key, am.IV);
                this.stream = new CryptoStream(outStream, crypter, CryptoStreamMode.Write);
            }
        }

        protected override void Dispose(bool disposing)
        {
            this.stream?.Dispose();
            this.stream = null;

            this.crypter?.Dispose();
            this.crypter = null;
        }

        public override bool CanRead => this.stream.CanRead;
        public override bool CanSeek => this.stream.CanSeek;
        public override bool CanWrite => this.stream.CanWrite;
        public override long Length => this.stream.Length;
        public override long Position { get => this.stream.Position; set => this.stream.Position = value; }
        public override void Flush() => this.stream.Flush();
        public override int Read(byte[] buffer, int offset, int count) => this.stream.Read(buffer, offset, count);
        public override long Seek(long offset, SeekOrigin origin) => this.stream.Seek(offset, origin);
        public override void SetLength(long value) => this.stream.SetLength(value);
        public override void Write(byte[] buffer, int offset, int count) => this.stream.Write(buffer, offset, count);
    }

    public class AESDecryptoStream : Stream
    {
        CryptoStream stream;
        ICryptoTransform crypter;

        public AESDecryptoStream(Stream stream, string password)
        {
            byte[] salt = new byte[16];
            stream.Read(salt, 0, 16);

            byte[] IV = new byte[16];
            stream.Read(IV, 0, 16);

            using (var am = new AesManaged())
            {
                am.BlockSize = 128;
                am.KeySize = 128;
                am.Mode = CipherMode.CBC;
                am.Padding = PaddingMode.PKCS7;

                var deriveBytes = new Rfc2898DeriveBytes(password, salt);
                am.Key = deriveBytes.GetBytes(16);
                am.IV = IV;

                this.crypter = am.CreateDecryptor(am.Key, am.IV);
                this.stream = new CryptoStream(stream, crypter, CryptoStreamMode.Read);
            }
        }

        protected override void Dispose(bool disposing)
        {
            this.stream?.Dispose();
            this.stream = null;

            this.crypter?.Dispose();
            this.crypter = null;
        }

        public override bool CanRead => this.stream.CanRead;
        public override bool CanSeek => this.stream.CanSeek;
        public override bool CanWrite => this.stream.CanWrite;
        public override long Length => this.stream.Length;
        public override long Position { get => this.stream.Position; set => this.stream.Position = value; }
        public override void Flush() => this.stream.Flush();
        public override int Read(byte[] buffer, int offset, int count) => this.stream.Read(buffer, offset, count);
        public override long Seek(long offset, SeekOrigin origin) => this.stream.Seek(offset, origin);
        public override void SetLength(long value) => this.stream.SetLength(value);
        public override void Write(byte[] buffer, int offset, int count) => this.stream.Write(buffer, offset, count);
    }
}
