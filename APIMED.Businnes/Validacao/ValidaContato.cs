using APIMED.Businnes.ViewModel;
using APIMED.Domain.Entities;

namespace APIMED.Businnes.Validation
{
    public class ValidaContato(Contato obj)
    {
        public static ErroViewModel ValidarDados(ContatoViewModel obj)
        {
            var validacao = new ErroViewModel() { Valido = true };

            if (obj.DtNascimento > DateTime.Now)
                validacao = new ErroViewModel() { Valido = false, Erro = "Data de nascimento é maior que a data atual" };
            if (obj.Idade < 18)
                validacao = new ErroViewModel() { Valido = false, Erro = "O contato deve ser maior de idade" };

            return validacao;
        }

        public static int CalcularIdade(ContatoViewModel obj)
        {
            var dtNascimento = obj.DtNascimento;
            int idade = DateTime.Now.Year - dtNascimento.Year;
            if (DateTime.Now.DayOfYear < dtNascimento.DayOfYear)
            {
                idade = idade - 1;
            }
            return idade;
        }
    }
}
