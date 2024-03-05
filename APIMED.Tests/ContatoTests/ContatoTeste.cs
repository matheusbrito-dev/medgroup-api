using APIMED.Businnes.Service.Interface;
using APIMED.Businnes.ViewModel;
using APIMED.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace APIMED.Tests.ContatoTests
{
    public class ContatoTeste : TesteBase
    {
        public IContatoService _contatoService;

        public ContatoTeste()
        {
            _contatoService = _serviceProvider.GetRequiredService<IContatoService>();
        }

        #region Testes
        [Test]
        public async Task ObterTodos_Sucesso()
        {
            //arrange
            PopularBancoDeDados(ContatoMock.ObterMoq_Para_ObterTodosContatos());

            //act
            var resultAct = await _contatoService.GetAll();

            //assert	
            Assert.That(resultAct, Is.Not.Null);
            Assert.That(resultAct, Is.TypeOf<List<ContatoViewModel>>());
        }

        [Test]
        public async Task ObterTodos_Resultado_Nulo()
        {
            // arrange
            await _context.Database.EnsureDeletedAsync();
            await _context.Database.EnsureCreatedAsync();

            // act
            var resultAct = await _contatoService.GetAll();

            // assert    
            Assert.That(resultAct, Is.Empty);
        }

        [Test]
        public async Task ObterPorId_Falha()
        {
            //expected 
            var mensagem = "Nenhum contato com esse ID foi encontrado";
            var validacao = false;
            //arrange
            await _context.Database.EnsureDeletedAsync();
            await _context.Database.EnsureCreatedAsync();

            //act
            var resultAct = await _contatoService.RetornaPorId(Guid.NewGuid());

            //assert
            Assert.That(resultAct.Valido == validacao && resultAct.MsgErro == mensagem);
        }

        [Test]
        public async Task ObterPorId_Sucesso()
        {
            //arrange
            PopularBancoDeDados(ContatoMock.ObterMoq_Para_ObterContatoPorId());

            //act
            var constato = await _context.Contatos.FirstOrDefaultAsync();
            var resultAct = await _contatoService.RetornaPorId(constato.Id);

            //assert
            Assert.That(resultAct, Is.Not.Null);
        }

        [Test]
        public async Task Adicionar_Sucesso()
        {
            //arrange 
            var createContato = new ContatoViewModel
            {
                DtNascimento = DateTime.Parse("05-10-1995"),
                NomeContato = "Luiz Filipe",
            };

            //act 
            var resultadoAcao = await _contatoService.Incluir(createContato);
            var contatoExiste = await _context.Contatos.AnyAsync(p => p.NomeContato == createContato.NomeContato);

            //assert
            Assert.That(contatoExiste, Is.True);
        }

        [Test]
        public async Task Adicionar_IdadeInvalida_Falha()
        {

            //arrange
            var createContato = new ContatoViewModel
            {
                DtNascimento = DateTime.Now,
                NomeContato = "Luiz Filipe"
            };

            //act
            var resultadoAcao = await _contatoService.Incluir(createContato);
            var contatoExiste = await _context.Contatos.AnyAsync(p => p.NomeContato == createContato.NomeContato);

            //assert	
            Assert.That(contatoExiste, Is.False);
        }



        [Test]
        public async Task Adicionar_DataInvalida_Falha()
        {

            //arrange
            var createContato = new ContatoViewModel
            {
                DtNascimento = DateTime.Now.AddDays(1),
                NomeContato = "Luiz Filipe"
            };

            //act
            var resultadoAcao = await _contatoService.Incluir(createContato);
            var contatoExiste = await _context.Contatos.AnyAsync(p => p.NomeContato == createContato.NomeContato);

            //assert	
            Assert.That(contatoExiste, Is.False);
        }

        [Test]
        public async Task Atualizar_Sucesso()
        {
            PopularBancoDeDados(ContatoMock.ObterMoq_Para_AtualizarContato());

            // Arrange
            var updatedContato = await _context.Contatos.FirstOrDefaultAsync();
            var contato = _mapper.Map<ContatoViewModel>(updatedContato);
            contato.NomeContato = "Luiz Teste Atualizar";

            // Act
            var result = await _contatoService.Alterar(contato);

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task Atualizar_ContatoNaoExiste_Falha()
        {
            //arrange 
            var updateContato = new ContatoViewModel
            {
                DtNascimento = DateTime.Parse("05-10-1995"),
                NomeContato = "David"
            };

            //act 
            var resultadoAcao = await _contatoService.Alterar(updateContato);
            var contatoExiste = await _context.Contatos.AnyAsync(p => p.NomeContato == updateContato.NomeContato);

            //assert
            Assert.That(contatoExiste, Is.False);
        }
        [Test]
        public async Task AtivarDesativar_Ativar_Sucesso()
        {

            // arrange
            var contatoId = Guid.NewGuid();
            var contato = new Contato { Id = contatoId, Ativo = false };

            PopularBancoDeDados(new List<Contato> { contato });

            // act
            var result = await _contatoService.Ativar(contatoId);

            // assert
            Assert.That(result, Is.Not.Null);
        }


        [Test]
        public async Task AtivarDesativar_Desativar_Sucesso()
        {

            // arrange
            var contatoId = Guid.NewGuid();
            var contato = new Contato { Id = contatoId, Ativo = false };

            PopularBancoDeDados(new List<Contato> { contato });

            // act
            var result = await _contatoService.Ativar(contatoId);

            // assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task Remover_ContatoNaoExiste_Falha()
        {
            // arrange
            var contatoId = Guid.NewGuid();

            // act
            var result = await _contatoService.Excluir(contatoId);

            // assert
            Assert.That(result.Valido == false);
        }

        [Test]
        public async Task Remover_ContatoExiste_Sucesso()
        {
            // arrange
            var contatoId = Guid.NewGuid();
            var contato = new Contato { Id = contatoId };

            PopularBancoDeDados(new List<Contato> { contato });

            // act
            var result = await _contatoService.Excluir(contatoId);

            // assert
            Assert.That(result.Valido == true);
        }
    }
    #endregion

   
}
