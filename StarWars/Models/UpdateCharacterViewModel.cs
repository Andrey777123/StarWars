


namespace StarWars.Models
{
    public class UpdateCharacterViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string OriginalName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Planet { get; set; }
        public string Sex { get; set; }
        public string Race { get; set; }
        public int Height { get; set; }
        public string HairColor { get; set; }
        public string EyeColor { get; set; }
        public string? Description { get; set; }
    }
}
