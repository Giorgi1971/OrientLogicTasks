using System;

namespace CredoProject.Core.Models.AuthRequests
{
    public class ResetPasswordRequest
    {
        public int UserId { get; set; }
        public string Token { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
