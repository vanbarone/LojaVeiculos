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


        public TipoUsuario BuscarPorTipo(string tipo)
        {
            return ctx.TipoUsuario.FirstOrDefault(t => t.Tipo == tipo);
        }

    }
}
