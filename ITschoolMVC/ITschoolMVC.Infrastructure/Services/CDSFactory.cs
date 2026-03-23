using ITschoolMVC.Domain.Entities;
using ITschoolMVC.Infrastructure;

namespace ITschoolMVC.WebMVC.Infrastructure.Services;

public class CourseDataPortServiceFactory : IDataPortServiceFactory<Course>
{
    private readonly ITschoolContext _context;
    private const string ExcelType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

    public CourseDataPortServiceFactory(ITschoolContext context) => _context = context;

    public IImportService<Course> GetImportService(string contentType)
    {
        if (contentType == ExcelType) return new CourseImportService(_context);
        throw new NotImplementedException("Тип файлу не підтримується для імпорту");
    }

    public IExportService<Course> GetExportService(string contentType)
    {
        if (contentType == ExcelType) return new CourseExportService(_context);
        throw new NotImplementedException("Тип файлу не підтримується для експорту");
    }
}