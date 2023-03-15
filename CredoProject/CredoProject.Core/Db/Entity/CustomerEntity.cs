using Microsoft.AspNetCore.Identity;

namespace CredoProject.Core.Db.Entity
{
    public class UserEntity: IdentityUser<int>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PersonalNumber { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        //public string? Email { get; set; }
        //public string? Hash { get; set; }

        public ICollection<AccountEntity>? AccountEntities { get; set; }
        //public ICollection<CardEntity> CardEntities { get; set; }
    }
}
