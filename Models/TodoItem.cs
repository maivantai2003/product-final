using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace QuanLySanPhamBasic.Models
{
    public class TodoItem
    {
        [Key]
        [Column(TypeName = "int")]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("description")]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        [DisplayName("isDone")]
        public bool IsDone { get; set; }
    }
}
