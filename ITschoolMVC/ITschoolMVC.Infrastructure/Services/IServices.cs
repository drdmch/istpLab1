using ITschoolMVC.Domain.Entities;
namespace ITschoolMVC.WebMVC.Infrastructure.Services;

public interface IImportService<TEntity> where TEntity : Entity
{
    Task ImportFromStreamAsync(Stream stream, CancellationToken ct);
}

public interface IExportService<TEntity> where TEntity : Entity
{
    Task WriteToAsync(Stream stream, CancellationToken ct);
}

public interface IDataPortServiceFactory<TEntity> where TEntity : Entity
{
    IImportService<TEntity> GetImportService(string contentType);
    IExportService<TEntity> GetExportService(string contentType);
}