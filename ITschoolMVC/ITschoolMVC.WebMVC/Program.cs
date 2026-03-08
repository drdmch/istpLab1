using ITschoolMVC.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args); 

builder.Services.AddDbContext<ITschoolContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ITschoolContext"),
    sqlOptions => sqlOptions.MigrationsAssembly("ITschoolMVC.Infrastructure")));

builder.Services.AddControllersWithViews();

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