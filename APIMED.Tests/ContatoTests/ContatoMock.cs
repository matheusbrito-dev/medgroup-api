using APIMED.Domain.Entities;

namespace APIMED.Tests.ContatoTests
{
    public class ContatoMock
    {
        private static Contato ObterContatoParaOMoq(string nome)
        {
            return new Contato
            {
                DtNascimento = DateTime.Parse("05-10-1995"),
                Sexo = "Masculino",
                NomeContato = nome,
                Ativo = true,
            };
        }

        private static List<Contato> ObterListaContatos(int quantidade)
        {
            var lista = new List<Contato>();

            for (int i = 0; i < quantidade; i++)
                lista.Add(ObterContatoParaOMoq($"{nameof(ObterMoq_Para_ObterTodosContatos)}{i}"));

            return lista;
        }

        public static List<Contato> ObterMoq_Para_ObterTodosContatos()
        {
            return ObterListaContatos(10);
        }

        public static List<Contato> ObterMoq_Para_AtualizarContato()
        {
            return ObterListaContatos(10);
        }

        public static List<Contato> ObterMoq_Para_ObterContatoPorId()
        {
            var lista = new List<Contato>();
            lista.Add(ObterContatoParaOMoq(nameof(ObterMoq_Para_ObterContatoPorId)));
            return lista;
        }
    }
}
