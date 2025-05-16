using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLySanPhamBasic.Models;
using QuanLySanPhamBasic.ViewModel;

namespace QuanLySanPhamBasic.Controllers
{
    [Authorize]
    public class CalendarController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public CalendarController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int? month, int? year)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            DateTime today = DateTime.Today;
            int currentMonth = month ?? today.Month;
            int currentYear = year ?? today.Year;
            
            var firstDayOfMonth = new DateTime(currentYear, currentMonth, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            var events = await _context.CalendarEvents
                .Where(e => e.UserId == currentUser.Id && 
                           e.EventDate.Month == currentMonth && 
                           e.EventDate.Year == currentYear)
                .OrderBy(e => e.EventDate)
                .Select(e => new CalendarEventViewModel
                {
                    Id = e.Id,
                    Title = e.Title,
                    EventDate = e.EventDate,
                    IsLunarCalendar = e.IsLunarCalendar,
                    Description = e.Description,
                    HasNotification = e.HasNotification,
                    NotificationType = e.NotificationType
                })
                .ToListAsync();

            var viewModel = new CalendarViewModel
            {
                CurrentDate = today,
                CurrentMonth = currentMonth,
                CurrentYear = currentYear,
                Events = events
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create(int day, int month, int year)
        {
            var model = new CalendarEventViewModel
            {
                EventDate = new DateTime(year, month, day)
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CalendarEventViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                {
                    return RedirectToAction("Login", "Auth");
                }

                var calendarEvent = new CalendarEvent
                {
                    Title = model.Title,
                    EventDate = model.EventDate,
                    IsLunarCalendar = model.IsLunarCalendar,
                    Description = model.Description,
                    HasNotification = model.HasNotification,
                    NotificationType = model.NotificationType,
                    UserId = currentUser.Id
                };

                _context.CalendarEvents.Add(calendarEvent);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index), new { month = model.EventDate.Month, year = model.EventDate.Year });
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var calendarEvent = await _context.CalendarEvents.FindAsync(id);
            if (calendarEvent == null)
            {
                return NotFound();
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null || calendarEvent.UserId != currentUser.Id)
            {
                return Forbid();
            }

            var model = new CalendarEventViewModel
            {
                Id = calendarEvent.Id,
                Title = calendarEvent.Title,
                EventDate = calendarEvent.EventDate,
                IsLunarCalendar = calendarEvent.IsLunarCalendar,
                Description = calendarEvent.Description,
                HasNotification = calendarEvent.HasNotification,
                NotificationType = calendarEvent.NotificationType
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CalendarEventViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var calendarEvent = await _context.CalendarEvents.FindAsync(id);
                if (calendarEvent == null)
                {
                    return NotFound();
                }

                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null || calendarEvent.UserId != currentUser.Id)
                {
                    return Forbid();
                }

                calendarEvent.Title = model.Title;
                calendarEvent.EventDate = model.EventDate;
                calendarEvent.IsLunarCalendar = model.IsLunarCalendar;
                calendarEvent.Description = model.Description;
                calendarEvent.HasNotification = model.HasNotification;
                calendarEvent.NotificationType = model.NotificationType;

                _context.Update(calendarEvent);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index), new { month = model.EventDate.Month, year = model.EventDate.Year });
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var calendarEvent = await _context.CalendarEvents.FindAsync(id);
            if (calendarEvent == null)
            {
                return NotFound();
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null || calendarEvent.UserId != currentUser.Id)
            {
                return Forbid();
            }

            _context.CalendarEvents.Remove(calendarEvent);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new { month = calendarEvent.EventDate.Month, year = calendarEvent.EventDate.Year });
        }
    }
} 