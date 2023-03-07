using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CE160059_SignalRLab.Models;
using Microsoft.AspNetCore.SignalR;

namespace CE160059_SignalRLab.Controllers
{
    public class PostsController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IHubContext<SignalrServer> _signalrHub;

        public PostsController(ApplicationDBContext context, IHubContext<SignalrServer> signalrHub)
        {
            _context = context;
            _signalrHub = signalrHub;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            var applicationDBContext = _context.Posts.Include(p => p.cat).Include(p => p.user);
            return View(await applicationDBContext.ToListAsync());
        }

        [HttpGet]
        public IActionResult GetPosts()
        {
            var res = _context.Posts.Include(p => p.cat).Include(p => p.user).ToList();
            return Ok(res);
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.cat)
                .Include(p => p.user)
                .FirstOrDefaultAsync(m => m.postID == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            ViewData["categoryID"] = new SelectList(_context.Categories, "categoryID", "categoryID");
            ViewData["authorID"] = new SelectList(_context.Users, "userID", "email");
            return View();
        }


        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("postID,authorID,createdDate,updatedDate,title,content,publicStatus,categoryID")] Post post)
        {
            if (ModelState.IsValid)
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                await _signalrHub.Clients.All.SendAsync("LoadPosts");
                return RedirectToAction(nameof(Index));
            }
            ViewData["categoryID"] = new SelectList(_context.Categories, "categoryID", "categoryID", post.categoryID);
            ViewData["authorID"] = new SelectList(_context.Users, "userID", "email", post.authorID);
            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["categoryID"] = new SelectList(_context.Categories, "categoryID", "categoryID", post.categoryID);
            ViewData["authorID"] = new SelectList(_context.Users, "userID", "email", post.authorID);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("postID,authorID,createdDate,updatedDate,title,content,publicStatus,categoryID")] Post post)
        {
            if (id != post.postID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                    await _signalrHub.Clients.All.SendAsync("LoadPosts");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.postID))
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
            ViewData["categoryID"] = new SelectList(_context.Categories, "categoryID", "categoryID", post.categoryID);
            ViewData["authorID"] = new SelectList(_context.Users, "userID", "email", post.authorID);
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.cat)
                .Include(p => p.user)
                .FirstOrDefaultAsync(m => m.postID == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            await _signalrHub.Clients.All.SendAsync("LoadPosts");
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.postID == id);
        }
    }
}
