var array = new[] { 7, 5, 1, 8, 9, 2, 2, 3, 4, 8 };
var toMove = 2;
MoveElementToEnd(array, toMove);
Console.WriteLine(string.Join(", ", array));

void Swap(int[] array, int left, int right)
{
    (array[left], array[right]) = (array[right], array[left]);
}

void MoveElementToEnd(int[] array, int toMove)
{
    var right = array.Length - 1;


    for (int i = 0; i < right; i++)
    {

        // თუ წინა წევრი არ უდრის 2-ს გააგრძელე
        if (array[i] != toMove)
        {
            continue;
        }
        // როცა მარჯვენა მხარე უდრის 2-ს, ვნახულობთ წინა რიხვს
        else
        {
            while (array[right] == toMove)
            {
                if (i >= right)
                    break;
                right--;
            }
        }
        Swap(array, i, right);
    }
}
