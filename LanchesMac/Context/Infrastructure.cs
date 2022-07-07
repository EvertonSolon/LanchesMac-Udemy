using Microsoft.EntityFrameworkCore;

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

        //Configuração dos cookies da aplicação 
        services.ConfigureApplicationCookie(options =>
        options.AccessDeniedPath = "/Account/Login"); //Controller Account, Action Login

        return services;
    }
}
