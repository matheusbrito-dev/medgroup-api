using APIMED.Businnes.Service.Interface;
using APIMED.Businnes.Validation;
using APIMED.Businnes.ViewModel;
using APIMED.Data.Repository.Interfaces;
using APIMED.Domain.Entities;
using AutoMapper;

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
            var funcionarios = await _contatoRepository.ObterTodos();
            var filtroAtivo = funcionarios.Where(funcionarios => funcionarios.Ativo == true).ToList();

            return _mapper.Map<List<ContatoViewModel>>(filtroAtivo);
        }
        public async Task<ContatoViewModel> RetornaPorId(Guid id)
        {
            var funcionario = await _contatoRepository.ObterPorId(id);
            return _mapper.Map<ContatoViewModel>(funcionario);
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
                var obj = _contatoRepository.ObterPorId(id);

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
                var objMap = _mapper.Map<Contato>(obj);
                await _contatoRepository.Atualizar(objMap);
                obj.Valido = true;
            }
            catch (Exception ex)
            {
                obj.Valido = false;
                obj.MsgErro = "Erro ao incluir dados! " + ex.Message;
            }

            return obj;
        }
    }
}