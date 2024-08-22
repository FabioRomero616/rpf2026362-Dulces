using Microsoft.EntityFrameworkCore;
using Chocoworld.Data;
using Microsoft.AspNetCore.Authentication.Cookies;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
{
    option.LoginPath = "/Acceso/Login";
    option.ExpireTimeSpan = TimeSpan.FromMinutes(15);
    option.AccessDeniedPath = "/Dashboard/Index";
});

builder.Services.AddDbContext<ChocomundoContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("ChocomundoContext"), new MySqlServerVersion(new Version(6, 0, 2) ?? throw new InvalidOperationException("Connection string 'WebApplication7Context' not found."))));

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
