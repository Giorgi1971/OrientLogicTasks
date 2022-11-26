using BooksInShelf.Models;
using BooksInShelf.Services;

path1();
bool booking = true;

start(booking);

static void start(bool booking)
{
    while (booking)
    {
        string str = "0 show shelfs\t1 Get Shelf\t2 Create Shelf\t3 Delete Shelf\t4 Rename shelf\t9 Exit";
        startMenu(str, booking);
    }
}


static void startMenu(string str, bool booking)
{
    Console.WriteLine(str);
    var switch_on = Console.ReadLine();
    switch (switch_on)
    {
        case "0":
            ShelfService.showShelfs();
            break;
        case "1":
            Console.WriteLine("Enter Shelf Id");
            var shelf_id = int.Parse(Console.ReadLine()!);
            ShelfService.GetShelf(shelf_id);
            ShelfMenu(shelf_id);
            break;
        case "2":
            ShelfService.CreateShelf();
            break;
        case "3":
            ShelfService.DeleteShelf();
            break;
        case "4":
            ShelfService.RenameShelf();
            break;
        case "9":
            System.Environment.Exit(0);
            break;
        default:
            Console.WriteLine("Enter Correct Number!");
            break;
    }
}


static void ShelfMenu(int shelf_id)
{
    bool shelfing = true;
    while (shelfing)
    {
        Console.WriteLine("1 Create book\t2 Change shelf\t3 Delete book\t4 Back\t9 Exit.");
        var choose = Console.ReadLine();
        if (choose == "1")
        {
            ShelfService.AddBookInShelf(shelf_id);
        }
        else if (choose == "2")
            ShelfService.ChangeShelf();
        else if (choose == "3")
            ShelfService.RemoveBook();
        else if (choose == "4")
            break;
        else if (choose == "9")
            System.Environment.Exit(0);
        else
            Console.WriteLine("Enter Coorect Number!!");
    }
}

// აქ წინსაწინ თაროებს და წიგნებს ვქმნი რომ მაგალითისთვის მქონდეს
static void path1()
{
    Console.WriteLine("\t\t\t\tHello, World!");
    string[] book_list = { "Dostoevski", "Tolstoi", "Lirics", "Classics", "Adventure", "Fanny" };
    foreach (var item in book_list)
    {
        var shelf = new Shelf(item);
    }
    //var shelf4 = new Shelf("Classics");

    var book1 = new Book("Idiot", 1);
    var book2 = new Book("War", 2);
    var book3 = new Book("Peace", 2);
    var book4 = new Book("Gala1", 3);

    ShelfService.AddBookInShelf("Devils", 1);
    ShelfService.AddBookInShelf("Player", 1);

    Console.WriteLine(Shelf.Shelfs.Count);
}
