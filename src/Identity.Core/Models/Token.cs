using System;

namespace Identity.Core.Models
{
    public class Token
    {
        public Token(User user, string idSession, string refreshToken)
        {
            IdSession = idSession ?? throw new ArgumentNullException(nameof(idSession));
            RefreshToken = refreshToken ?? throw new ArgumentNullException(nameof(refreshToken));
            User = user ?? throw new ArgumentNullException(nameof(refreshToken));
        }

        protected Token() { }

        public int TokenId { get; set; }
        public string IdSession { get; protected set; }
        public string RefreshToken { get; protected set; }
        
        public User User { get; set; }
        
    }
}