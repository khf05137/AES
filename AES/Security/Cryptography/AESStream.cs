using System.IO;
using System.Security.Cryptography;

namespace khf05137.Security.Cryptography
{
    public enum AESCryptoStreamMode
    {
        //
        // 概要:
        //     暗号ストリームに対する読み取りアクセス。
        Read = 0,
        //
        // 概要:
        //     暗号ストリームへの書き込みアクセス。
        Write = 1
    }

    public class AESStream : Stream
    {
        CryptoStream stream;

        public AESStream(Stream stream, AESCryptoStreamMode mode, string password)
        {
            using (var am = new AesManaged())
            {
                am.BlockSize = 128;
                am.KeySize = 128;
                am.Mode = CipherMode.CBC;
                am.Padding = PaddingMode.PKCS7;

                CryptoStreamMode cyptoMode;
                if (mode == AESCryptoStreamMode.Read)
                {
                    cyptoMode = CryptoStreamMode.Read;

                    byte[] salt = new byte[16];
                    stream.Read(salt, 0, 16);

                    byte[] IV = new byte[16];
                    stream.Read(IV, 0, 16);
                    am.IV = IV;

                    var deriveBytes = new Rfc2898DeriveBytes(password, salt);
                    am.Key = deriveBytes.GetBytes(16);
                }
                else
                {
                    cyptoMode = CryptoStreamMode.Write;

                    var deriveBytes = new Rfc2898DeriveBytes(password, 16);
                    am.Key = deriveBytes.GetBytes(16);

                    am.GenerateIV();
                    stream.Write(deriveBytes.Salt, 0, 16);
                    stream.Write(am.IV, 0, 16);
                }

                var encryptor = am.CreateEncryptor(am.Key, am.IV);
                this.stream = new CryptoStream(stream, encryptor, cyptoMode);
            }
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
