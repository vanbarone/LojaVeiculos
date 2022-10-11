﻿using LojaVeiculos.Context;
using LojaVeiculos.Interfaces;
using LojaVeiculos.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LojaVeiculos.Repositories
{
    public class ClienteRepository : IRepository<Cliente>
    {
        LojaVeiculosContext ctx;

        public ClienteRepository(LojaVeiculosContext _ctx)
        {
            ctx = _ctx;
        }
        public void Delete(Cliente entity)
        {
            ctx.Cliente.Remove(entity);

            //Apaga tb o usuario
            IRepository<Usuario> repo = new UsuarioRepository(ctx);
            repo.Delete(entity.Usuario);
        }

        public ICollection<Cliente> FindAll()
        {
            return ctx.Cliente
                        .Include(u => u.Usuario).ThenInclude(t => t.TipoUsuario)
                        .Include(c=> c.Cartoes)
                        .ToList();
        }

        public Cliente FindById(int id)
        {
            return ctx.Cliente
                        .Include(u => u.Usuario).ThenInclude(t => t.TipoUsuario)
                        .Include(c => c.Cartoes)
                        .FirstOrDefault(u => u.Id == id);
        }

        public Cliente Insert(Cliente entity)
        {
            //Pega o id do TipoUsuario 'Cliente'
            ITipoUsuarioRepository repo = new TipoUsuarioRepository(ctx);
            var tipo = repo.BuscarPorTipo("Cliente");

            entity.Usuario.IdTipoUsuario = tipo.Id;   

            //criptografa a senha
            entity.Usuario.Senha = BCrypt.Net.BCrypt.HashPassword(entity.Usuario.Senha);

            ctx.Cliente.Add(entity);

            // Salva as alterações no banco

            ctx.SaveChanges();

            entity = FindById(entity.Id);

            return entity;
        }

        public void Update(Cliente entity)
        {
            //criptografa a senha
            if (entity != null)
                entity.Usuario.Senha = BCrypt.Net.BCrypt.HashPassword(entity.Usuario.Senha);

            //Salva no BD
            ctx.Cliente.Update(entity);

            ctx.SaveChanges();
        }

        public void UpdatePartial(JsonPatchDocument patch, Cliente entity)
        {
            patch.ApplyTo(entity);

            //criptografa a senha
            if (entity.Usuario != null)
                entity.Usuario.Senha = BCrypt.Net.BCrypt.HashPassword(entity.Usuario.Senha);

            //
            ctx.Entry(entity).State = EntityState.Modified;

            ctx.SaveChanges();
        }
    }
}
