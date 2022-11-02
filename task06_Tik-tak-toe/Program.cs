using System;

string[,] cells;
cells = new string[3, 3] { { "-", "-", "-" }, { "-", "-", "-" }, { "-", "-", "-" } };

Console.Write("Enter Player1 name: ");
// არ მუშაობს ??, დამჭირდა if გამოყენება. რა უნდა?
string player1 = Console.ReadLine() ?? "Player 1";
if (player1 == "")
    player1 = "Player 1";
Console.WriteLine($"Welcome: {player1}");

Console.Write("Enter Player2 name: ");
string player2 = Console.ReadLine() ?? "Player 2";
if (player2 == "")
    player2 = "Player2";
Console.WriteLine($"Welcome {player2}");

string currentPlayer = player2;


string mark = "O";

drawBoard(cells);
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
        Console.WriteLine($"\nCongratulations, {currentPlayer.ToUpper()} won!");
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
    Console.WriteLine();

    if (counter == 9)
    {
        Console.WriteLine("It's a Draw!");
        break;
    }
        counter++;

    int[] coordinate = getCoordinate(currentPlayer, cells);
    cells[coordinate[0], coordinate[1]] = mark;

    drawBoard(cells);
}


static int[] getCoordinate(string currentPlayer, string[,] cells)
{
    while (true)
    {
        try
        {
            Console.Write($"{currentPlayer.ToUpper()} enter position (x,y) or (x y): ");
            string text = Console.ReadLine() ?? "77 77";
            string[] subs = text.Trim().Split(' ', ',');
            int x = Convert.ToInt16(subs[0]);
            int y = Convert.ToInt16(subs[1]);
            if ((x < 0 || x > 2) || (y < 0 || y > 2))
                throw new IndexOutOfRangeException();
            if (cells[x,y] != "-")
            {
                throw new TypeAccessException("This place is occupied.");
            }
            int[] coordinate = { x, y };
            return coordinate;
        }
        catch (IndexOutOfRangeException e)
        {
            Console.WriteLine($"{e.Message}\b: 0, 1, 2.");
        }
        catch (TypeAccessException)
        {
            Console.WriteLine($"Cannot set position. Cell isn't empty.");
        }
        catch
        {
            Console.WriteLine("Please, Enter valid position!");
            continue;
        }
    }
}

static void drawBoard(string[,] cells)
{
    for (int i = 0; i < 3; i++)
    {
        for (int j = 0; j < 3; j++)
            Console.Write(cells[i, j] + " ");
        if (i != 2)
            Console.Write("\n");
    }
}
