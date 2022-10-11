using LojaVeiculos.Context;
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
        }

        public ICollection<Cliente> FindAll()
        {
            return ctx.Cliente.ToList();
        }

        public Cliente FindById(int id)
        {
            return ctx.Cliente.Find(id);
        }

        public Cliente Insert(Cliente entity)
        {
            ctx.Cliente.Add(entity);
            ctx.SaveChanges();
            return entity;
        }

        public void Update(Cliente entity)
        {
            ctx.Entry(entity).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public void UpdatePartial(JsonPatchDocument patch, Cliente entity)
        {
            patch.ApplyTo(entity);
            ctx.Entry(entity).State = EntityState.Modified;
            ctx.SaveChanges();
        }
    }
}

