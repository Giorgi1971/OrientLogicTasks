using System;

Console.WriteLine("Enter Player2 name: ");
string player1 = Console.ReadLine();

Console.WriteLine("Enter Player1 name: ");
string player2 = Console.ReadLine();

string currentPlayer = player1;

string[,] cells;
cells = new string[3, 3] { { "-", "-", "-" }, { "-", "-", "-" }, { "-", "-", "-" } };

string mark = "X";

// არ მუშაობს, იქნებ გავარკვიო.
bool move = (cells[0, 0] == cells[0, 1] && cells[0, 0] != "-" && cells[0, 1] == cells[0, 2]);
//Console.WriteLine(move);
int counter = 0;

while (true)
{
    if (
        (cells[0, 0] != "-" && cells[0, 0] == cells[0, 1] && cells[0, 1] == cells[0, 2]) ||
        (cells[1, 0] == cells[1, 1] && cells[1, 0] != "-" && cells[1, 1] == cells[1, 2]) ||
        (cells[2, 0] == cells[2, 1] && cells[2, 1] == cells[2, 2] && cells[2, 0] != "-") ||
        (cells[0, 0] == cells[1, 0] && cells[1, 0] == cells[2, 0] && cells[0, 0] != "-") ||
        (cells[0, 1] == cells[1, 1] && cells[1, 1] == cells[2, 1] && cells[0, 1] != "-") ||
        (cells[0, 2] == cells[1, 2] && cells[1, 2] == cells[2, 2] && cells[0, 2] != "-") ||
        (cells[2, 0] == cells[1, 1] && cells[1, 1] == cells[0, 2] && cells[0, 2] != "-") ||
        (cells[0, 0] == cells[1, 1] && cells[0, 0] != "-" && cells[1, 1] == cells[2, 2])
        )
    {
        Console.WriteLine($"Congratulation You Winner!");
        break;
    }
    
    if (currentPlayer != player1)
    {
        currentPlayer = player1;
        mark = "X";
    }
    else
    {
        currentPlayer = player2;
        mark = "O";
    }
    Console.Write("\n");

    counter++;
    Console.WriteLine(counter);
    if (counter == 10)
    {
        Console.WriteLine("Congratulation Play Draw!");

        break;
    }


    int[] coordinate = getCoordinate(currentPlayer, cells);
    if (coordinate[0] == 77 )
    {
        Console.WriteLine($"Game played Draw!");
        break;
    }

    cells[coordinate[0], coordinate[1]] = mark;

    drawBoard(cells);
}


Console.WriteLine($"Congratulation {currentPlayer.ToUpper()}, You Wiiiiin!");

static int[] getCoordinate(string currentPlayer, string[,] cells)
{

    while (true)
    {

        try
        {
            Console.Write($"{currentPlayer.ToUpper()} enter position (x,y) or (x y): ");
            string text = Console.ReadLine();
            string[] subs = text.Trim().Split(' ', ',');
            int x = Convert.ToInt16(subs[0]);
            int y = Convert.ToInt16(subs[1]);
            if ((x < 0 || x > 2) || (y < 0 || y > 2))
                throw new Exception("Range is 0 - 2.");
            if (cells[x,y] != "-")
            {
                throw new Exception("This place is occupied.");
            }
            int[] coordinate = { x, y };
            return coordinate;
        }
        catch
        {
            Console.WriteLine("oeeeeeeeeee!");
            continue;
        }
    }
}

static void drawBoard(string[,] cells)
{
    for (int i = 0; i < 3; i++)
    {
        Console.Write("\n");
        for (int j = 0; j < 3; j++)
            Console.Write(cells[i, j] + " ");
    }
    Console.Write("\n");
}
