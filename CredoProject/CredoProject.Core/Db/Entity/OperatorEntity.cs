using Microsoft.AspNetCore.Identity;

namespace CredoProject.Core.Db.Entity
{
    public class OperatorEntity: IdentityUser<int>
    {
        public string? FullName { get; set; }
        public string? Password { get; set; }
    }
}
