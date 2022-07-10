using LanchesMac.Models;
using LanchesMac.Repositories;
using LanchesMac.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LanchesMac.Context;

public static class Infrastructure
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        //services.AddDbContext<AppDbContext>(options => 
        //    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
        //    b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

        services.AddTransient<ICategoriaRepository, CategoriaRepository>();
        services.AddTransient<ILancheRepository, LancheRepository>();
        services.AddScoped(sp => CarrinhoCompra.GetCarrinho(sp));
        services.AddTransient<IPedidoRepository, PedidoRepository>();

        //HttpContext
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        //Configuração dos cookies da aplicação 
        services.ConfigureApplicationCookie(options =>
        options.AccessDeniedPath = "/Account/Login"); //Controller Account, Action Login

        return services;
    }
}
