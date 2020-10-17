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
    public class TestAESFile_password
    {
        [TestMethod]
        public void TestBytes()
        {
            var temp = Path.GetTempFileName();
            string password = "p@ssw0rd";
            var data = Enumerable.Range(0, 1024 + 1).Select(val => (byte)val).ToArray();

            AES128File.WriteAllBytes(temp, data, password);

            var readedData = AES128File.ReadAllBytes(temp, password);

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

            AES128File.WriteLines(temp, expect, password);

            var list = AES128File.ReadLines(temp, password);
            Assert.IsTrue(expect.SequenceEqual(list));
        }

        [TestMethod]
        public void TestLinesEncoding()
        {
            var temp = Path.GetTempFileName();
            string password = "p@ssw0rd";
            var expect = GetStringList();

            AES128File.WriteLines(temp, expect, Encoding.Unicode, password);

            var list = AES128File.ReadLines(temp, Encoding.Unicode, password);

            Assert.IsTrue(expect.SequenceEqual(list));
        }

        [TestMethod]
        public void TestAllLines()
        {
            var temp = Path.GetTempFileName();
            string password = "p@ssw0rd";
            var expect = GetStringList().ToArray();

            AES128File.WriteAllLines(temp, expect, password);

            var list = AES128File.ReadAllLines(temp, password);
            Assert.IsTrue(expect.SequenceEqual(list));
        }

        [TestMethod]
        public void TestAllLinesEncoding()
        {
            var temp = Path.GetTempFileName();
            string password = "p@ssw0rd";
            var expect = GetStringList().ToArray();

            AES128File.WriteAllLines(temp, expect, Encoding.Unicode, password);
            var list = AES128File.ReadAllLines(temp, Encoding.Unicode, password);
            Assert.IsTrue(expect.SequenceEqual(list));
        }

        [TestMethod]
        public void TestAllText()
        {
            var temp = Path.GetTempFileName();
            string password = "p@ssw0rd";
            var expect = "The quick brown fox jumps over the lazy dog. 1234567890";

            AES128File.WriteAllText(temp, expect, password);
            var text = AES128File.ReadAllText(temp, password);

            Assert.AreEqual(expect, text);
        }
    }
}
