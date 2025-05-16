using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLySanPhamBasic.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, ErrorMessage = "Title cannot be longer than 100 characters")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Author is required")]
        public string? Author { get; set; }
        public int Price { get; set; }
        public bool Available { get; set; }
        public string Publisher { get; set; }
        public DateTime CreatedDate { get; set; }
        public int GenreId { get; set; }

        [ForeignKey("GenreId")]
        public Genre? Genre { get; set; }
        public ICollection<BookCatalog>? BookCatalogs { get; set; }
        public ICollection<CartDetail>? CartDetails { get; set; }
        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
