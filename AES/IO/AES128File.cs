using khf05137.Security.Cryptography;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace khf05137.IO
{
    public static class AES128File
    {
        // Key

        public static Stream OpenRead(string path, byte[] key) => AES128.DecryptStream(new FileStream(path, FileMode.Open, FileAccess.Read), key);
        public static StreamReader OpenText(string path, Encoding encoding, byte[] key) => new StreamReader(OpenRead(path, key), encoding);
        public static StreamReader OpenText(string path, byte[] key) => OpenText(path, Encoding.UTF8, key);
        public static Stream OpenWrite(string path, byte[] key) => AES128.EncryptStream(new FileStream(path, FileMode.Create, FileAccess.Write), key);
        public static StreamWriter OpenWriteText(string path, Encoding encoding, byte[] key) => new StreamWriter(OpenWrite(path, key), encoding);
        public static StreamWriter OpenWriteText(string path, byte[] key) => new StreamWriter(OpenWrite(path, key), new UTF8Encoding(false));


        public static byte[] ReadAllBytes(string path, byte[] key)
        {
            using (var output = new MemoryStream())
            {
                using (var cs = OpenRead(path, key))
                {
                    cs.CopyTo(output);
                }
                return output.ToArray();
            }
        }

        public static IEnumerable<string> ReadLines(string path, byte[] key)
        {
            using (var sr = OpenText(path, key))
            {
                while (!sr.EndOfStream)
                {
                    yield return sr.ReadLine();
                }
            }
        }

        public static IEnumerable<string> ReadLines(string path, Encoding encoding, byte[] key)
        {
            using (var sr = OpenText(path, encoding, key))
            {
                while (!sr.EndOfStream)
                {
                    yield return sr.ReadLine();
                }
            }
        }

        public static string[] ReadAllLines(string path, byte[] key) => ReadLines(path, key).ToArray();
        public static string[] ReadAllLines(string path, Encoding encoding, byte[] key) => ReadLines(path, encoding, key).ToArray();

        public static string ReadAllText(string path, byte[] key)
        {
            using (var sr = OpenText(path, key))
            {
                return sr.ReadToEnd();
            }
        }
        public static string ReadAllText(string path, Encoding encoding, byte[] key)
        {
            using (var sr = OpenText(path, encoding, key))
            {
                return sr.ReadToEnd();
            }
        }

        public static void WriteAllBytes(string path, byte[] bytes, byte[] key)
        {
            using (var cs = OpenWrite(path, key))
            {
                cs.Write(bytes, 0, bytes.Length);
            }
        }

        public static void WriteLines(string path, IEnumerable<string> contents, Encoding encoding, byte[] key)
        {
            using (var cs = OpenWriteText(path, encoding, key))
            {
                foreach (var line in contents)
                {
                    cs.WriteLine(line);
                }
            }
        }

        public static void WriteLines(string path, IEnumerable<string> contents, byte[] key)
        {
            using (var cs = OpenWriteText(path, key))
            {
                foreach (var line in contents)
                {
                    cs.WriteLine(line);
                }
            }
        }
        public static void WriteAllLines(string path, string[] contents, byte[] key) => WriteLines(path, contents, key);
        public static void WriteAllLines(string path, string[] contents, Encoding encoding, byte[] key) => WriteLines(path, contents, encoding, key);

        public static void WriteAllText(string path, string contents, byte[] key)
        {
            using (var cs = OpenWriteText(path, key))
            {
                cs.Write(contents);
            }
        }

        public static void WriteAllText(string path, string contents, Encoding encoding, byte[] key)
        {
            using (var cs = OpenWriteText(path, encoding, key))
            {
                cs.Write(contents);
            }
        }

        // Password

        public static Stream OpenRead(string path, string password) => AES128.DecryptStream(new FileStream(path, FileMode.Open, FileAccess.Read), password);
        public static StreamReader OpenText(string path, Encoding encoding, string password) => new StreamReader(OpenRead(path, password), encoding);
        public static StreamReader OpenText(string path, string password) => OpenText(path, Encoding.UTF8, password);
        public static Stream OpenWrite(string path, string password) => AES128.EncryptStream(new FileStream(path, FileMode.Create, FileAccess.Write), password);
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
