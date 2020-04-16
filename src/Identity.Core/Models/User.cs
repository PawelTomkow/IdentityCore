using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Identity.Core.Exceptions;

namespace Identity.Core.Models
{
    public class User
    {
        private static readonly Regex NameRegex = new Regex("^(?![_.-])(?!.*[_.-]{2})[a-zA-Z0-9._.-]+(?<![_.-])$");

        protected User()
        {
        }

        public User( string email, string username,
            string password, string salt)
        {
            SetEmail(email);
            SetUsername(username);
            SetPassword(password, salt);
            CreatedAt = DateTime.UtcNow;
        }

        public int Id { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public string Salt { get; protected set; }
        public string Username { get; protected set; }
        public IEnumerable<Role> Roles { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        public void SetUsername(string username)
        {
            if (!NameRegex.IsMatch(username))
                throw new DomainException("Username is invalid.");

            if (string.IsNullOrEmpty(username))
                throw new DomainException("Username is invalid.");

            Username = username.ToLowerInvariant();
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new DomainException("Email can not be empty.");
            if (Email == email) return;

            Email = email.ToLowerInvariant();
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetRole(IEnumerable<Role> role)
        {
            if (role.Any())
                throw new DomainException("Role can not be empty.");
            if (Roles.Any(role.Contains)) return;
            Roles = role;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetPassword(string password, string salt)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new DomainException("Password can not be empty.");
            if (string.IsNullOrWhiteSpace(salt))
                throw new DomainException("Salt can not be empty.");
            if (password.Length < IdentityUserConstants.PasswordMinLength)
                throw new DomainException("Password must contain at least 4 characters.");
            if (password.Length > IdentityUserConstants.PasswordMaxLength)
                throw new DomainException("Password can not contain more than 100 characters.");
            if (Password == password) return;
            Password = password;
            Salt = salt;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}