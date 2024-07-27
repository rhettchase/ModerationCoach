using Microsoft.AspNetCore.Identity;
using System;

namespace ModerationCrudApp.Data
{
    public class ApplicationUser : IdentityUser
    {
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
}
