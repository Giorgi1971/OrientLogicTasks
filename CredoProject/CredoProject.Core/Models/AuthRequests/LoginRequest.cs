using System;

namespace CredoProject.Core.Models.AuthRequests
{
    public class LoginRequest
    {
        public int userId { get; set; }
        public string Password { get; set; } = null!;
    }
}
