namespace related_example_crud.Models
{
    public class Course
    {
        public int Id { get; set; }

        public string ?Title { get; set; }

        public int? Credits { get; set; }

        public int ?DepartmentID { get; set; }

        public Department ?Department { get; set; }
    }
}
