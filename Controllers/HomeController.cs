using BlogMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BlogMVC.Controllers
{
    public class HomeController : Controller
    {
        ApplicationContext db;
        public HomeController(ApplicationContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.Posts.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(PostViewModel postVM)
        {
            Console.WriteLine(postVM.Title);
            Console.WriteLine("1");
            Console.WriteLine(postVM.Photo);
            Post post = new Post { Title = postVM.Title, CreatedDate = postVM.CreatedDate, Text = postVM.Text };
            if (postVM.Photo != null)
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(postVM.Photo.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)postVM.Photo.Length);
                }
                post.Photo = imageData;
            }
            db.Posts.Add(post);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Post post = new Post { Id = id.Value };
                db.Entry(post).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Post? post = await db.Posts.FirstOrDefaultAsync(p => p.Id == id);
                if (post != null) return View(post);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Post post)
        {
            db.Posts.Update(post);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Post(int? id)
        {

            if (id != null)
            {
                Post? post = await db.Posts.FirstOrDefaultAsync(p => p.Id == id);
                if (post != null) return View(post);
            }
            return NotFound();
        }
        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}