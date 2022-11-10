using Microsoft.EntityFrameworkCore;

namespace MoviesAPI.Services
{
    public class MovieService : IMovieService
    {

        private readonly DataContext _context;

        public MovieService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Movie>> AddMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return await _context.Movies.ToListAsync();
        }

        public async Task<List<Movie>?> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
                return null;

            _context.Movies.Remove(movie);

            await _context.SaveChangesAsync();


            return await _context.Movies.ToListAsync();
        }

        public async Task<List<Movie>> GetAllMovies()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task<Movie?> GetSingleMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
                return null;
            return movie;
        }

        public async Task<List<Movie>?> UpdateMovie(int id, Movie request)
        {
            var movie = await _context.Movies.FindAsync(request.Id);
            if (movie == null)
                return null;

            movie.Title = request.Title;
            movie.Genre = request.Genre;

            await _context.SaveChangesAsync();

            return await _context.Movies.ToListAsync();
        }
    }
}
