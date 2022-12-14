using ExtensionMethods;
// რიცხვების ექსტენშენ მეთოდების შემოწმება
int num1 = 15;
Console.WriteLine(num1.IsGreaterThan(10));

// რიცხვია თუ არა?
string strNumber = "123";
Console.WriteLine(strNumber.IsNumber());

// თარიღია თუ არა
string strDate = "2033 12 09 2:20";
Console.WriteLine(strDate.IsDate());

// 
string strSplit = "2033 12 09 werfwer";
var splited = strSplit.ToWords();
foreach (var item in splited)
{
    Console.WriteLine(item);
}

string strHash = "Hello World!";
var hash = strHash.CalculateHash();
Console.WriteLine($"Hash for {strHash} is: {hash}.");

var path = "Files/file.txt";
var txt = "Hello, This is text for Write in file.";
txt.SaveToFile(path);

// 