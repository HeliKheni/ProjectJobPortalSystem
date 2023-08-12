using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectJobPortalSystem.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
// Register the custom 404 route
app.UseStatusCodePagesWithReExecute("/Home/NotFound", "?statusCode={0}");


app.MapControllerRoute(
   name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");




app.MapControllerRoute(
       name: "custom",
       pattern: "Account/Register",
       defaults: new { controller = "Account", action = "Register" });

app.MapRazorPages();

app.Run();
