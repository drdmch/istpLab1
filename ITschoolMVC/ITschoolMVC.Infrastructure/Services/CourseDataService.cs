using ClosedXML.Excel;
using ITschoolMVC.Domain.Entities;
using ITschoolMVC.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ITschoolMVC.WebMVC.Infrastructure.Services;

public class CourseImportService : IImportService<Course>
{
    private readonly ITschoolContext _context;
    public CourseImportService(ITschoolContext context) => _context = context;

    public async Task ImportFromStreamAsync(Stream stream, CancellationToken ct)
    {
        using var workbook = new XLWorkbook(stream);
        var worksheet = workbook.Worksheets.FirstOrDefault();
        if (worksheet == null) return;

        foreach (var row in worksheet.RowsUsed().Skip(1))
        {
            var title = row.Cell(1).GetValue<string>();
            if (string.IsNullOrEmpty(title)) continue;

            var exists = await _context.Courses.AnyAsync(c => c.Title == title, ct);
            if (!exists)
            {
                _context.Courses.Add(new Course 
                { 
                    Title = title, 
                    Description = row.Cell(2).GetValue<string>(),
                    Price = row.Cell(3).GetValue<decimal>()
                });
            }
        }
        await _context.SaveChangesAsync(ct);
    }
}

public class CourseExportService : IExportService<Course>
{
    private readonly ITschoolContext _context;
    public CourseExportService(ITschoolContext context) => _context = context;

    public async Task WriteToAsync(Stream stream, CancellationToken ct)
    {
        var courses = await _context.Courses.ToListAsync(ct);
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Courses");

        worksheet.Cell(1, 1).Value = "Назва";
        worksheet.Cell(1, 2).Value = "Опис";
        worksheet.Cell(1, 3).Value = "Ціна";
        worksheet.Row(1).Style.Font.Bold = true;

        for (int i = 0; i < courses.Count; i++)
        {
            worksheet.Cell(i + 2, 1).Value = courses[i].Title;
            worksheet.Cell(i + 2, 2).Value = courses[i].Description;
            worksheet.Cell(i + 2, 3).Value = courses[i].Price;
        }
        workbook.SaveAs(stream);
    }
}