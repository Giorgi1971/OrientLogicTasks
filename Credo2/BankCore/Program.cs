// See https://aka.ms/new-console-template for more information
using BankCore.Data;

using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
optionsBuilder.UseSqlServer(
    "Server = localhost; " +
                    "Database = CredoProjectDb; " +
                    "User Id=sa; " +
                    "Password=HardT0Gue$$Pa$$word; " +
                    "TrustedConnection=true;" +
                    "Encrypt=False;"
    );

var db = new AppDbContext(optionsBuilder.Options);

db.Customers.Add(new Customer { Name = "Giorgi" });
