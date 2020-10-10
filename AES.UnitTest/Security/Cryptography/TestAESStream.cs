using khf05137.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

namespace AES.UnitTest.Security.Cryptography
{
    [TestClass]
    public class TestAESStream
    {
        [TestMethod]
        public void EncryptDecrypt()
        {
            string password = "p@ssw0rd";

            byte[] testData = Enumerable.Range(0, 16).Select(val => (byte)val).ToArray();

            byte[] result;
            using (var outStream = new MemoryStream())
            {
                using (var encStream = new AESEncryptoStream(outStream, password))
                {
                    encStream.Write(testData, 0, testData.Length);
                }

                result = outStream.ToArray();
            }

            byte[] decrypted;
            using (var outStream2 = new MemoryStream())
            {
                using (var encStream = new AESDecryptoStream(new MemoryStream(result), password))
                {
                    byte[] buffer = new byte[128];
                    int len = encStream.Read(buffer, 0, 128);
                    outStream2.Write(buffer, 0, len);
                }

                decrypted = outStream2.ToArray();
            }

            Assert.IsTrue(testData.SequenceEqual(decrypted));
        }
    }
}
