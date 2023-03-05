﻿namespace Jamify.BE.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string? LastName { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime Birthday { get; set; }

        public string Password { get; set; }
    }
}
