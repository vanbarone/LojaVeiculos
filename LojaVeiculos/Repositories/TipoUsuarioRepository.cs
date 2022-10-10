using LojaVeiculos.Context;
using LojaVeiculos.Interfaces;
using LojaVeiculos.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Linq;

namespace LojaVeiculos.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuarioRepository
    {

        LojaVeiculosContext ctx;

        public TipoUsuarioRepository(LojaVeiculosContext _ctx)
        {
            ctx = _ctx;
        }
        public TipoUsuario BuscarPorId(int id)
        {
            return ctx.TipoUsuario.Find(id);
        }

        public ICollection<TipoUsuario> ListarTodos()
        {
            // Utiliza o linq para listar os tipos de usuários
            return ctx.TipoUsuario.ToList();
        }
    }
}
