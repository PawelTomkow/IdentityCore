namespace Identity.Application.Commands
{
    public class GetTokenCommand
    {
        public string IdRequest { get; set; }
        public int UserId { get; set; }
    }
}