using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModerationCrudApp.Data;
using ModerationCrudApp.Models;
using System.Threading.Tasks;

namespace ModerationCrudApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly AppDbContext _context;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, AppDbContext context, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    // Save additional user information
                    var appUser = new User
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        DateOfBirth = model.DateOfBirth,
                        NumberOfDryDays = model.NumberOfDryDays,
                        DrinksPerWeek = model.DrinksPerWeek,
                        GoalReduceFrequency = model.GoalReduceFrequency,
                        GoalReduceAmount = model.GoalReduceAmount,
                        ReduceByDrinks = model.ReduceByDrinks,
                        ReduceByDays = model.ReduceByDays
                    };
                    _context.Users.Add(appUser);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation("User registered successfully with email {Email}", model.Email);

                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                    _logger.LogError("Error registering user: {Error}", error.Description);
                }
            }

            _logger.LogWarning("Invalid model state for user registration");
            return View(model);
        }
    }
}
