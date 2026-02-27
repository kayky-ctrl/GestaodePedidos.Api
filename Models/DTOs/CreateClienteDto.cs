namespace ShopGestProjeto.Api.Models.DTOs
{
    public class CreateClienteDto
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }

        // vem do frontend
        public string NumeroCartao { get; set; }
    }

    public class ViewClienteDto
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string NumeroCartao { get; set; }

    }
}
