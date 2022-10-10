using LojaVeiculos.Models;
using System.Collections.Generic;

namespace LojaVeiculos.Interfaces
{
    public interface ITipoUsuarioRepository
    {
        ICollection<TipoUsuario> ListarTodos();

        TipoUsuario BuscarPorId(int id);
    }
}
