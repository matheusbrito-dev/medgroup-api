namespace APIMED.Businnes.ViewModel
{
    public class ContatoViewModel
    {
        public string? NomeContato { get; set; }
        public DateTime DtNascimento { get; set; }
        public string? Sexo { get; set; }
        public bool Ativo { get; set; }
        public int Idade { get; set; }
        public string MsgErro { get; set; }
        public bool Valido { get; set; } = true;
    }
}
