﻿using ExtensionMethods;
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

//string strHash = "Hello World!";
//var hash = strHash.CalculateHash();
//Console.WriteLine($"Hash for {strHash} is: {hash}.");

//var path = "Files/file.txt";
//var txt = "Hello, This is text for Write in file.";
//txt.SaveToFile(path);

// Decimal Functions
// 1. პროცენტი
Console.WriteLine(0.5.ToPercent());

// 3. ტიპის დეციმალად გადაკონვერტირება
double doubleNumber2 = 32.5;
var result = doubleNumber2.ToDecimal();
Console.WriteLine(result.GetType());

// 4. უფრო მეტია
Console.WriteLine(doubleNumber2.IsGreater(35.5)); // False
Console.WriteLine(doubleNumber2.IsGreater(5.5)); // True

//5. ნაკლბია ვიდრე
Console.WriteLine(doubleNumber2.IsLess(35.5)); // True
Console.WriteLine(doubleNumber2.IsLess(5.5)); // False

// 2. ქვედა დამრგვალება
double doubleNumber5 = 12.1;
var result2 = doubleNumber5.RoundDown();
Console.WriteLine(result2);
