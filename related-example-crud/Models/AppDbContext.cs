using Microsoft.EntityFrameworkCore;
using related_example_crud.Models;

namespace related_example_crud.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Movie> Movie { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<MovieGenre> MovieGenre { get; set; }
        public DbSet<Course>? Courses { get; set; }
        public DbSet<Department>? Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<MovieGenre>().HasKey(bc => new { bc.MovieId, bc.GenreId });
           
            modelBuilder.Entity<MovieGenre>().HasOne(bc => bc.Movie)
                                             .WithMany(b => b.MovieGenres)
                                             .HasForeignKey(bc => bc.MovieId);
           
            modelBuilder.Entity<MovieGenre>()   
                .HasOne(bc => bc.Genre)
                .WithMany(c => c.MovieGenres)
                .HasForeignKey(bc => bc.GenreId);


            base.OnModelCreating(modelBuilder);
        }
 

        

        
    }
}
