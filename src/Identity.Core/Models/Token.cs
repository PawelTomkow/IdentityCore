using System;

namespace Identity.Core.Models
{
    public class Token
    {
        public Token(User user, string idSession, string accessToken, string refreshToken, long experienceTime)
        {
            IdSession = idSession ?? throw new ArgumentNullException(nameof(idSession));
            AccessToken = accessToken ?? throw new ArgumentNullException(nameof(accessToken));
            RefreshToken = refreshToken ?? throw new ArgumentNullException(nameof(refreshToken));
            ExperienceTime = experienceTime;
            User = user ?? throw new ArgumentNullException(nameof(refreshToken));
        }

        protected Token() { }

        public int TokenId { get; set; }
        public string IdSession { get; protected set; }
        public string AccessToken { get; protected set; }
        public string RefreshToken { get; protected set; }
        public long ExperienceTime { get; protected set; }
        
        public User User { get; set; }
        
    }
}