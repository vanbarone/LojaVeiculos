using LojaVeiculos.Models;
using System.Collections.Generic;

namespace LojaVeiculos.Interfaces
{
    public interface ITipoUsuarioRepository
    {
        TipoUsuario BuscarPorTipo(string tipo);
    }
}
