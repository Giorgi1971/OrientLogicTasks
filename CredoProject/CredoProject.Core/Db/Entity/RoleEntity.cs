using System;
using Microsoft.AspNetCore.Identity;

namespace CredoProject.Core.Db.Entity
{
    public class RoleEntity:IdentityRole<int>
    {
        public RoleEntity()
        {
        }
    }
}

