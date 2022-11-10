using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Services;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Movie>>> Get()
        {

            return Ok( await _movieService.GetAllMovies());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> Get(int id)
        {
            var movie = await _movieService.GetSingleMovie(id);
            if (movie == null)
                return BadRequest("movie not found");
            return Ok(movie);
        }

        [HttpPost]
        public async Task<ActionResult<List<Movie>>> AddMovie(Movie movie)
        {
            var result = await _movieService.AddMovie(movie);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<List<Movie>>> UpdateMovie(int id, Movie request)
        {
            var result = await _movieService.UpdateMovie(id, request);
            if (result is null)
                return NotFound("Hero not found.");

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Movie>>> Delete(int id)
        {
            var result = await _movieService.DeleteMovie(id);
            if (result is null)
                return NotFound("Hero not found.");

            return Ok(result);
        }
    }
}
