namespace Identity.Application.Commands.Management
{
    public class ChangeEmailCommand : ICommand
    {
        public int UserId { get; set; }
        public string NewEmail { get; set; }
    }
}