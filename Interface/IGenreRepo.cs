using QuanLySanPhamBasic.Models;

namespace QuanLySanPhamBasic.Interface
{
    public interface IGenreRepo
    {
        Task<IEnumerable<Genre>> GetAllAsync(string? search);
        Task<Genre> GetByIdAsync(int id);
        Task<bool> CreateAsync(Genre genre);
        Task<bool> UpdateAsync(int id, Genre genre);
        Task<bool> DeleteAsync(int id);
        Task<bool> SaveAsync();
    }
}
