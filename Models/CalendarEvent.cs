using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLySanPhamBasic.Models
{
    public class CalendarEvent
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime EventDate { get; set; }

        public bool IsLunarCalendar { get; set; }

        public string Description { get; set; }

        public bool HasNotification { get; set; }

        public string NotificationType { get; set; }

        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
} 