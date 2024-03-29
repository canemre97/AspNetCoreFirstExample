using System.Reflection;
using AspNetCoreFirstExample.Web.Filters;
using AspNetCoreFirstExample.Web.Helpers;
using AspNetCoreFirstExample.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"));
});


//builder.Services.AddSingleton<IHelper, Helper>();


//builder.Services.AddScoped<IHelper, Helper>();
//builder.Services.AddScoped<Helper>();
//builder.Services.AddScoped<Helper>(sp=>new Helper());
//builder.Services.AddScoped(new Helper());

builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Directory.GetCurrentDirectory()));

builder.Services.AddTransient<IHelper, Helper>(//sp =>
//{
//    return new Helper(true);
//}
);

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddScoped<NotFoundFilter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();



//app.MapControllerRoute(
//    name: "pages",
//    pattern: "blog/{*article}",
//    defaults: new { controller = "Blog", action = "Article" });

//app.MapControllerRoute(
//    name: "pages",
//    pattern: "{controller=Blog}/{action=Artigle}/{name}/{id}");

//app.MapControllerRoute(
//    name: "pages",
//    pattern: "{controller}/{action}/{page}/{pagesize}");
////pattern: "{controller=product}/{action=pages}/{page}/{pagesize}");

//app.MapControllerRoute(
//    name: "productgetbyid",
//    pattern: "{controller}/{action}/{productid}");
////pattern: "{controller=product}/{action=getbyid}/{productid}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.MapControllers();

app.Run();
