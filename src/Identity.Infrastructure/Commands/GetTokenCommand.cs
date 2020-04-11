namespace Identity.Infrastructure.Commands
{
    public class GetTokenCommand
    {
        public string IdRequest { get; set; }
        public int UserId { get; set; }
    }
}