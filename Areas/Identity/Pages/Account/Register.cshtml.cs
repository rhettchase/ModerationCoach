using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ModerationCrudApp.Data;
using ModerationCrudApp.Models;

namespace ModerationCrudApp.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly AppDbContext _context;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }

        [BindProperty]
        public RegisterViewModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            Input = new RegisterViewModel(); // Ensure Input is initialized
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            ReturnUrl ??= Url.Content("~/");
            if (ModelState.IsValid)
            {
                // Ensure the email is not null
                if (string.IsNullOrEmpty(Input.Email))
                {
                    ModelState.AddModelError(string.Empty, "Email cannot be null.");
                    return Page();
                }

                var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var appUser = new User
                    {
                        FirstName = Input.FirstName,
                        LastName = Input.LastName,
                        Email = Input.Email,
                        DateOfBirth = Input.DateOfBirth,
                        NumberOfDryDays = Input.NumberOfDryDays,
                        DrinksPerWeek = Input.DrinksPerWeek,
                        GoalReduceFrequency = Input.GoalReduceFrequency,
                        GoalReduceAmount = Input.GoalReduceAmount,
                        ReduceByDrinks = Input.ReduceByDrinks,
                        ReduceByDays = Input.ReduceByDays
                    };

                    _context.Users.Add(appUser);
                    await _context.SaveChangesAsync();

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }
    }
}
