using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CredoProject.Core.Db
{
    public class CredoDbContextFactory : IDesignTimeDbContextFactory<CredoDbContext>
    {
        public CredoDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CredoDbContext>();
            optionsBuilder.UseSqlServer(args[0]);

            return new CredoDbContext(optionsBuilder.Options);
        }
    }
}   

// dotnet ef migrations add Initial -- "Server=localhost; Database = CredoProjectDb; User Id=sa; Password=HardT0Gue\$\$Pa\$\$word; Trusted_Connection=True;integrated security=False; Encrypt=False;"
// ეს მუშაობს და ამატებს ცხრილებს:
// dotnet ef database update -- "Server=localhost; Database = CredoProjectDb; User Id=sa; Password=HardT0Gue\$\$Pa\$\$word; Trusted_Connection=True;integrated security=False; Encrypt=False;"
