namespace CapstoneDewarupa.Models
{
    public class TambahCourse
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public IFormFile? Image { get; set; }
        public required string Category { get; set; }
        public required string Slug { get; set; }
        public required DateTime CreateDate { get; set; }
    }
}
