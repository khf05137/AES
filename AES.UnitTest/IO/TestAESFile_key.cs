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
    public class TestAESFile_key
    {
        [TestMethod]
        public void TestBytes()
        {
            var temp = Path.GetTempFileName();
            byte[] key = Encoding.UTF8.GetBytes("ABCDEFGHIJKLMNOP");
            var data = Enumerable.Range(0, 1024 + 1).Select(val => (byte)val).ToArray();

            AES128File.WriteAllBytes(temp, data, key);

            var readedData = AES128File.ReadAllBytes(temp, key);

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
            byte[] key = Encoding.UTF8.GetBytes("ABCDEFGHIJKLMNOP");
            var expect = GetStringList();

            AES128File.WriteLines(temp, expect, key);

            var list = AES128File.ReadLines(temp, key);
            Assert.IsTrue(expect.SequenceEqual(list));
        }

        [TestMethod]
        public void TestLinesEncoding()
        {
            var temp = Path.GetTempFileName();
            byte[] key = Encoding.UTF8.GetBytes("ABCDEFGHIJKLMNOP");
            var expect = GetStringList();

            AES128File.WriteLines(temp, expect, Encoding.Unicode, key);

            var list = AES128File.ReadLines(temp, Encoding.Unicode, key);

            Assert.IsTrue(expect.SequenceEqual(list));
        }

        [TestMethod]
        public void TestAllLines()
        {
            var temp = Path.GetTempFileName();
            byte[] key = Encoding.UTF8.GetBytes("ABCDEFGHIJKLMNOP");
            var expect = GetStringList().ToArray();

            AES128File.WriteAllLines(temp, expect, key);

            var list = AES128File.ReadAllLines(temp, key);
            Assert.IsTrue(expect.SequenceEqual(list));
        }

        [TestMethod]
        public void TestAllLinesEncoding()
        {
            var temp = Path.GetTempFileName();
            byte[] key = Encoding.UTF8.GetBytes("ABCDEFGHIJKLMNOP");
            var expect = GetStringList().ToArray();

            AES128File.WriteAllLines(temp, expect, Encoding.Unicode, key);
            var list = AES128File.ReadAllLines(temp, Encoding.Unicode, key);
            Assert.IsTrue(expect.SequenceEqual(list));
        }

        [TestMethod]
        public void TestAllText()
        {
            var temp = Path.GetTempFileName();
            byte[] key = Encoding.UTF8.GetBytes("ABCDEFGHIJKLMNOP");
            var expect = "The quick brown fox jumps over the lazy dog. 1234567890";

            AES128File.WriteAllText(temp, expect, key);
            var text = AES128File.ReadAllText(temp, key);

            Assert.AreEqual(expect, text);
        }
    }
}
