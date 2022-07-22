using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using related_example_crud.Models;

namespace related_example_crud.Controllers
{
    public class MovieController : Controller
    {
        private readonly AppDbContext _context;
        public MovieController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var movies = await _context.Movie.Include(m => m.MovieGenres).ThenInclude(s=>s.Genre).ToListAsync();
           
            return View(movies);
        }
        public IActionResult Create()
        {
            List<SelectListItem> selectListItems = _context.Genre.Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Id.ToString()
            }).ToList();
            ViewBag.GenreId = selectListItems;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Movie movie, List<int> GenreIdList)
        {
            _context.Movie.Add(movie);

            _context.SaveChanges();
            foreach (var item in GenreIdList)
            {
                MovieGenre movieGenre = new MovieGenre()
                {
                    GenreId = item,
                    MovieId = movie.Id,
                    Genre = _context.Genre.Where(x => x.Id == item).FirstOrDefault(),
                    Movie = movie
                };
                _context.MovieGenre.Add(movieGenre);
            }
            _context.SaveChanges();
            return Redirect("Index");
        }

        public IActionResult Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var movies =  _context.Movie.Find(id);
            var moviesinGenre=_context.MovieGenre.Where(m => m.MovieId == id);
            if (movies == null)
            {
                return NotFound();
            }

            List<SelectListItem> selectListItems = _context.Genre.Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Id.ToString(),
               
            }).ToList();
            
            foreach (var selectedItem in selectListItems)
            {
                foreach (var item in moviesinGenre)
                {
                    if (selectedItem.Value == item.GenreId.ToString())
                        selectedItem.Selected = true;
                }
            }
            ViewBag.GenreId = selectListItems;

            return View(movies);            
        }

        [HttpPost]
        public IActionResult UpdatePost(Movie movie, List<int> GenreIdList)
        {
            _context.Movie.Update(movie);
            var movieGenres = _context.MovieGenre.Where(m => m.MovieId == movie.Id);
            foreach (var item in movieGenres)
            {
                _context.MovieGenre.Remove(item);
            }   
           
            _context.SaveChanges();
            foreach (var item in GenreIdList)
            {                
                MovieGenre movieGenre = new MovieGenre()
                {
                    GenreId = item,
                    MovieId = movie.Id,
                    Genre = _context.Genre.Where(x => x.Id == item).FirstOrDefault(),
                    Movie = movie
                };
                _context.MovieGenre.Add(movieGenre);
            }
            _context.SaveChanges();
            return Redirect("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _context.Movie.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _context.Movie.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _context.Movie.Remove(obj);
            _context.SaveChanges();
            return Redirect("Index");
        }
    }
}
