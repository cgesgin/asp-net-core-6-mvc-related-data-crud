namespace related_example_crud.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public List<MovieGenre> ?MovieGenres { get; set; }
    }
}
