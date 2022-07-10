using LanchesMac.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllersWithViews();
builder.Services.AddMemoryCache();
builder.Services.AddSession();

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
app.UseSession();

app.UseAuthorization();

//app.MapControllerRoute(
//    name: "teste",
//    pattern: "testeme",
//    defaults: new { controller = "teste", Action = "Index"});

//app.MapControllerRoute(
//    name: "admin",
//    pattern: "admin/{action=Index}/{id?}",
//    defaults: new { controller = "Admin" });

//A ordenação das rotas é muito importante!
//Nos exemplos abaixo, se digitar admin na url, a primeira condição irá atender e não irá para admin.
//Já se inverter a ordem das rotas nesta configuração e colocar a admin antes da home, ao digitar admin
//irá para o controller Admin.
app.MapControllerRoute(
    name: "home",
    pattern: "{home}",
    defaults: new { controller = "Home", Action = "Index" });

app.MapControllerRoute(
    name: "admin",
    pattern: "admin",
    defaults: new { controller = "Admin", Action = "Index" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
