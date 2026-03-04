using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopGestProjeto.Api.Models.DTOs;
using ShopGestProjeto.Api.Models;
using ShopGestProjeto.Api.Data;
using Microsoft.EntityFrameworkCore;
using ShopGestProjeto.Api.Services.Security;

namespace ShopGestProjeto.Api.Controllers
{
    [ApiController]
    [Route("api/pedidos")]
    public class PedidosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PedidosController(AppDbContext context)
        {
            _context = context;

        }

        [HttpGet]
        public async Task<ActionResult<List<PedidoEntregadorDto>>> GetPedidos()
        {
            var pedidos = await _context.Pedidos
                .Select(p => new PedidoEntregadorDto
                {
                    PedidoId = p.Id,
                    ValorTotal = p.ValorTotal,
                    EnderecoEntrega = p.EnderecoEntrega,
                    NomeCliente = p.Cliente.Nome
                })
                .ToListAsync();

            return Ok(pedidos);
        }

        [HttpGet("entregador")]
        public async Task<ActionResult<List<PedidoEntregadorDto>>> GetPedidosEmTransito()
        {
            var pedidos = await _context.Pedidos
                .Where(p => p.Status == StatusPedido.EmTransito)
                .Select(p => new PedidoEntregadorDto
                {
                    PedidoId = p.Id,
                    ValorTotal = p.ValorTotal,
                    EnderecoEntrega = p.EnderecoEntrega,
                    NomeCliente = p.Cliente.Nome
                })
                .ToListAsync();

            return Ok(pedidos);
        }

        [HttpPost("criar-pedido")]
        public async Task<ActionResult> CriarPedido(CreatePedidoDto dto)
        {   

            var newPedido = new Pedido
            {
                ClienteId = dto.ClienteId,
                EnderecoEntrega = dto.EnderecoEntrega,
                ValorTotal = dto.ValorTotal,

                // 🔒 REGRAS FORÇADAS PELO BACKEND
                DataCompra = DateTime.Now,
                Status = StatusPedido.AguardandoPagamento
            };

            _context.Pedidos.Add(newPedido);
            await _context.SaveChangesAsync();

            return Ok(newPedido);
        }
    }
}
