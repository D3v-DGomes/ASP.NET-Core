using Microsoft.EntityFrameworkCore;
using Pizzaria.Data;
using Pizzaria.Services.Pizza;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// Enviando a ConnectionString para o DbContext:
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));    // Entrando no appsettings.json para pegar o "DefaultConnection"
});

// Registrando o serviço de pizza:
builder.Services.AddScoped<IPizzaInterface, PizzaService>(); // Registrando o serviço de pizza no contêiner de injeção de dependência (DI) do ASP.NET Core.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
