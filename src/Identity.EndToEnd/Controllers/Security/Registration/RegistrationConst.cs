namespace Identity.EndToEnd.Controllers.Security.Registration
{
    public static class RegistrationConst
    {
        //username
        public static readonly string CorrectUsername = "correctUsername";
        public static readonly string IncorrectUsername = "incorrectUsername";
        public static readonly string IncorrectCharsUsername = "inc()r3t\\/Username";
        public static readonly string EmptyUsername = string.Empty;
        public static readonly string NullUsername = null;
        
        //password
        public static readonly string CorrectPassword = "correctPassword";
        public static readonly string IncorrectPassword = "incorrectPassword";
        public static readonly string EmptyPassword = string.Empty;
        public static readonly string NullPassword = null; 
        
        //email
        public static readonly string CorrectEmail = "correct@email.com";
        public static readonly string IncorrectWithoutAtEmail = "incorrectmail.pl";
        public static readonly string IncorrectWithMoreAtNextToEachOtherEmail = "in@@correct.pl";
        public static readonly string IncorrectWithMoreAtEmail = "in@co@rrect.pl";
        public static readonly string IncorrectWithoutDomainEmail = "incorrect@.pl";
        public static readonly string IncorrectWithoutEndEmail = "incorrect@mail";
        public static readonly string IncorrectWithoutStartEmail = "@mail.com";
        
    }
}