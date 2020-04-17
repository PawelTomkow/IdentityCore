using System;

namespace Identity.Core.Models
{
    public class Token
    {
        public Token(int userId, string idSession, string accessToken, string refreshToken, long experienceTime)
        {
            UserId = userId;
            IdSession = idSession ?? throw new ArgumentNullException(nameof(idSession));
            AccessToken = accessToken ?? throw new ArgumentNullException(nameof(accessToken));
            RefreshToken = refreshToken ?? throw new ArgumentNullException(nameof(refreshToken));
            ExperienceTime = experienceTime;
        }

        protected Token() { }

        public int TokenId { get; set; }
        public int UserId { get; protected set; }
        public string IdSession { get; protected set; }
        public string AccessToken { get; protected set; }
        public string RefreshToken { get; protected set; }
        public long ExperienceTime { get; protected set; }
        
        public User User { get; set; }
        
    }
}