using System;

namespace T13_WordAnalizer
{
    public class WordReader
    {

        public readonly char[] _vowels = { 'a', 'e', 'i', 'o', 'u', };
        public string? shortWord { get; set; }
        public string? longWord { get; set; }

        //public WordReader()
        //{
        //}

        public void DoTask13()
        {
            Console.WriteLine("Hello");
        }

        public string[] Read(string path)
        {
            string[] fileAll = File.ReadAllLines(path);
            return fileAll;
        }

        public void FindWords(string[] allText)
        {
            int shortWordLenth = int.MaxValue;

            int longWordLenth = 0;

            foreach (var item in allText)
            {
                if (item.Length > longWordLenth)
                {
                    longWordLenth = item.Length;
                    longWord = item;
                }
                if (item.Length < shortWordLenth)
                {
                    shortWordLenth = item.Length;
                    shortWord = item;
                }
            }
            Console.WriteLine(longWord);
            Console.WriteLine(shortWord);
        }
    }
}

