using System;

namespace CredoProject.Core.Models.AuthRequests
{
    public class RegisterUserRequest
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}

