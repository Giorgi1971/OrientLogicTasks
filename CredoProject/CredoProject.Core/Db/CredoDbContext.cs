using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CredoProject.Core.Db.Entity;
using Microsoft.AspNetCore.Identity;


namespace CredoProject.Core.Db
{
    public class CredoDbContext : IdentityDbContext<UserEntity, RoleEntity, int>
    {
        public DbSet<OperatorEntity> OperatorEntities { get; set; }
        public DbSet<UserEntity> CustomerEntities { get; set; }
        public DbSet<AccountEntity> AccountEntities { get; set; }
        public DbSet<CardEntity> CardEntities { get; set; }
        public DbSet<TransactionEntity> TransactionEntities { get; set; }
        public DbSet<SendEmailRequestEntity> SendEmailRequestEntities { get; set; }

        public CredoDbContext(DbContextOptions<CredoDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RoleEntity>().HasData(
                new RoleEntity { Id = 1, Name = "Manager", NormalizedName = "MANAGER" },
                new RoleEntity { Id = 2, Name = "Customer", NormalizedName = "CUSTOMER" },
                new RoleEntity { Id = 3, Name = "Operator", NormalizedName = "OPERATOR" },
                new RoleEntity { Id = 4, Name = "Admin", NormalizedName = "ADMIN" }
            );

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

            var userName = "gio2@gmail.com";
            var password = "pas123";
            var operator1 = new OperatorEntity { Id = 1, Email = userName, UserName = userName };

            var hasher = new PasswordHasher<OperatorEntity>();
            operator1.PasswordHash = hasher.HashPassword(operator1, password);
            modelBuilder.Entity<OperatorEntity>().HasData(operator1);

            var hasherUser = new PasswordHasher<UserEntity>();

            var cust1 = new UserEntity { Id = 1, FirstName = "Gio", LastName = "Mas",
                BirthDate = DateTime.Parse("1971-11-26"), Email = "gio5@gmail.com", PersonalNumber = "01030019697" };
            cust1.PasswordHash = hasherUser.HashPassword(cust1, password);

            var cust2 = new UserEntity { Id = 2, FirstName = "Nino", LastName = "Chale",
                BirthDate = DateTime.Parse("1978-03-31"), Email = "nino@gmail.com", PersonalNumber = "01015003600" };
            cust2.PasswordHash = hasherUser.HashPassword(cust2, password);

            var cust3 = new UserEntity { Id = 3, FirstName = "Niko", LastName = "Mas",
                BirthDate = DateTime.Parse("2017-12-09"), Email = "nikoCha@gmail.com", PersonalNumber = "01015008765" };
            cust3.PasswordHash = hasherUser.HashPassword(cust3, password);

            modelBuilder.Entity<UserEntity>().HasData(cust1, cust2, cust3);

            modelBuilder.Entity<IdentityUserRole<int>>().HasData(new[] {
                new IdentityUserRole<int> { UserId = 1, RoleId = 3 },
                new IdentityUserRole<int> { UserId = 2, RoleId = 2 },
                new IdentityUserRole<int> { UserId = 3, RoleId = 2 },
            });
        }
    }   
}

