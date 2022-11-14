using Concesionario;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// builder.Services.AddDbContext<ProjectContext>(options => 
//     options.UseInMemoryDatabase("TareasDb")
// );
builder.Services.AddEntityFrameworkMySql()
    .AddDbContext<ProjectContext>(
        (serviceProvider, options) => {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    );

var app = builder.Build();

app.MapGet("/dbConnection", async ([FromServices] ProjectContext context) => {
    context.Database.EnsureCreated();
    return Results.Ok($"Base de datos en memoria: {context.Database.IsInMemory()}");
});

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
