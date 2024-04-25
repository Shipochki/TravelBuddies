namespace TravelBuddies.Application.User.Commands.LoginApplicationUser
{
    using MediatR;

    public class LoginApplicationUserCommand : IRequest<string>
    {
        public LoginApplicationUserCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
