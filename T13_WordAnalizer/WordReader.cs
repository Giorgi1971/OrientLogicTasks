using System;
using System.Collections.Concurrent;

namespace T13_WordAnalizer
{
    public class WordReader
    {
        public readonly char[] _vowels = { 'a', 'e', 'i', 'o', 'u' };
        public string? shortWord { get; set; }
        public string? longWord { get; set; }
        public ConcurrentDictionary<string, int>? wordVowels;

        public WordReader()
        {
            wordVowels = new ConcurrentDictionary<string, int>();
        }

        public void DoTask13()
        {
            return;
        }

        public string[] Read(string path)
        {
            string[] fileAll = File.ReadAllLines(path);
            return fileAll;
        }

        public void Dict100()
        {
            if (wordVowels is not null)
            {
                var dict_100 = wordVowels
                .OrderByDescending(d => d.Value)
                .Take(20)
                .ToDictionary(pair => pair.Key, pair => pair.Value);
                //Console.WriteLine(dict_100.Count());
                DosplayAnyDict(dict_100);
            }
        }

        // find shor and long word. Alse create Dictionary (Word => Vowels.Count)
        public void FindWords(string[] allText)
        {
            int shortWordLength = int.MaxValue;

            int longWordLength = 0;

            var options = new ParallelOptions { MaxDegreeOfParallelism = 4 };


            Parallel.ForEach(allText, options, (word) =>

            //foreach (var word in allText)
            {
                if (word.Length > longWordLength)
                {
                    longWordLength = word.Length;
                    longWord = word;
                }
                if (word.Length < shortWordLength)
                {
                    shortWordLength = word.Length;
                    shortWord = word;
                }

                int counter = 0;
                foreach (var letter in word)
                {
                    if (_vowels.Contains(letter))
                    {
                        counter++;
                    }
                }
                if (wordVowels is not null)
                    wordVowels.TryAdd(word, counter);

            });
            //Console.WriteLine(Dict.Count);
            Console.WriteLine($"The shortes word's {shortWord} lenth is {shortWordLength}");
            Console.WriteLine($"The longest word's {longWord} lenth is {longWordLength}");
        }

        public void DosplayAnyDict(Dictionary<string, int> dict)
        {
            if(dict != null)
            {
                foreach (var item in dict)
                {
                    System.Console.WriteLine($"Word {item.Key} has {item.Value} vowels");
                }
            }
        }
    }
}

