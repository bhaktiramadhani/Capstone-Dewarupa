using CapstoneDewarupa.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<CourseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<CourseContext>();
builder.Services.AddRazorPages();

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

// Rute yang menggunakan ID
app.MapControllerRoute(
    name: "idRoute",
    pattern: "{controller=Home}/{action=Index}/{param?}");


app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetService(typeof(UserManager<IdentityUser>)) as UserManager<IdentityUser>;
    var roleManager = scope.ServiceProvider.GetService(typeof(RoleManager<IdentityRole>)) as RoleManager<IdentityRole>;

    await DatabaseInitializer.SeedDataAsync(userManager, roleManager);
}

app.Run();
