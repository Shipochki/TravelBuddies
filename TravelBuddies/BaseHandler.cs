namespace TravelBuddies.Application
{
	using Microsoft.AspNetCore.Identity;
	using TravelBuddies.Application.Repository;
	using TravelBuddies.Domain.Entities;

	public abstract class BaseHandler
	{
        protected readonly IRepository _repository;
        protected readonly UserManager<ApplicationUser> _userManager;
        protected readonly RoleManager<IdentityRole> _roleManager;

        protected BaseHandler(
            IRepository repository
            , UserManager<ApplicationUser> userManager
            , RoleManager<IdentityRole> roleManager)
        {
            _repository = repository;
            _userManager = userManager;
            _roleManager = roleManager;
        }
    }
}
