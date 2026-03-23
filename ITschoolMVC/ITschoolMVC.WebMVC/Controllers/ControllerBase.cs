using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ITschoolMVC.Infrastructure;
using ITschoolMVC.Domain.Entities;

namespace ITschoolMVC.WebMVC.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChartsController : ControllerBase
{
    private readonly ITschoolContext _context;

    public ChartsController(ITschoolContext context)
    {
        _context = context;
    }

    [HttpGet("topCourses")]
    public async Task<JsonResult> GetTopCourses(CancellationToken ct)
    {
        var data = await _context.Courses
            .Select(c => new { 
                Title = c.Title, 
                Count = c.Enrollments.Count() 
            })
            .OrderByDescending(c => c.Count)
            .Take(3)
            .ToListAsync(ct);

        return new JsonResult(data);
    }

    [HttpGet("userProgress/{courseId}")]
    public async Task<JsonResult> GetUserProgress(int courseId)
    {
        var enrollment = await _context.Enrollments
            .FirstOrDefaultAsync(e => e.CourseId == courseId);

        int progressValue = enrollment?.Progress ?? 0;
        int remainingValue = 100 - progressValue;

        var data = new[]
        {
            new { Value = progressValue },
            new { Value = remainingValue }
        };

        return new JsonResult(data);
    }

    [HttpPost("incrementProgress/{courseId}")]
    public async Task<IActionResult> IncrementProgress(int courseId)
    {
        var enrollment = await _context.Enrollments
            .Include(e => e.Course)
            .ThenInclude(c => c!.Lessons)
            .FirstOrDefaultAsync(e => e.CourseId == courseId);

        if (enrollment?.Course?.Lessons != null)
        {
            int totalLessons = enrollment.Course.Lessons.Count;
            int step = totalLessons > 0 ? 100 / totalLessons : 10;

            enrollment.Progress += step;
            if (enrollment.Progress > 100) enrollment.Progress = 100;

            await _context.SaveChangesAsync();
            return Ok();
        }
        
        return NotFound();
    }
}