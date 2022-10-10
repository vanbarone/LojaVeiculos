using LojaVeiculos.Models;
using LojaVeiculos.Interfaces;
using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch;
using LojaVeiculos.Context;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LojaVeiculos.Repositories
{
    public class MarcaRepository : IRepository<Marca>
    {
        LojaVeiculosContext ctx;

        public MarcaRepository(LojaVeiculosContext _ctx)
        {
            ctx = _ctx;
        }
        public void Delete(Marca entity)
        {
            ctx.Marca.Remove(entity);
        }

        public ICollection<Marca> FindAll()
        {
            return ctx.Marca.ToList();
        }

        public Marca FindById(int id)
        {
            return ctx.Marca.Find(id);
        }

        public Marca Insert(Marca entity)
        {
            ctx.Marca.Add(entity);
            ctx.SaveChanges();
            return entity;
        }

        public void Update(Marca entity)
        {
            ctx.Entry(entity).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        //não faz sentido ter um partial
        public void UpdatePartial(JsonPatchDocument patch, Marca entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
