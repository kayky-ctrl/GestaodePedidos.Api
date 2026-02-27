namespace ShopGestProjeto.Api.Services.Security
{
    public interface IHashService
    {
        string GerarHash(string valor);
    }
}
