using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Resort.Application.Common.Interfaces;
using Resort.Domain.Entities;
using Resort.Infrastructure.Data;
using Resort.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();         
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(option =>
{
    option.LoginPath = "/Account/Login";
    option.LogoutPath = "/Account/Logout";
    option.AccessDeniedPath = "/Account/AccessDenied";
});
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 6;
});
var app = builder.Build();
//cchecking pushing
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();   

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
