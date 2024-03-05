using APIMED.Data.Data;
using APIMED.Data.Repository.Interfaces;
using APIMED.Domain.Entities;

namespace APIMED.Data.Repository
{
    public class ContatosRepository : Repository<Contato>, IContatosRepository
    {
        public ContatosRepository(ApiMedDbContext context) : base(context) { }
    }
}