namespace related_example_crud.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string ? Name { get; set; }
        public DateTime ? StartDate { get; set; }
        public ICollection<Course> ? Courses { get; set; }
    }
}