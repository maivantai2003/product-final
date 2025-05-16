using System.ComponentModel.DataAnnotations;

namespace QuanLySanPhamBasic.Models
{
    public class OrderDetail
    {
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        
        public Order? Order { get; set; }
        public Book? Book { get; set; }
    }
} 