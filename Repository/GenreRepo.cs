using Microsoft.EntityFrameworkCore;
using QuanLySanPhamBasic.Interface;
using QuanLySanPhamBasic.Models;

namespace QuanLySanPhamBasic.Repository
{
    public class GenreRepo : IGenreRepo
    {
        private readonly AppDbContext _context;
        private readonly ILogger<GenreRepo> _logger;

        public GenreRepo(AppDbContext context, ILogger<GenreRepo> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> CreateAsync(Genre genre)
        {
            try
            {
                await _context.Genres.AddAsync(genre);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating item.");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre != null)
            {
                _context.Genres.Remove(genre);
                return await SaveAsync();
            }
            else
            {
                _logger.LogWarning("Attempted to delete a non-existing Genre with ID {Id}.", id);
                return false;
            }
        }

        public async Task<IEnumerable<Genre>> GetAllAsync(string? search)
        {
            IQueryable<Genre> query = _context.Genres.AsNoTracking();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(c => c.Name.Contains(search)
                                      || c.Description.Contains(search));
            }

            return await query.ToListAsync();
        }

        public async Task<Genre> GetByIdAsync(int id)
        {
            return await _context.Genres.AsNoTrackingWithIdentityResolution()
                .FirstOrDefaultAsync(e => e.Id == id) ?? new Genre();
        }

        public async Task<bool> UpdateAsync(int id, Genre genre)
        {
            var affectedRows = await _context.Genres
                .Where(e => e.Id == id)
                .ExecuteUpdateAsync(e => e
                    .SetProperty(p => p.Name, genre.Name)
                    .SetProperty(p => p.Description, genre.Description));

            if (affectedRows == 0)
            {
                _logger.LogWarning("Attempted to update a non-existing Genre with ID {Id}.", id);
                return false;
            }

            return affectedRows > 0;
        }

        public async Task<bool> SaveAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
