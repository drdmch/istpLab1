using ITschoolMVC.Domain.Entities;
using ITschoolMVC.Infrastructure;
using ITschoolMVC.WebMVC.Infrastructure.Services; 
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args); 

builder.Services.AddDbContext<ITschoolContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ITschoolContext"),
    sqlOptions => sqlOptions.MigrationsAssembly("ITschoolMVC.Infrastructure")));

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IDataPortServiceFactory<Course>, CourseDataPortServiceFactory>();
var app = builder.Build(); 

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); 
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();