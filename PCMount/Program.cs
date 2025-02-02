using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

using PCMount.Components;
using PCMount.Data;
using PCMount.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
// Add Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>{
    options.LoginPath = "/login";
    options.Cookie.Name = "auth_token";
    options.Cookie.MaxAge = TimeSpan.FromDays(3);
    options.AccessDeniedPath = "/access-denied";
});
builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();
// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);
DbContextConfig.Initialize(builder.Configuration); // Initialize DbContextConfig
// Add Services
builder.Services.AddDBServices();
// Add Fluent UI Config
builder.Services.AddHttpClient();
builder.Services.AddFluentUIComponents(options => {
    options.ValidateClassNames = false;
});

// Build the app
var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts(); // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
}

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();