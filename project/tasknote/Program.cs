using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();  // Allow all origins, methods, and headers
    });
});



builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 21))));

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseCors("AllowAllOrigins");
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// app.UseWhen(context => context.Request.Path.StartsWithSegments("/api/Users"), builder =>
// {
//     builder.UseMiddleware<CustomApiAuthenticationMiddleware>();
// });

// app.UseMiddleware<CustomApiAuthenticationMiddleware> ();


app.MapControllerRoute(
    name: "user",
    pattern: "{controller=User}/{action=Index}/{id?}"
);
app.MapControllerRoute(
    name: "note",
    pattern: "{controller=Note}/{action=Index}/{id?}"
);
app.MapControllerRoute(
    name: "auth",
    pattern: "{controller=Auth}/{action=Index}/{id?}"
);
// app.MapRazorPages();

app.Run();
