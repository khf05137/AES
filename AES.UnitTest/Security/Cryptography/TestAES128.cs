using System;
using System.IO;
using System.Linq;
using khf05137.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AES.UnitTest.Security.Cryptography
{
    [TestClass]
    public class TestAES128
    {
        [TestMethod]
        public void TestPassword()
        {
            string password = "p@ssw0rd";

            byte[] testData = Enumerable.Range(0, 16).Select(val => (byte)val).ToArray();

            byte[] result;
            using (var outStream = new MemoryStream())
            {
                using (var encStream = AES128.EncryptStream(outStream, password))
                {
                    encStream.Write(testData, 0, testData.Length);
                }
                result = outStream.ToArray();
            }

            byte[] decrypted;
            using (var outStream2 = new MemoryStream())
            {
                using (var encStream = AES128.DecryptStream(new MemoryStream(result), password))
                {
                    encStream.CopyTo(outStream2);
                }
                decrypted = outStream2.ToArray();
            }

            Assert.IsTrue(testData.SequenceEqual(decrypted));
        }

        [TestMethod]
        public void TestKey()
        {
            byte[] key = Enumerable.Range(100, 16).Select(val => (byte)val).ToArray();

            byte[] testData = Enumerable.Range(0, 16).Select(val => (byte)val).ToArray();

            byte[] result;
            using (var outStream = new MemoryStream())
            {
                using (var encStream = AES128.EncryptStream(outStream, key))
                {
                    encStream.Write(testData, 0, testData.Length);
                }
                result = outStream.ToArray();
            }

            byte[] decrypted;
            using (var outStream2 = new MemoryStream())
            {
                using (var encStream = AES128.DecryptStream(new MemoryStream(result), key))
                {
                    encStream.CopyTo(outStream2);
                }
                decrypted = outStream2.ToArray();
            }

            Assert.IsTrue(testData.SequenceEqual(decrypted));

        }
    }
}
