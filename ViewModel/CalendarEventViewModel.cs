using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using QuanLySanPhamBasic.Models;

namespace QuanLySanPhamBasic.ViewModel
{
    public class CalendarEventViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên sự kiện là bắt buộc")]
        [Display(Name = "Tên sự kiện")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Ngày sự kiện là bắt buộc")]
        [Display(Name = "Ngày sự kiện")]
        [DataType(DataType.Date)]
        public DateTime EventDate { get; set; }

        [Display(Name = "Âm lịch")]
        public bool IsLunarCalendar { get; set; }

        [Display(Name = "Dương lịch")]
        public bool IsSolarCalendar => !IsLunarCalendar;

        [Display(Name = "Ghi chú")]
        public string Description { get; set; }

        [Display(Name = "Thông báo")]
        public bool HasNotification { get; set; }

        [Display(Name = "Loại thông báo")]
        public string NotificationType { get; set; }
    }

    public class CalendarViewModel
    {
        public DateTime CurrentDate { get; set; }
        public int CurrentMonth { get; set; }
        public int CurrentYear { get; set; }
        public List<CalendarEventViewModel> Events { get; set; }
    }
} 