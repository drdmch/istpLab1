using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ITschoolMVC.WebMVC.Models;
using ITschoolMVC.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ITschoolMVC.WebMVC.Controllers;
using ITschoolMVC.Domain.Entities;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ITschoolContext _context; 

    public HomeController(ILogger<HomeController> logger, ITschoolContext context)
    {
        _logger = logger;
        _context = context;
    }


public async Task<IActionResult> Index()
    {
        var latestCourses = await _context.Courses
            .Include(c => c.Level)
            .OrderByDescending(c => c.CreatedAt)
            .Take(3)
            .ToListAsync();

        return View(latestCourses);
    }

public IActionResult About()
    {
        return View();
    }

public async Task<IActionResult> Profile()
    {
        var user = await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync();

        if (user == null)
        {
            return Content("База порожня.");
        }

        return View(user);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

   
public async Task<IActionResult> EditProfile()
{
    var user = await _context.Users.FirstOrDefaultAsync();
    if (user == null) return NotFound();
    return View(user);
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> EditProfile(User user)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync();

        if (existingUser != null)
        {
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.About = user.About;

            _context.Update(existingUser);
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Profile));
        }
        return View(user);
    }
}