using BlazingPizza.Server.Models;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<PizzaStoreContext>(options=> options.UseSqlServer(builder.Configuration.GetConnectionString("PizzaStore")));


var app = builder.Build();


//var connectionString = builder.Configuration.GetConnectionString("PizzaStore");
//builder.Services.AddDbContext<PizzaStoreContext>(
//    options => options.UseSqlServer(connectionString));





// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
   app.UseExceptionHandler("/Error");

// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

var ScopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var Scope = ScopeFactory.CreateScope())
{
    var Context = Scope.ServiceProvider.GetRequiredService<PizzaStoreContext>();
    if (!Context.Specials.Any())
    {
        SeedData.Initialize(Context);
    }
}


app.Run();
