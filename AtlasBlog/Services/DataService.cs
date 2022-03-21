using AtlasBlog.Data;
using AtlasBlog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AtlasBlog.Services
{
    public class DataService
    {
        //Calling a method or an instruction that executes the migrations
        readonly ApplicationDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<BlogUser> _userManager;
        private readonly IConfiguration _configuration;

        public DataService(ApplicationDbContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<BlogUser> userManager, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task SetupDbAsync()
        {
            //Run the Migrations async
            await _dbContext.Database.MigrateAsync();

            //Add a few Roles into the system
            await SeedRolesAsync();

            //Add a few Users into the system
            await SeedUsersAsync();
        }

        private async Task SeedRolesAsync()
        {
            if (_roleManager.Roles.Count() == 0)
            {
                await _roleManager.CreateAsync(new IdentityRole("Administrator"));
                await _roleManager.CreateAsync(new IdentityRole("Moderator"));
            }
        }
        private async Task SeedUsersAsync()
        {
            try
            {
                BlogUser user = new()
                {
                    UserName = "bmart1108@mailinator.com",
                    Email = "bmart1108@mailinator.com",
                    FirstName = "Brandon",
                    LastName = "Martin",
                    DisplayName = "Bmart",
                    PhoneNumber = "9106036287",
                    EmailConfirmed = true
                };

                var newUser = await _userManager.FindByEmailAsync(user.Email);
                if (newUser is null)
                {
                    var adminPassword = _configuration["DataService:AdminPassword"];
                    await _userManager.CreateAsync(user, adminPassword);
                    await _userManager.AddToRoleAsync(user, "Administrator");
                }

                user = new()
                {
                    UserName = "DrewRussell@mailinator.com",
                    Email = "DrewRussell@mailinator.com",
                    FirstName = "Andrew",
                    LastName = "Russell",
                    DisplayName = "Prof",
                    PhoneNumber = "3366667777",
                    EmailConfirmed = true
                };

                newUser = await _userManager.FindByEmailAsync(user.Email);
                if (newUser is null)
                {
                    await _userManager.CreateAsync(user, _configuration["DataService:ModPassword"]);
                    await _userManager.AddToRoleAsync(user, "Moderator");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }

}