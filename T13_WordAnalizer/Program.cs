using T13_WordAnalizer;
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

Thread mainThread = Thread.CurrentThread;
mainThread.Name = "Main Thread";
Console.WriteLine(mainThread.Name);
Console.WriteLine(mainThread.Name);
Console.WriteLine(mainThread.Name);

string path = "words_alpha.txt";


WordReader tast13 = new WordReader();
var allText = tast13.Read(path);

tast13.FindWords(allText);