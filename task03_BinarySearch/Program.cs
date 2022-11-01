int BinarySearch(int[] array, int target)
{
    return Array.IndexOf(array, target);
}



// tests

var array = new int[] { 0, 1, 5, 76, 234, 678 };

var target = 76;

var index = BinarySearch(array, target);

Console.WriteLine(index); // index უნდა იყოს 3

index = BinarySearch(array, 75);

Console.WriteLine(index); // index უნდა იყოს -1