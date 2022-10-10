using LojaVeiculos.Context;
using LojaVeiculos.Interfaces;
using LojaVeiculos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LojaVeiculos.Repositories
{
    public class UsuarioRepositorie : IRepository<Usuario>
    {

        LojaVeiculosContext ctx;

        public UsuarioRepositorie(LojaVeiculosContext _ctx)
        {
            ctx = _ctx;
        }

        [Authorize]
        public void Delete(Usuario entity)
        {
            ctx.Usuario.Remove(entity);

            ctx.SaveChanges();
        }

        public ICollection<Usuario> FindAll()
        {
            return ctx.Usuario
                .Include(u => u.TipoUsuario)
                .ToList();
        }

        public Usuario FindById(int id)
        {
            return ctx.Usuario
                .Include(t => t.TipoUsuario)
                .FirstOrDefault(u => u.Id == id);
        }

        public Usuario Insert(Usuario entity)
        {

            ctx.Usuario.Add(entity);

            // Salva as alterações no banco
             
            ctx.SaveChanges();

            // Retorna o usuário
            return entity;
        }

        public void Update(Usuario entity)
        {
            ctx.Entry(entity).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public void UpdatePartial(JsonPatchDocument patch, Usuario entity)
        {
            patch.ApplyTo(entity);
            ctx.Entry(entity).State = EntityState.Modified;
            ctx.SaveChanges();
        }
    }
}
