using System.ComponentModel.DataAnnotations;

namespace ModerationCrudApp.Models
{
    public class RegisterViewModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.Date)]
        public DateOnly? DateOfBirth { get; set; }

        public int? NumberOfDryDays { get; set; }
        public int? DrinksPerWeek { get; set; }
        public bool GoalReduceFrequency { get; set; }  // Change to non-nullable
        public bool GoalReduceAmount { get; set; }  // Change to non-nullable
        public int? ReduceByDrinks { get; set; }
        public int? ReduceByDays { get; set; }
    }
}
