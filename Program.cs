using GASSBOOKING_WEBSITE.Interface;
using GASSBOOKING_WEBSITE.Repositories;
using GASSBOOKING_WEBSITE.Repository;
using GASSBOOKING_WEBSITE.Service;
using GASSBOOKING_WEBSITE.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register your services
builder.Services.AddScoped<IRegistrationService, RegistrationService>();
builder.Services.AddScoped<IAuthorizationService, AuthorizationService>();
builder.Services.AddScoped<CylinderRepository>();
builder.Services.AddScoped<ICylinderService, CylinderService>();
builder.Services.AddScoped<BookingRepository>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<BookingRepository>();
builder.Services.AddScoped<AuthorizationRepository>();

// Configure authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/AuthLogin";
        options.AccessDeniedPath = "/Home/AccessDenied";
        options.LogoutPath = "/Login/Logout";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Adds HTTP Strict Transport Security
}

app.UseHttpsRedirection(); // Redirects HTTP requests to HTTPS
app.UseStaticFiles();

app.UseRouting();

// Add authentication and authorization
app.UseAuthentication(); // Make sure this comes before UseAuthorization
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
