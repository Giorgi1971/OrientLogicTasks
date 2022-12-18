using T13_WordAnalizer;
using System.Diagnostics;

var watchAll = new Stopwatch();
//Thread mainThread = Thread.CurrentThread;
//mainThread.Name = "Main Thread";
//Console.WriteLine(mainThread.Name);

watchAll.Start();

var watch0 = Stopwatch.StartNew();
string path = "words_alpha.txt";
WordReader tast13 = new WordReader();
var allText = tast13.Read(path);
watch0.Stop();

var watch1 = Stopwatch.StartNew();
tast13.FindWords(allText);
watch1.Stop();

var watch2 = Stopwatch.StartNew();
tast13.Dict100();
watch2.Stop();

watchAll.Stop();

//tast13.DosplayAnyDict(dictVowels100);
Console.WriteLine(watchAll.Elapsed.Duration());
Console.WriteLine(watch0.Elapsed.Duration());
Console.WriteLine(watch1.Elapsed.Duration());
Console.WriteLine(watch2.Elapsed.Duration());
