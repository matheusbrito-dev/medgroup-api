using APIMED.Businnes.Service.Interface;
using APIMED.Businnes.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace APIMED.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatoController : Controller
    {
        private readonly IContatoService _contatoService;

        public ContatoController(IContatoService funcionarioService)
        {
            _contatoService = funcionarioService;
        }
        [HttpPost("Incluir")]
        public async Task<IActionResult> Incluir([FromBody] ContatoViewModel contato)
        {
            return Ok(await _contatoService.Incluir(contato));
        }
        [HttpPut("Alterar")]
        public async Task<IActionResult> Alterar([FromBody] ContatoViewModel obj)
        {
            return Ok(await _contatoService.Alterar(obj));
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _contatoService.GetAll());
        }
        [HttpGet("RetornarPorId/{id:guid}")]
        public async Task<IActionResult> RetornaPorId([FromRoute] Guid id)
        {
            return Ok(await _contatoService.RetornaPorId(id));
        }
        [HttpDelete("Excluir/{id:guid}")]
        public async Task<IActionResult> Excluir([FromRoute] Guid id)
        {
            return Ok(await _contatoService.Excluir(id));
        }
    }
}