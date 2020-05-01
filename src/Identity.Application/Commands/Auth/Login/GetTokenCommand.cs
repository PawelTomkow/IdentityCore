namespace Identity.Application.Commands.Auth.Login
{
    public class GetTokenCommand
    {
        public string IdRequest { get; set; }
        public int UserId { get; set; }
    }
}