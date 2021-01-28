using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookmarksWebApp.Data;
using BookmarksWebApp.Models;

namespace BookmarksWebApp
{
    public class BookmarksController : Controller
    {
        private readonly BookmarksWebAppContext _context;

        public BookmarksController(BookmarksWebAppContext context)
        {
            _context = context;
        }

        // GET: Bookmarks
        public async Task<IActionResult> Index()
        {
            return View(await _context.Bookmark.ToListAsync());
        }

        // GET: Bookmarks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookmark = await _context.Bookmark
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookmark == null)
            {
                return NotFound();
            }

            return View(bookmark);
        }

        // GET: Bookmarks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bookmarks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Url")] Bookmark bookmark)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookmark);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bookmark);
        }

        // GET: Bookmarks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookmark = await _context.Bookmark.FindAsync(id);
            if (bookmark == null)
            {
                return NotFound();
            }
            return View(bookmark);
        }

        // POST: Bookmarks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Url")] Bookmark bookmark)
        {
            if (id != bookmark.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookmark);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookmarkExists(bookmark.Id))
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
            return View(bookmark);
        }

        // GET: Bookmarks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookmark = await _context.Bookmark
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookmark == null)
            {
                return NotFound();
            }

            return View(bookmark);
        }

        // POST: Bookmarks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookmark = await _context.Bookmark.FindAsync(id);
            _context.Bookmark.Remove(bookmark);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookmarkExists(int id)
        {
            return _context.Bookmark.Any(e => e.Id == id);
        }
    }
}
