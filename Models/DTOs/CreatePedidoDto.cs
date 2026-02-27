namespace ShopGestProjeto.Api.Models.DTOs
{
    public class CreatePedidoDto
    {
        public int ClienteId { get; set; }
        public string EnderecoEntrega { get; set; }
        public decimal ValorTotal { get; set; }
    }
    public class PedidoEntregadorDto
    {
        public int PedidoId { get; set; }
        public decimal ValorTotal { get; set; }
        public string EnderecoEntrega { get; set; }
        public string NomeCliente { get; set; }
    }


}
