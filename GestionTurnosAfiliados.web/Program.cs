using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using GestionTurnosAfiliados.Web.Data;

var builder = WebApplication.CreateBuilder(args);

// VARIABLES DE CONEXIÓN DESDE ENTORNO
var host = Environment.GetEnvironmentVariable("DB_HOST");
var port = Environment.GetEnvironmentVariable("DB_PORT");
var user = Environment.GetEnvironmentVariable("DB_USER");
var password = Environment.GetEnvironmentVariable("DB_PASS");
var database = Environment.GetEnvironmentVariable("DB_NAME");

// CADENA DE CONEXIÓN
var connectionString = $"Server={host};Port={port};Database={database};User={user};Password={password};";

// CONFIGURAR CONTEXTO DE BASE DE DATOS
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

// CONFIGURAR MVC
builder.Services.AddControllersWithViews();

// ================================
// 🔐 CONFIGURAR AUTENTICACIÓN GOOGLE
// ================================
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle(options =>
{
    // LEER CLIENT ID Y SECRET DESDE appsettings.json
    options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];

    // ⚠️ IMPORTANTE: Debe coincidir con la URL configurada en Google Cloud
    options.CallbackPath = "/signin-google";
});

var app = builder.Build();

// ================================
// PIPELINE
// ================================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 👇 Debe ir antes de UseAuthorization()
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
