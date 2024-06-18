namespace CapstoneDewarupa.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Image { get; set; }
        public required string Category { get; set; }
        public required string Slug { get; set; }
        public required DateTime CreateDate { get; set; }
    }
}
