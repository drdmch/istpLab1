using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ITschoolMVC.Infrastructure;
using ITschoolMVC.Domain.Entities;

namespace ITschoolMVC.WebMVC.Controllers;

public class LessonsController : Controller
{
    private readonly ITschoolContext _context;

    public LessonsController(ITschoolContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index(int courseId)
    {
        int currentUserId = 1;

        var isEnrolled = await _context.Enrollments
            .AnyAsync(e => e.CourseId == courseId && e.UserId == currentUserId);

        if (!isEnrolled)
        {
            TempData["ErrorMessage"] = "Спочатку потрібно записатися на курс, щоб переглянути уроки!";
            return RedirectToAction("Index", "Courses");
        }

        var lessons = await _context.Lessons
            .Where(l => l.CourseId == courseId)
            .OrderBy(l => l.OrderNumber)
            .ToListAsync();

        ViewBag.CourseId = courseId;
        var course = await _context.Courses.FindAsync(courseId);
        ViewBag.CourseTitle = course?.Title;

        return View(lessons);
    }

    public IActionResult Create(int courseId)
    {
        var lesson = new Lesson { CourseId = courseId };
        
        var course = _context.Courses.Find(courseId);
        ViewBag.CourseTitle = course?.Title;
        
        return View(lesson);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Title,VideoUrl,OrderNumber,CourseId")] Lesson lesson)
    {
        if (ModelState.IsValid)
        {
            _context.Add(lesson);
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index), new { courseId = lesson.CourseId });
        }
        return View(lesson);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var lesson = await _context.Lessons.Include(l => l.Course).FirstOrDefaultAsync(m => m.Id == id);
        if (lesson == null) return NotFound();

        ViewBag.CourseTitle = lesson.Course?.Title;
        return View(lesson);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Title,VideoUrl,OrderNumber,CourseId")] Lesson lesson)
    {
        if (id != lesson.Id) return NotFound();

        if (ModelState.IsValid)
        {
            _context.Update(lesson);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { courseId = lesson.CourseId });
        }
        return View(lesson);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var lesson = await _context.Lessons.Include(l => l.Course).FirstOrDefaultAsync(m => m.Id == id);
        if (lesson == null) return NotFound();

        return View(lesson);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var lesson = await _context.Lessons.FindAsync(id);
        int? courseId = lesson?.CourseId;

        if (lesson != null) _context.Lessons.Remove(lesson);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index), new { courseId = courseId });
    }
}