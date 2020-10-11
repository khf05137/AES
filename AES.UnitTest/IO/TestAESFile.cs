using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using khf05137.IO;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace AES.UnitTest.IO
{
    [TestClass]
    public class TestAESFile
    {
        [TestMethod]
        public void TestBytes()
        {
            var temp = Path.GetTempFileName();
            string password = "p@ssw0rd";
            var data = Enumerable.Range(0, 1024 + 1).Select(val => (byte)val).ToArray();

            AESFile.WriteAllBytes(temp, data, password);

            var readedData = AESFile.ReadAllBytes(temp, password);

            Assert.IsTrue(data.SequenceEqual(readedData));
        }

        IEnumerable<string> GetStringList()
        {
            List<string> list = new List<string>
            {
                "aaaaaaaaaaaa",
                "bbbbbbbbbbbb",
                "cccccccccccc",
            };

            return list.ToArray();
        }

        [TestMethod]
        public void TestLines()
        {
            var temp = Path.GetTempFileName();
            string password = "p@ssw0rd";
            var expect = GetStringList();

            AESFile.WriteLines(temp, expect, password);

            var list = AESFile.ReadLines(temp, password);
            Assert.IsTrue(expect.SequenceEqual(list));
        }

        [TestMethod]
        public void TestLinesEncoding()
        {
            var temp = Path.GetTempFileName();
            string password = "p@ssw0rd";
            var expect = GetStringList();

            AESFile.WriteLines(temp, expect, Encoding.Unicode, password);

            var list = AESFile.ReadLines(temp, Encoding.Unicode, password);

            Assert.IsTrue(expect.SequenceEqual(list));
        }

        [TestMethod]
        public void TestAllLines()
        {
            var temp = Path.GetTempFileName();
            string password = "p@ssw0rd";
            var expect = GetStringList().ToArray();

            AESFile.WriteAllLines(temp, expect, password);

            var list = AESFile.ReadAllLines(temp, password);
            Assert.IsTrue(expect.SequenceEqual(list));
        }

        [TestMethod]
        public void TestAllLinesEncoding()
        {
            var temp = Path.GetTempFileName();
            string password = "p@ssw0rd";
            var expect = GetStringList().ToArray();

            AESFile.WriteAllLines(temp, expect, Encoding.Unicode, password);
            var list = AESFile.ReadAllLines(temp, Encoding.Unicode, password);
            Assert.IsTrue(expect.SequenceEqual(list));
        }

        [TestMethod]
        public void TestAllText()
        {
            var temp = Path.GetTempFileName();
            string password = "p@ssw0rd";
            var expect = "The quick brown fox jumps over the lazy dog. 1234567890";

            AESFile.WriteAllText(temp, expect, password);
            var text = AESFile.ReadAllText(temp, password);

            Assert.AreEqual(expect, text);
        }
    }
}
