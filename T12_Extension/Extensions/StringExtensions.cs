using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;


namespace ExtensionMethods
{
    public static class StringExtensions
    {
        public static bool IsNumber(this string str)
        {
            return int.TryParse(str, out int num);
        }

        public static bool IsDate(this string str)
        {
            return DateTime.TryParse(str, out DateTime num);
        }

        public static string[] ToWords(this string str)
        {
            return str.Split();
        }

        public static string CalculateHash(this string str)
        {
        using (SHA256 sha256Hash = SHA256.Create())
            {
            string hash = GetHash(sha256Hash, str);
            return hash;
            }
        }

        private static string GetHash(HashAlgorithm hashAlgorithm, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            var sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        public static void SaveToFile(this string txt, string filePath)
        {
            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(filePath))
            {
                sw.WriteLine(txt);
                sw.WriteLine(filePath);
            }
        }

        // ToDo ეს ორი მეთოდი დასაწერია:
        // Encrypt(string key, string iv) - უნდა დააბრუნოს დაშიფრული ტექსტი
        // Decrypt(string key, string iv) - უნდა დააბრუნოს გაშიფრული ტექსტი
    }
}

