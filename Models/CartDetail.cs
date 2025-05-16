using System.ComponentModel.DataAnnotations;

namespace QuanLySanPhamBasic.Models
{
    public class CartDetail
    {
        public int CartId { get; set; }
        public int BookId { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public Cart? Cart { get; set; }
        public Book? Book { get; set; }
    }
}
