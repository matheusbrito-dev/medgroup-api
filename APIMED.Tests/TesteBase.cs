using APIMED.Businnes.Service;
using APIMED.Businnes.Service.Interface;
using APIMED.Data.Data;
using APIMED.Data.Repository;
using APIMED.Data.Repository.Interfaces;
using APIMED.Domain.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace APIMED.Tests
{
    public class TesteBase
    {
        protected ApiMedDbContext _context = default!;
        protected IMapper _mapper = default!;
        protected IServiceProvider _serviceProvider = default!;
        private string DataBaseName = "DataBaseTest" + Guid.NewGuid();


        public TesteBase()
        {
            InicializarContainer();
            InicializarContexto();
        }

        private void InicializarContexto()
        {
            _context = _serviceProvider.GetRequiredService<ApiMedDbContext>();
            _mapper = _serviceProvider.GetRequiredService<IMapper>();
        }

        protected void PopularBancoDeDados<T>(ICollection<T> collection) where T : class
        {
            if (collection != null && collection.Any())
            {
                _context.AddRange(collection);
                _context.SaveChanges();
            }
        }

        private void InicializarContainer()
        {
            var serviceColection = new ServiceCollection();
            serviceColection.AddDbContext<ApiMedDbContext>(options => options.UseInMemoryDatabase(databaseName: DataBaseName));
            serviceColection.AddScoped<IContatosRepository, ContatosRepository>();
            serviceColection.AddScoped<IContatoService, ContatoService>();
            serviceColection.AddAutoMapper(typeof(Configuração.AutoMapperConfig));
            _serviceProvider = serviceColection.BuildServiceProvider();
        }
    }
}
