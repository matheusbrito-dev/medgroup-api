using APIMED.Businnes.DTO;
using APIMED.Businnes.Service.Interface;
using APIMED.Businnes.ViewModel;
using APIMED.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;

namespace APIMED.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatoController : Controller
    {
        private readonly IContatoService _contatoService;
        private readonly IMapper _mapper;

        public ContatoController(IContatoService funcionarioService, IMapper mapper)
        {
            _contatoService = funcionarioService;
            _mapper = mapper;
        }
        [HttpPost("Incluir")]
        public async Task<IActionResult> Incluir([FromBody] ContatoDTO contato)
        {
            return Ok(await _contatoService.Incluir(_mapper.Map<ContatoViewModel>(contato)));
        }
        [HttpPut("Alterar/{id:guid}")]
        public async Task<IActionResult> Alterar([FromBody] ContatoDTO obj)
        {
            return Ok(await _contatoService.Alterar(_mapper.Map<ContatoViewModel>(obj)));
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
        [HttpPut("AlterarAtivo/{id:guid}")]
        public async Task<IActionResult> Ativo(Guid id)
        {
            return Ok(await _contatoService.Ativar(id));
        }
    }
}