using APIMED.Businnes.ViewModel;
using APIMED.Domain.Entities;
using AutoMapper;

namespace APIMED.Configuração
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Contato, ContatoViewModel>().ReverseMap();
        }
    }
}