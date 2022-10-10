using LojaVeiculos.Models;
using LojaVeiculos.Interfaces;
using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch;
using LojaVeiculos.Context;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LojaVeiculos.Repositories
{
    public class ModeloRepository : IRepository<Modelo>
    {
        LojaVeiculosContext ctx;

        public ModeloRepository(LojaVeiculosContext _ctx)
        {
            ctx = _ctx;
        }

        public void Delete(Modelo entity)
        {
            ctx.Modelo.Remove(entity);
            ctx.SaveChanges();
        }

        public ICollection<Modelo> FindAll()
        {
            return ctx.Modelo.ToList();
        }

        public Modelo FindById(int id)
        {
            return ctx.Modelo.Find(id);
        }

        public Modelo Insert(Modelo modelo)
        {
            ctx.Modelo.Add(modelo);
            ctx.SaveChanges();
            return modelo;
        }

        public void Update(Modelo entity)
        {
            ctx.Entry(entity).State = EntityState.Modified;
            ctx.SaveChanges();

        }

        
        public void UpdatePartial(JsonPatchDocument patch, Modelo entity)
        {
            patch.ApplyTo(entity);
            ctx.Entry(entity).State = EntityState.Modified;
            ctx.SaveChanges();
        }
    }
}
