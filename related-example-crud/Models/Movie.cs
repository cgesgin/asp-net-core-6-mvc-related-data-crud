namespace related_example_crud.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string ? Title { get; set; }

        public List<MovieGenre>? MovieGenres { get; set; }
    }
}
