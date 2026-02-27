namespace ShopGestProjeto.Api.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }

        // DADOS SENSÍVEIS (NUNCA DEVEM SAIR EM DTO DE ENTREGADOR)
        public string HashCartaoCredito { get; set; }
        public int ScoreRisco { get; set; } // 0 a 100

        public List<Pedido>? Pedidos { get; set; }
    }

    public class Pedido
    {
        public int Id { get; set; }

        public DateTime DataCompra { get; set; }
        public string EnderecoEntrega { get; set; }
        public decimal ValorTotal { get; set; }

        public StatusPedido Status { get; set; }

        // Relacionamento
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
    }

    public enum StatusPedido
    {
        AguardandoPagamento = 0,
        Pendente = 1,
        EmTransito = 2,
        Entregue = 3
    }
}
