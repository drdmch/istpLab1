using ITschoolMVC.WebMVC.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using ITschoolMVC.Infrastructure;
using ITschoolMVC.Domain.Entities;
using System.Xml.Serialization;

namespace ITschoolMVC.WebMVC.Controllers;

public class CoursesController : Controller
{
    private readonly ITschoolContext _context;
    private readonly IDataPortServiceFactory<Course> _factory;

    public CoursesController(ITschoolContext context, IDataPortServiceFactory<Course> factory)
    {
        _context = context;
        _factory = factory;
    }

    public async Task<IActionResult> Index(string searchString)
    {
        int currentUserId = 1; 

        var enrolledCourseIds = await _context.Enrollments
            .Where(e => e.UserId == currentUserId)
            .Select(e => e.CourseId)
            .ToListAsync();

        ViewBag.EnrolledCourseIds = enrolledCourseIds;

        var coursesQuery = from c in _context.Courses select c;

        if (!string.IsNullOrEmpty(searchString))
        {
            coursesQuery = coursesQuery.Where(s => s.Title.Contains(searchString));
        }

        return View(await coursesQuery.ToListAsync());
    }

    public IActionResult Create()
    {
        ViewBag.LevelId = new SelectList(_context.CourseLevels, "Id", "Name");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Course course)
    {
        if (ModelState.IsValid)
        {
            course.CreatedAt = DateTime.UtcNow; 
            course.UpdatedAt = DateTime.UtcNow;
            _context.Add(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.LevelId = new SelectList(_context.CourseLevels, "Id", "Name", course.LevelId);
        return View(course);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var course = await _context.Courses.FindAsync(id);
        if (course == null) return NotFound();
        return View(course);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Course course)
    {
        if (id != course.Id) return NotFound();

        if (ModelState.IsValid)
        {
            var existingCourse = await _context.Courses.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            if (existingCourse != null)
            {
                course.CreatedAt = DateTime.SpecifyKind(existingCourse.CreatedAt, DateTimeKind.Utc);     
            }
            
            course.UpdatedAt = DateTime.UtcNow; 
            _context.Update(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(course);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var course = await _context.Courses.FirstOrDefaultAsync(m => m.Id == id);
        if (course == null) return NotFound();
        return View(course);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course != null)
        {
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Enroll(int courseId)
    {
        int currentUserId = 1; 
        var isAlreadyEnrolled = await _context.Enrollments
            .AnyAsync(e => e.CourseId == courseId && e.UserId == currentUserId);

        if (isAlreadyEnrolled)
        {
            TempData["ErrorMessage"] = "Ви вже записані на цей курс!";
            return RedirectToAction(nameof(Index));
        }

        var course = await _context.Courses.FindAsync(courseId);
        if (course == null) return NotFound();

        var enrollment = new Enrollment
        {
            CourseId = courseId,
            UserId = currentUserId, 
            EnrolledAt = DateTime.UtcNow,
            Progress = 0
        };

        _context.Enrollments.Add(enrollment);
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = $"Ви успішно записалися на курс '{course.Title}'!";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int id)
    {
        var course = await _context.Courses
            .Include(c => c.Lessons) 
            .Include(c => c.Level)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (course == null) return NotFound();
        return View(course);
    }

    public async Task<IActionResult> MyCourses()
    {
        int currentUserId = 1;
        var myEnrollments = await _context.Enrollments
            .Where(e => e.UserId == currentUserId)
            .Include(e => e.Course)
            .ToListAsync();

        return View(myEnrollments);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Import(IFormFile coursesFile, CancellationToken ct)
    {
        if (coursesFile == null || coursesFile.Length == 0) return RedirectToAction(nameof(Index));

        var importService = _factory.GetImportService(coursesFile.ContentType);
        using (var stream = coursesFile.OpenReadStream())
        {
            await importService.ImportFromStreamAsync(stream, ct);
        }

        TempData["SuccessMessage"] = "Дані успішно оновлено з Excel!";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Export(CancellationToken ct)
    {
        var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        var exportService = _factory.GetExportService(contentType);
        var memoryStream = new MemoryStream();
        await exportService.WriteToAsync(memoryStream, ct);
        memoryStream.Position = 0;
        return new FileStreamResult(memoryStream, contentType)
        {
            FileDownloadName = $"courses_{DateTime.Now:yyyyMMdd}.xlsx"
        };
    }
}

