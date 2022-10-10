using LojaVeiculos.Models;

namespace LojaVeiculos.Interfaces
{
    public interface ILoginRepositorie
    {
        string Logar(string email, string senha);
    }
}
