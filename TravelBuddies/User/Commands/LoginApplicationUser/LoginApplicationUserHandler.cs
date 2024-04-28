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
    using TravelBuddies.Application.Exceptions;
    using TravelBuddies.Application.Repository;
    using TravelBuddies.Domain.Entities;
    using static TravelBuddies.Application.Exceptions.Messages.ExceptionMessages;

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

            if (user != null && await _userManager.CheckPasswordAsync(user, request.Password))
            {
                await _signInManager.SignInAsync(user, isPersistent: false);

                string token = GenerateJwtToken(user);

                return token;
            }
            else
            {
                throw new InvalidLoginException(InvalidLoginMessage);
            }
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {

            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.NameId, user.Id),
            // Add additional claims as needed (e.g., roles, custom claims)
            };

            foreach (var role in _userManager.GetRolesAsync(user).Result)
            {
                claims.Add(new Claim("role", role));
            }

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
				expires: DateTime.UtcNow.AddHours(1),
				signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}