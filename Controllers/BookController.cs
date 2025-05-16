using QuanLySanPhamBasic.Models;
using QuanLySanPhamBasic.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace QuanLySanPhamBasic.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookRepo _bookRepo;
        private readonly IGenreRepo _genreRepo;

        public BooksController(IBookRepo bookRepo, IGenreRepo genreRepo)
        {
            _bookRepo = bookRepo;
            _genreRepo = genreRepo;
        }

		//public async Task<IActionResult> Index(string ?search)
		//{
		//    var books = await _bookRepo.GetAllAsync(search);

		//    if (!ModelState.IsValid)
		//    {
		//        return NotFound(books);
		//    }

		//    return View(books);
		//}
		public async Task<IActionResult> Index(string? search, string? author, string? publisher,
	    bool? available, int? genreId, DateTime? createdDate, int? priceFrom, int? priceTo,int page = 1)
		{
			ViewBag.Genres = new SelectList(await _genreRepo.GetAllAsync(""), "Id", "Description");
			var allBooks = await _bookRepo.GetAllAsync(search, author, publisher, available, genreId, createdDate, priceFrom, priceTo, 1, int.MaxValue);
			ViewBag.TotalPages = (int)Math.Ceiling(allBooks.Count() / 6.0);
			ViewBag.CurrentPage = page;

			var books = await _bookRepo.GetAllAsync(search, author, publisher, available, genreId, createdDate, priceFrom, priceTo, page, 6);
			return View(books);
		}

		public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _bookRepo.GetByIdAsync(id.Value);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["GenreId"] = new SelectList(await _genreRepo.GetAllAsync(""), "Id", "Description");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Title,Author,Price,Available,Publisher,CreatedDate,GenreId")] Book book)
        {
            if (ModelState.IsValid)
            {
                await _bookRepo.CreateAsync(book);
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new SelectList(await _genreRepo.GetAllAsync(""), "Id", "Description", book.GenreId);
            return View(book);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _bookRepo.GetByIdAsync(id.Value);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["GenreId"] = new SelectList(await _genreRepo.GetAllAsync(""), "Id", "Description", book.GenreId);
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Author,Price,Available,Publisher,CreatedDate,GenreId")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _bookRepo.UpdateAsync(id, book);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _bookRepo.GetByIdAsync(id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new SelectList(await _genreRepo.GetAllAsync(""), "Id", "Description", book.GenreId);
            return View(book);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _bookRepo.GetByIdAsync(id.Value);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bookRepo.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
