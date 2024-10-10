using MercDevs_ej2.Models;
using MercDevs_ej2.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure database connection
builder.Services.AddDbContext<MercyDeveloperContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("connection"),
    Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.25-mariadb")));

// Configure SMTP settings
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("Smtp"));

var smtpSettings = builder.Configuration.GetSection("Smtp").Get<SmtpSettings>();
if (smtpSettings == null)
{
    throw new Exception("Error: Configuración de SMTP no encontrada.");
}

// Register the email service
builder.Services.AddTransient<EmailService>();

// Configure authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Ingresar";
        options.ExpireTimeSpan = TimeSpan.FromDays(1);
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseRotativa();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Ingresar}/{id?}");

app.Run();
