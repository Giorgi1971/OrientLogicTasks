string ToBinary(long number)
{
    return Convert.ToString(number, 2);
}

Console.WriteLine(ToBinary(2)); // 10
Console.WriteLine(ToBinary(256)); // 100000000
Console.WriteLine(ToBinary(33)); // 100001
Console.WriteLine(ToBinary(999)); // 1111100111