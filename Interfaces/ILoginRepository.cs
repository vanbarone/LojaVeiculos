using LojaVeiculos.Models;
using System.Threading.Tasks;

namespace LojaVeiculos.Interfaces
{
    public interface ILoginRepository
    {
        string Logar(string email, string senha);

        Task<string> RecuperarSenha(string email);

        void RedefinirSenha(string email, string codigo, string novaSenha);
    }
}
