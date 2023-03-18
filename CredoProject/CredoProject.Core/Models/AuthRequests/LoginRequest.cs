﻿using System;
using System.ComponentModel.DataAnnotations;

namespace CredoProject.Core.Models.AuthRequests
{
    public class LoginRequest
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
