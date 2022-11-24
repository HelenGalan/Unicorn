using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Unicorn.Data;
using Unicorn.Models.Enums;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddTransient<SeedingService>(); //mudei para AddTransient //nao funcionou tambem

builder.Services.AddDbContext<UnicornContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("UnicornContext") ?? throw new InvalidOperationException("Connection string 'UnicornContext' not found.")));


var app = builder.Build();

/*
Nao funcionou, estou usando outra forma 

//aqui comeca o seeding operation
if (args.Length == 1 && args[0].ToLower() == "SeedingService")
    SeedData(app);

void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetServices<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<SeedingService>;
        service.Seed();
    }
}
//aqui termina
*/




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

    //verificar com alguem onde fica, pois esta no Startup
    //SeedingService.Seed(); //nao vou usar mais
        
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

UnicornDbInitializer.Seed(app);
