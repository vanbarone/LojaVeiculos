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
    public class AdministradorRepository : IRepository<Usuario>
    {

        LojaVeiculosContext ctx;

        public AdministradorRepository(LojaVeiculosContext _ctx)
        {
            ctx = _ctx;
        }


        public void Delete(Usuario entity)
        {
            ctx.Usuario.Remove(entity);

            ctx.SaveChanges();
        }

        public ICollection<Usuario> FindAll()
        {
            return ctx.Usuario.Where(u => u.TipoUsuario.Tipo == Util.TpUsuario_Administrador)
                            .Include(u => u.TipoUsuario)
                            .ToList();
        }

        public Usuario FindById(int id)
        {
            return  ctx.Usuario
                            .Include(t => t.TipoUsuario)
                            .FirstOrDefault(u => u.Id == id && u.TipoUsuario.Tipo == Util.TpUsuario_Administrador);
        }

        public Usuario Insert(Usuario entity)
        {
            //só permite incluir usuário do tipo 'administrador'

            //Pega o id do TipoUsuario 'Administrador'            
            ITipoUsuarioRepository repo = new TipoUsuarioRepository(ctx);
            var tipo = repo.BuscarPorTipo(Util.TpUsuario_Administrador);

            entity.IdTipoUsuario = tipo.Id;


            //criptografa a senha
            entity.Senha = BCrypt.Net.BCrypt.HashPassword(entity.Senha);


            ctx.Usuario.Add(entity);

            // Salva as alterações no banco
             
            ctx.SaveChanges();

            entity = FindById(entity.Id);

            // Retorna o usuário
            return entity;
        }

        public void Update(Usuario entity)
        {
            //Checa se usuário é administrador
            if (entity.TipoUsuario == null)
            {
                entity = FindById(entity.Id);
            }

            if (entity.TipoUsuario.Tipo != Util.TpUsuario_Administrador)
            {
                throw new ConstraintException("Administrador não cadastrado'");
            }


            //criptografa a senha
            if (entity != null)
                entity.Senha = BCrypt.Net.BCrypt.HashPassword(entity.Senha);

            //Salva no BD
            ctx.Usuario.Update(entity);

            ctx.SaveChanges();
        }

        public void UpdatePartial(JsonPatchDocument patch, Usuario entity)
        {
            patch.ApplyTo(entity);

            //Checa se usuário é administrador
            if (entity.TipoUsuario == null)
            {
                entity = FindById(entity.Id);
            }

            if (entity.TipoUsuario.Tipo != Util.TpUsuario_Administrador)
            {
                throw new ConstraintException("Administrador não cadastrado'");
            }

            //criptografa a senha
            if (entity != null)
                entity.Senha = BCrypt.Net.BCrypt.HashPassword(entity.Senha);

            //
            ctx.Entry(entity).State = EntityState.Modified;

            ctx.SaveChanges();
        }
    }
}
