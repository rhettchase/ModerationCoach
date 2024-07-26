namespace ModerationCrudApp.Data
{
    public class User
    {
        public int Id { get; set; }
        public string ?FirstName { get; set; }
        public string ?LastName { get; set; }
        public string ?Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        // Add other properties as needed
    }
}
