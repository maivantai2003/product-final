using QuanLySanPhamBasic.Models;

namespace QuanLySanPhamBasic.Interface
{
    public interface IBookRepo
    {
        Task<IEnumerable<Book>> GetAllAsync(string? search);
        Task<IEnumerable<Book>> GetAllAsync(string? search, string? author, string? publisher,
        bool? available, int? genreId, DateTime? createdDate, int? priceFrom, int? priceTo, int page = 1, int pageSize = 6);
		Task<Book> GetByIdAsync(int id);
        Task<bool> CreateAsync(Book book);
        Task<bool> UpdateAsync(int id, Book book);
        Task<bool> DeleteAsync(int id);
        Task<bool> SaveAsync();
    }
}
