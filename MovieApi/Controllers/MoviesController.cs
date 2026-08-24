using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Models;
using MovieApi.Dtos;

namespace MovieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieApiContext _context;

        public MoviesController(MovieApiContext context)
        {
            _context = context;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies()
        {
            var movies = await _context.Movie.ToListAsync();
            var movieDtos = movies.Select(m => new MovieDto
            {
                Id = m.Id,
                Title = m.Title,
                Genre = m.Genre,
                Director = m.Director,
                ReleaseYear = m.ReleaseYear,
                DurationMinutes = m.DurationMinutes,
                Rating = m.Rating,
                Description = m.Description
            }).ToList();
            return Ok(movieDtos);


        }

        // GET: api/Movies/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<MovieDto>> GetMovie(int id)
        {
            var movie = await _context.Movie.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Genre = movie.Genre,
                Director = movie.Director,
                ReleaseYear = movie.ReleaseYear,
                DurationMinutes = movie.DurationMinutes,
                Rating = movie.Rating,
                Description = movie.Description
            };
              
        }

        // POST: api/Movies
        [HttpPost]
        public async Task<ActionResult<MovieDto>> CreateMovie(CreateMovieDto createMovieDto)
        {
            var movie = new Movie
            {
                Title = createMovieDto.Title,
                Genre = createMovieDto.Genre,
                Director = createMovieDto.Director,
                ReleaseYear = createMovieDto.ReleaseYear,
                DurationMinutes = createMovieDto.DurationMinutes,
                Rating = createMovieDto.Rating,
                Description = createMovieDto.Description
            };
            _context.Movie.Add(movie);
            await _context.SaveChangesAsync();
            var movieDto = new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Genre = movie.Genre,
                Director = movie.Director,
                ReleaseYear = movie.ReleaseYear,
                DurationMinutes = movie.DurationMinutes,
                Rating = movie.Rating,
                Description = movie.Description
            };
            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movieDto);

        }

        
        // DELETE: api/Movies/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movie.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}