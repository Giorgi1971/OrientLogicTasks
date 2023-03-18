using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CredoProject.Core.Db.Entity;
using Microsoft.AspNetCore.Identity;


namespace CredoProject.Core.Db
{
    public class CredoDbContext : IdentityDbContext<UserEntity, RoleEntity, int>
    {
        public DbSet<OperatorEntity> OperatorEntities { get; set; }
        public DbSet<UserEntity> UserEntities { get; set; }
        public DbSet<AccountEntity> AccountEntities { get; set; }
        public DbSet<CardEntity> CardEntities { get; set; }
        public DbSet<TransactionEntity> TransactionEntities { get; set; }
        public DbSet<SendEmailRequestEntity> SendEmailRequestEntities { get; set; }
        public DbSet<ExchangeEntity> exchangeEntities { get; set; }
        
        public CredoDbContext(DbContextOptions<CredoDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TransactionEntity>()
                .HasKey(sc => new { sc.AccountFromId, sc.AccountToId });

            modelBuilder.Entity<TransactionEntity>()
                .HasOne(sc => sc.AccountEntityFrom)
                .WithMany(s => s.FromTransactionEntities)
                .HasForeignKey(sc => sc.AccountFromId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TransactionEntity>()
                .HasOne(sc => sc.AccountEntityTo)
                .WithMany(c => c.ToTransactionEntities)
                .HasForeignKey(sc => sc.AccountToId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<RoleEntity>().HasData(
                new RoleEntity { Id = 1, Name = "api-manager", NormalizedName = "API-MANAGER" },
                new RoleEntity { Id = 2, Name = "api-user", NormalizedName = "API-USER" },
                new RoleEntity { Id = 3, Name = "api-operator", NormalizedName = "API-OPERATOR" },
                new RoleEntity { Id = 4, Name = "api-admin", NormalizedName = "API-ADMIN" }
            ); ;

            modelBuilder.Entity<ExchangeEntity>().HasData(
                new ExchangeEntity { Id = 1, currencyFrom = Currency.GEL, currencyTo = Currency.GEL, date = DateTime.Now, rate = 1 },
                new ExchangeEntity { Id = 2, currencyFrom = Currency.GEL, currencyTo = Currency.USD, date = DateTime.Now, rate = 0.361m },
                new ExchangeEntity { Id = 3, currencyFrom = Currency.GEL, currencyTo = Currency.EUR, date = DateTime.Now, rate = 0.3636m },
                new ExchangeEntity { Id = 4, currencyFrom = Currency.USD, currencyTo = Currency.USD, date = DateTime.Now, rate = 1 },
                new ExchangeEntity { Id = 5, currencyFrom = Currency.USD, currencyTo = Currency.GEL, date = DateTime.Now, rate = 2.77m },
                new ExchangeEntity { Id = 6, currencyFrom = Currency.USD, currencyTo = Currency.EUR, date = DateTime.Now, rate = 0.98m },
                new ExchangeEntity { Id = 7, currencyFrom = Currency.EUR, currencyTo = Currency.EUR, date = DateTime.Now, rate = 1 },
                new ExchangeEntity { Id = 8, currencyFrom = Currency.EUR, currencyTo = Currency.GEL, date = DateTime.Now, rate = 2.87m },
                new ExchangeEntity { Id = 9, currencyFrom = Currency.EUR, currencyTo = Currency.USD, date = DateTime.Now, rate = 1.0071m }
            );


            var userName = "gio2@gmail.com";
            var password = "pas123";
            var operator1 = new OperatorEntity { Id = 1, Email = userName, UserName = userName };

            var hasher = new PasswordHasher<OperatorEntity>();
            operator1.PasswordHash = hasher.HashPassword(operator1, password);
            modelBuilder.Entity<OperatorEntity>().HasData(operator1);

            var hasherUser = new PasswordHasher<UserEntity>();

            var cust1 = new UserEntity
            {
                Id = 1,
                FirstName = "Gio",
                LastName = "Mas",
                NormalizedEmail = "GIO5@GMAIL.COM",
                BirthDate = DateTime.Parse("1971-11-26"),
                Email = "gio5@gmail.com",
                PersonalNumber = "01030019697"
            };
            cust1.PasswordHash = hasherUser.HashPassword(cust1, password);

            var cust2 = new UserEntity
            {
                Id = 2,
                FirstName = "Nino",
                LastName = "Chale",
                NormalizedEmail = "NINO@GMAIL.COM",
                BirthDate = DateTime.Parse("1978-03-31"),
                Email = "nino@gmail.com",
                PersonalNumber = "01015003600"
            };
            cust2.PasswordHash = hasherUser.HashPassword(cust2, password);

            var cust3 = new UserEntity
            {
                Id = 3,
                FirstName = "Niko",
                LastName = "Mas",
                NormalizedEmail = "NIKO@GMAIL.COM",
                BirthDate = DateTime.Parse("2017-12-09"),
                Email = "niko@gmail.com",
                PersonalNumber = "01015008765"
            };
            cust3.PasswordHash = hasherUser.HashPassword(cust3, password);

            modelBuilder.Entity<UserEntity>().HasData(cust1, cust2, cust3);

            modelBuilder.Entity<IdentityUserRole<int>>().HasData(new[] {
                new IdentityUserRole<int> { UserId = 1, RoleId = 3 },
                new IdentityUserRole<int> { UserId = 2, RoleId = 2 },
                new IdentityUserRole<int> { UserId = 3, RoleId = 2 },
            });

            modelBuilder.Entity<AccountEntity>().HasData(
                new AccountEntity { AccountEntityId = 1, Currency = Currency.GEL, Amount = 5000, CreateAt = DateTime.Now, CustomerEntityId = 2, IBAN = "AL35202111090000000001234567" },
                new AccountEntity { AccountEntityId = 2, Currency = Currency.USD, Amount = 5000, CreateAt = DateTime.Now, CustomerEntityId = 2, IBAN = "AD1400080001001234567890" },
                new AccountEntity { AccountEntityId = 3, Currency = Currency.EUR, Amount = 5000, CreateAt = DateTime.Now, CustomerEntityId = 2, IBAN = "AT483200000012345864" },
                new AccountEntity { AccountEntityId = 4, Currency = Currency.GEL, Amount = 4000, CreateAt = DateTime.Now, CustomerEntityId = 3, IBAN = "AZ77VTBA00000000001234567890" },
                new AccountEntity { AccountEntityId = 5, Currency = Currency.USD, Amount = 3000, CreateAt = DateTime.Now, CustomerEntityId = 3, IBAN = "BH02CITI00001077181611" }
                );

            modelBuilder.Entity<CardEntity>().HasData(
                new CardEntity { CardEntityId = 1, OwnerName = "Name", OwnerLastName = "LasName", AccountEntityId = 1, CardNumber = "Card01", CVV = "321", RegistrationDate = DateTime.Now, ExpiredDate = "03-2024", PIN = "4444", Status = Status.Active },
                new CardEntity { CardEntityId = 2, OwnerName = "Name", OwnerLastName = "LasName", AccountEntityId = 2, CardNumber = "Card02", CVV = "321", RegistrationDate = DateTime.Now, ExpiredDate = "03-2024", PIN = "4444", Status = Status.Active },
                new CardEntity { CardEntityId = 3, AccountEntityId = 1, CardNumber = "Card03", OwnerName = "Name", OwnerLastName = "LasName", CVV = "321", RegistrationDate = DateTime.Now, ExpiredDate = "03-2022", PIN = "4444", Status = Status.Active },
                new CardEntity { CardEntityId = 4, AccountEntityId = 3, CardNumber = "Card04", CVV = "321", RegistrationDate = DateTime.Now, OwnerName = "Name", OwnerLastName = "LasName", ExpiredDate = "03-2024", PIN = "4444", Status = Status.Active },
                new CardEntity { CardEntityId = 5, AccountEntityId = 4, CardNumber = "Card05", CVV = "321", RegistrationDate = DateTime.Now, ExpiredDate = "03-2024", PIN = "4444", OwnerName = "Name", OwnerLastName = "LasName", Status = Status.Active }
                );
        }
    }   
}

