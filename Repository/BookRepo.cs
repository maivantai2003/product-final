using Microsoft.EntityFrameworkCore;
using QuanLySanPhamBasic.Interface;
using QuanLySanPhamBasic.Models;

namespace QuanLySanPhamBasic.Repository
{
    public class BookRepo : IBookRepo
    {
        private readonly AppDbContext _context;
        private readonly ILogger<BookRepo> _logger;

        public BookRepo(AppDbContext context, ILogger<BookRepo> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> CreateAsync(Book book)
        {
            try
            {
                await _context.Books.AddAsync(book);
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
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                return await SaveAsync();
            }
            else
            {
                _logger.LogWarning("Attempted to delete a non-existing Book with ID {Id}.", id);
                return false;
            }
        }

        public async Task<IEnumerable<Book>> GetAllAsync(string? search)
        {
            IQueryable<Book> query = _context.Books
          .Include(b => b.Genre)
          .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();

                query = query.Where(b =>
                    b.Title!.ToLower().Contains(search) ||
                    b.Author!.ToLower().Contains(search) ||
                    b.Publisher.ToLower().Contains(search) ||
                    b.Genre != null && b.Genre.Name.ToLower().Contains(search)
                );
            }

            return await query.ToListAsync();
        }
        public async Task<IEnumerable<Book>> GetAllAsync(string? search, string? author, string? publisher,
	    bool? available, int? genreId, DateTime? createdDate, int? priceFrom, int? priceTo, int page = 1, int pageSize = 6)
		{
			IQueryable<Book> query = _context.Books
		.Include(b => b.Genre)
		.AsNoTracking();

			if (!string.IsNullOrWhiteSpace(search))
			{
				search = search.ToLower();
				query = query.Where(b =>
					b.Title!.ToLower().Contains(search) ||
					b.Author!.ToLower().Contains(search) ||
					b.Publisher.ToLower().Contains(search) ||
					b.Genre != null && b.Genre.Name.ToLower().Contains(search)
				);
			}

			if (!string.IsNullOrWhiteSpace(author))
				query = query.Where(b => b.Author!.Contains(author));

			if (!string.IsNullOrWhiteSpace(publisher))
				query = query.Where(b => b.Publisher.Contains(publisher));

			if (available.HasValue)
				query = query.Where(b => b.Available == available);

			if (genreId.HasValue)
				query = query.Where(b => b.GenreId == genreId);

			if (createdDate.HasValue)
				query = query.Where(b => b.CreatedDate.Date == createdDate.Value.Date);

			if (priceFrom.HasValue)
				query = query.Where(b => b.Price >= priceFrom);

			if (priceTo.HasValue)
				query = query.Where(b => b.Price <= priceTo);

			return await query
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();
		}

		public async Task<Book> GetByIdAsync(int id)
        {
            return await _context.Books.AsNoTrackingWithIdentityResolution()
                .FirstOrDefaultAsync(e => e.Id == id) ?? new Book();
        }

        public async Task<bool> UpdateAsync(int id, Book book)
        {
            var affectedRows = await _context.Books
                .Where(e => e.Id == id)
                .ExecuteUpdateAsync(e => e
                    .SetProperty(p => p.Title, book.Title));

            if (affectedRows == 0)
            {
                _logger.LogWarning("Attempted to update a non-existing Book with ID {Id}.", id);
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
