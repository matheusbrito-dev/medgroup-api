using APIMED.Businnes.Service;
using APIMED.Businnes.Service.Interface;
using APIMED.Data.Data;
using APIMED.Data.Repository;
using APIMED.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APIMED.Configuração
{
    public static class Injecao
    {

        public static IServiceCollection ConfigureDB(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApiMedDbContext>(options =>
                                                options.UseSqlServer(configuration
                                                    .GetConnectionString("DefaultConnection")));

            return services;
        }

        public static IServiceCollection Addrepositories(this IServiceCollection repositories)
        {
            repositories.AddScoped<IContatosRepository, ContatosRepository>();

            return repositories;
        }

        public static IServiceCollection Addservices(this IServiceCollection services)
        {
            services.AddScoped<IContatoService, ContatoService>();

            return services;
        }
    }
}
