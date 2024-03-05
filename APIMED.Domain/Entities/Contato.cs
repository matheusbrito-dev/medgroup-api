namespace APIMED.Domain.Entities
{
    public class Contato : BaseEntity
    {
        public string? NomeContato { get; set; }
        public DateTime DtNascimento { get; set; }
        public string? Sexo { get; set; }
        public bool Ativo { get; set; }
        public int Idade { get; set; }
    }
}