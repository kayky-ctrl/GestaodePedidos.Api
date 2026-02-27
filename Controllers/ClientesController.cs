using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopGestProjeto.Api.Data;
using ShopGestProjeto.Api.Models;
using ShopGestProjeto.Api.Models.DTOs;
using ShopGestProjeto.Api.Services.Security;

namespace ShopGestProjeto.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IHashService _hashService;

        public ClientesController(AppDbContext context, IHashService hashService)
        {
            _context = context;
            _hashService = hashService;
        }

        [HttpPost("Criar-cliente")]
        public async Task<ActionResult> CriarCLiente(CreateClienteDto dto)
        {
            var hashCartao = _hashService.GerarHash(dto.NumeroCartao);

            var newCliente = new Cliente
            {
                Nome = dto.Nome,
                CPF = dto.CPF,
                Email = dto.Email,
                HashCartaoCredito = hashCartao,
                ScoreRisco = 0
            };

            _context.Clientes.Add(newCliente);
            await _context.SaveChangesAsync();
            return Ok(newCliente);
        }
    }
}
