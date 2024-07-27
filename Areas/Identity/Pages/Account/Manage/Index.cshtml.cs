using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModerationCrudApp.Data;

namespace ModerationCrudApp.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public DateTime? DateOfBirth { get; set; }
            public int? NumberOfDryDays { get; set; }
            public int? DrinksPerWeek { get; set; }
            public bool GoalReduceFrequency { get; set; } = false;  // Default value
            public bool GoalReduceAmount { get; set; } = false;  // Default value
            public int? ReduceByDrinks { get; set; }
            public int? ReduceByDays { get; set; }
        }

        [BindProperty]
        public InputModel Input { get; set; }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var email = await _userManager.GetEmailAsync(user);

            Username = userName;

            Input = new InputModel
            {
                Email = email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                NumberOfDryDays = user.NumberOfDryDays,
                DrinksPerWeek = user.DrinksPerWeek,
                GoalReduceFrequency = user.GoalReduceFrequency,
                GoalReduceAmount = user.GoalReduceAmount,
                ReduceByDrinks = user.ReduceByDrinks,
                ReduceByDays = user.ReduceByDays
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            user.FirstName = Input.FirstName;
            user.LastName = Input.LastName;
            user.DateOfBirth = Input.DateOfBirth;
            user.NumberOfDryDays = Input.NumberOfDryDays;
            user.DrinksPerWeek = Input.DrinksPerWeek;
            user.GoalReduceFrequency = Input.GoalReduceFrequency;
            user.GoalReduceAmount = Input.GoalReduceAmount;
            user.ReduceByDrinks = Input.ReduceByDrinks;
            user.ReduceByDays = Input.ReduceByDays;

            await _userManager.UpdateAsync(user);

            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }

    }
}
