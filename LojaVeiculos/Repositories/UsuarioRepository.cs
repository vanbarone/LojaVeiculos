using LojaVeiculos.Context;
using LojaVeiculos.Interfaces;
using LojaVeiculos.Models;
using LojaVeiculos.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LojaVeiculos.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {

        LojaVeiculosContext ctx;

        public UsuarioRepository(LojaVeiculosContext _ctx)
        {
            ctx = _ctx;
        }


        public void Delete(Usuario entity)
        {
            ctx.Usuario.Remove(entity);

            ctx.SaveChanges();
        }

        public Usuario FindById(int id)
        {
            return  ctx.Usuario
                            .Include(t => t.TipoUsuario)
                            .FirstOrDefault(u => u.Id == id);
        }

    }
}
