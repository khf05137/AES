using khf05137.Security.Cryptography;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace khf05137.IO
{
    public static class AESFile
    {
        public static Stream OpenRead(string path, string password) => new AESDecryptoStream(new FileStream(path, FileMode.Open, FileAccess.Read), password);
        public static StreamReader OpenText(string path, Encoding encoding, string password) => new StreamReader(OpenRead(path, password), encoding);
        public static StreamReader OpenText(string path, string password) => OpenText(path, Encoding.UTF8, password);
        public static Stream OpenWrite(string path, string password) => new AESEncryptoStream(new FileStream(path, FileMode.Create, FileAccess.Write), password);
        public static StreamWriter OpenWriteText(string path, Encoding encoding, string password) => new StreamWriter(OpenWrite(path, password), encoding);
        public static StreamWriter OpenWriteText(string path, string password) => new StreamWriter(OpenWrite(path, password), new UTF8Encoding(false));

        public static byte[] ReadAllBytes(string path, string password)
        {
            using (var output = new MemoryStream())
            {
                using (var cs = OpenRead(path, password))
                {
                    cs.CopyTo(output);
                }
                return output.ToArray();
            }
        }

        public static IEnumerable<string> ReadLines(string path, string password)
        {
            using (var sr = OpenText(path, password))
            {
                while (!sr.EndOfStream)
                {
                    yield return sr.ReadLine();
                }
            }
        }

        public static IEnumerable<string> ReadLines(string path, Encoding encoding, string password)
        {
            using (var sr = OpenText(path, encoding, password))
            {
                while (!sr.EndOfStream)
                {
                    yield return sr.ReadLine();
                }
            }
        }

        public static string[] ReadAllLines(string path, string password) => ReadLines(path, password).ToArray();
        public static string[] ReadAllLines(string path, Encoding encoding, string password) => ReadLines(path, encoding, password).ToArray();

        public static string ReadAllText(string path, string password)
        {
            using (var sr = OpenText(path, password))
            {
                return sr.ReadToEnd();
            }
        }
        public static string ReadAllText(string path, Encoding encoding, string password)
        {
            using (var sr = OpenText(path, encoding, password))
            {
                return sr.ReadToEnd();
            }
        }

        public static void WriteAllBytes(string path, byte[] bytes, string password)
        {
            using (var cs = OpenWrite(path, password))
            {
                cs.Write(bytes, 0, bytes.Length);
            }
        }

        public static void WriteLines(string path, IEnumerable<string> contents, Encoding encoding, string password)
        {
            using (var cs = OpenWriteText(path, encoding, password))
            {
                foreach (var line in contents)
                {
                    cs.WriteLine(line);
                }
            }
        }

        public static void WriteLines(string path, IEnumerable<string> contents, string password)
        {
            using (var cs = OpenWriteText(path, password))
            {
                foreach (var line in contents)
                {
                    cs.WriteLine(line);
                }
            }
        }
        public static void WriteAllLines(string path, string[] contents, string password) => WriteLines(path, contents, password);
        public static void WriteAllLines(string path, string[] contents, Encoding encoding, string password) => WriteLines(path, contents, encoding, password);

        public static void WriteAllText(string path, string contents, string password)
        {
            using (var cs = OpenWriteText(path, password))
            {
                cs.Write(contents);
            }
        }

        public static void WriteAllText(string path, string contents, Encoding encoding, string password)
        {
            using (var cs = OpenWriteText(path, encoding, password))
            {
                cs.Write(contents);
            }
        }
    }
}
