using APIMED.Businnes.Service.Interface;
using APIMED.Businnes.Validation;
using APIMED.Businnes.ViewModel;
using APIMED.Data.Repository.Interfaces;
using APIMED.Domain.Entities;
using AutoMapper;
using System;

namespace APIMED.Businnes.Service
{
    public class ContatoService : IContatoService
    {
        private readonly IContatosRepository _contatoRepository;
        private readonly IMapper _mapper;
        public ContatoService(IContatosRepository contatoService, IMapper mapper)
        {
            _contatoRepository = contatoService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ContatoViewModel>> GetAll()
        {
            var contatos = await _contatoRepository.ObterTodos();
            var filtroAtivo = contatos.Where(contatos => contatos.Ativo == true).ToList();

            return _mapper.Map<List<ContatoViewModel>>(filtroAtivo);
        }
        public async Task<ContatoViewModel> RetornaPorId(Guid id)
        {
            var contato = await _contatoRepository.ObterPorId(id);
            if(contato ==  null)
            {
                return new ContatoViewModel()
                {
                    Valido = false,
                    MsgErro = "Nenhum contato com esse ID foi encontrado"
                };
            }
            return _mapper.Map<ContatoViewModel>(contato);
        }
        public async Task<ContatoViewModel> Incluir(ContatoViewModel obj)
        {

            var criar = new ContatoViewModel();
            obj.Idade = ValidaContato.CalcularIdade(obj);
            var validarContato = ValidaContato.ValidarDados(obj);

            if (!validarContato.Valido)
                criar.MsgErro = "Erro ao incluir !" + validarContato.Erro;

            else
            {
                try
                {
                    var objMap = _mapper.Map<Contato>(obj);
                    await _contatoRepository.Adicionar(objMap);
                    criar = _mapper.Map<ContatoViewModel>(objMap);
                }
                catch (Exception ex)
                {
                    criar.MsgErro = "Erro ao incluir dados! " + ex.Message;
                }
            }

            return criar;
        }
        public async Task<ContatoViewModel> Excluir(Guid id)
        {
            var retorno = new ContatoViewModel();

            try
            {
                var obj = await _contatoRepository.ObterPorId(id);

                if (obj == null)
                {
                    retorno.Valido = false;
                    retorno.MsgErro = "Contato não encontrado !";
                }

                else
                {
                    await _contatoRepository.Remover(id);
                    retorno.Valido = true;
                }

            }
            catch (Exception ex)
            {
                retorno.Valido = false;
                retorno.MsgErro = "Erro ao excluir Contato! " + ex.Message;
            }

            return retorno;
        }
        public async Task<ContatoViewModel> Alterar(ContatoViewModel obj)
        {
            try
            {
                var contato = await _contatoRepository.ObterPorId(obj.Id);
                obj.Idade = ValidaContato.CalcularIdade(obj);
                var validarContato = ValidaContato.ValidarDados(obj);

                if (!validarContato.Valido)
                    obj.MsgErro = "Erro ao incluir !" + validarContato.Erro;

                else
                {
                    try
                    {
                        _mapper.Map<ContatoViewModel, Contato>(obj, contato);
                        await _contatoRepository.SaveChanges();
                        obj.Valido = true;
                    }
                    catch (Exception ex)
                    {
                        obj.MsgErro = "Erro ao alterar dados! " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                obj.Valido = false;
                obj.MsgErro = "Erro ao incluir dados! " + ex.Message;
            }

            return obj;
        }
        public async Task<ContatoViewModel> Ativar(Guid id)
        {
            var resultado = new ContatoViewModel();
            try
            {

                var contato = await _contatoRepository.ObterPorId(id);

                if (contato == null)
                {
                    resultado.MsgErro = "Contato não encontrado!";
                    resultado.Valido = false;
                    return resultado;
                }

                if (contato.Ativo)
                {
                    contato.Ativo = false;
                }
                else
                {
                    contato.Ativo = true;
                }

                await _contatoRepository.Atualizar(contato);
                resultado.Valido = true;
                resultado.MsgErro = contato.Ativo ? "Contato ativado com sucesso!" : "Contato desativado com sucesso!";
                resultado = _mapper.Map<ContatoViewModel>(contato);
            }
            catch (Exception)
            {

                throw;
            }

            return resultado;
        }
    }
}