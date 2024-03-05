using APIMED.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace APIMED.Data.Data
{
    public class ApiMedDbContext : IdentityDbContext
    {
        public ApiMedDbContext(DbContextOptions option) : base(option) 
        {

        }
        public DbSet<Contato> Contatos { get; set; }
    }
}