using LojaVeiculos.Context;
using LojaVeiculos.Interfaces;
using LojaVeiculos.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LojaVeiculos.Repositories
{
    public class CartaoRepository : IRepository<Cartao>
    {
        LojaVeiculosContext ctx;

        public CartaoRepository(LojaVeiculosContext _ctx)
        {
            ctx = _ctx;
        }

        public void Delete(Cartao entity)
        {
            ctx.Cartao.Remove(entity);
        }

        public ICollection<Cartao> FindAll()
        {
            return ctx.Cartao.ToList();
        }

        public Cartao FindById(int id)
        {
            return ctx.Cartao.Find(id);
        }

        public Cartao Insert(Cartao entity)
        {
            ctx.Cartao.Add(entity);
            ctx.SaveChanges();
            return entity;
        }

        public void Update(Cartao entity)
        {
            ctx.Entry(entity).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public void UpdatePartial(JsonPatchDocument patch, Cartao entity)
        {
            patch.ApplyTo(entity);
            ctx.Entry(entity).State = EntityState.Modified;
            ctx.SaveChanges();
        }
    }
}