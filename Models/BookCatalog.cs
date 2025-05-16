using System.ComponentModel.DataAnnotations;

namespace QuanLySanPhamBasic.Models
{
    public class BookCatalog
    {
        [Key]
        public int Id { get; set; }
        public int BookId { get; set; }
        public int CatalogId { get; set; }
        public Book? Book { get; set; }
        public Catalog? Catalog { get; set; }
    }
}
