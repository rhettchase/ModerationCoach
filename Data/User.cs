namespace ModerationCrudApp.Data
{
    public class User
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public int? NumberOfDryDays { get; set; }
        public int? DrinksPerWeek { get; set; }
        public bool? GoalReduceFrequency { get; set; }
        public bool? GoalReduceAmount { get; set; }
        public int? ReduceByDrinks { get; set; }
        public int? ReduceByDays { get; set; }
    }
}
