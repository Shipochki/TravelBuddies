﻿namespace TravelBuddies.Application.User.Commands.LoginApplicationUser
{
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using TravelBuddies.Application.Common.Exceptions;
    using TravelBuddies.Domain.Entities;
    using static TravelBuddies.Application.Common.Exceptions.Messages.ExceptionMessages;
    using TravelBuddies.Application.Common.Interfaces.Repository;

    public class LoginApplicationUserHandler : BaseHandler, IRequestHandler<LoginApplicationUserCommand, string>
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginApplicationUserHandler(
            IRepository repository
            , UserManager<ApplicationUser> userManager
            , RoleManager<IdentityRole> roleManager
            , IConfiguration configuration
            , SignInManager<ApplicationUser> signInManager)
            : base(repository, userManager, roleManager)
        {
            _configuration = configuration;
            _signInManager = signInManager;
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

            string token = GenerateJwtToken(user);

            return token;
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>()
            {

                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, user.Id),
                new Claim("fullname", $"{user.FirstName} {user.LastName}"),
                
                // Add additional claims as needed (e.g., roles, custom claims)
            };

            if(user.ProfilePictureLink != null)
            {
                claims.Add(new Claim("profilePictureLink", user.ProfilePictureLink));
            }

            foreach (var role in _userManager.GetRolesAsync(user).Result)
            {
                claims.Add(new Claim("role", role));
            }

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
				expires: DateTime.UtcNow.AddHours(24),
				signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
