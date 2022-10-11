using LojaVeiculos.Models;

namespace LojaVeiculos.Interfaces
{
    public interface ILoginRepository
    {
        string Logar(string email, string senha);
    }
}
