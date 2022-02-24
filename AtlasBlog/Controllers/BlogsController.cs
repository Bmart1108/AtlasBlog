#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AtlasBlog.Data;
using AtlasBlog.Models;
using Microsoft.AspNetCore.Authorization;
using AtlasBlog.Services;
using AtlasBlog.Services.Interfaces;

namespace AtlasBlog.Controllers

{
    [Authorize(Roles = "Administrator")]
    
    public class BlogsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IImageService _imageService;

        public BlogsController(ApplicationDbContext context, IImageService imageService)
        {
            _context = context;
            _imageService = imageService;

        }

        // GET: Blogs
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Blogs.ToListAsync());
        }

        // GET: Blogs/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }


        [Authorize(Roles = "Administrator")]
     
        // GET: Blogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Blogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
       

        public async Task<IActionResult> Create([Bind("BlogName,Description,ImageFile")] Blog blog, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                if (blog.ImageFile is not null)
                {

                    blog.ImageData = await _imageService.ConvertFileToByteArrayAsync(blog.ImageFile);
                    blog.ImageType = blog.ImageFile.ContentType;
                }

                //Specify the DateTime Kind for the incoming Created Date
                blog.Created = DateTime.UtcNow;

                _context.Add(blog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }

        // GET: Blogs/Edit/5
        [Authorize(Roles = "Administrator")]
       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)



            {

                return NotFound();
            }

            return View(blog);
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
    
        public async Task<IActionResult> Edit(int id, [Bind("Id,BlogName,Description,Updated,Created,ImageFile,ImageData,ImageType")] Blog blog)
        {
            if (id != blog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (blog.ImageFile is not null)
                {

                    blog.ImageData = await _imageService.ConvertFileToByteArrayAsync(blog.ImageFile);
                    blog.ImageType = blog.ImageFile.ContentType;
                }
                try
                {
                    blog.Created = DateTime.SpecifyKind(blog.Created, DateTimeKind.Utc);
                    blog.Updated = DateTime.UtcNow;
                    _context.Update(blog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogExists(blog.Id))
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
            return View(blog);
        }
        
        // GET: Blogs/Delete/5
        [Authorize(Roles = "Administrator")]
       

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
    
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogExists(int id)
        {
            return _context.Blogs.Any(e => e.Id == id);
        }
    }
}