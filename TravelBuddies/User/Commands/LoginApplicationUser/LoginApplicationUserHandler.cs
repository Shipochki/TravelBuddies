namespace TravelBuddies.Application.User.Commands.LoginApplicationUser
{
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using System.Threading;
    using System.Threading.Tasks;
    using TravelBuddies.Domain.Entities;
    using static TravelBuddies.Application.Common.Exceptions.Messages.ExceptionMessages;
    using TravelBuddies.Application.Common.Interfaces.Repository;
	using TravelBuddies.Application.Common.Interfaces;
	using TravelBuddies.Application.Common.Exceptions.NotFound;
	using TravelBuddies.Application.Common.Exceptions.BadRequest;

	public class LoginApplicationUserHandler : BaseHandler, IRequestHandler<LoginApplicationUserCommand, string>
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAuthTokenService _tokenService;

        public LoginApplicationUserHandler(
            IRepository repository
            , UserManager<ApplicationUser> userManager
            , RoleManager<IdentityRole> roleManager
            , IConfiguration configuration
            , SignInManager<ApplicationUser> signInManager
            , IAuthTokenService tokenService)
            : base(repository, userManager, roleManager)
        {
            _configuration = configuration;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<string> Handle(LoginApplicationUserCommand request, CancellationToken cancellationToken)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(request.Email);

            if(user == null)
            {
                throw new ApplicationUserNotFoundException(
                    string.Format(ApplicationUserNotFoundMessage, request.Email));
            }

            if (!await _userManager.CheckPasswordAsync(user, request.Password))
            {
                throw new InvalidLoginException(InvalidLoginMessage);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);

            string token = _tokenService.GenerateAccessToken(user, _configuration, _userManager);

            return token;
        }
    }
}
