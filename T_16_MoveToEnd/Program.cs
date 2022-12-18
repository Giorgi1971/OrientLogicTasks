var array = new List<int> { 2, 1, 2, 2, 2, 3, 4, 2 };
var toMove = 2;

MoveToEnd(array, toMove);
Display(array);


void MoveToEnd(List<int> array, int toMove)
{
    var arrayEnd = array.FindAll(n => n == toMove);
    array.RemoveAll(n => n == toMove);
    array.AddRange(arrayEnd);
}

void Display(List<int> array)
{
    foreach (var item in array)
    {
        Console.Write(item + ", ");
    }
    Console.Write("\b\b");
}


