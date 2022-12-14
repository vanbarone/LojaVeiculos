using LojaVeiculos.Models;
using LojaVeiculos.Interfaces;
using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch;
using LojaVeiculos.Context;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Data;

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
            //Checa constraint - Não deixa excluir se tiver filhos
            var query = from item in ctx.Modelo
                        where item.IdMarca == entity.Id 
                        select item.Id;
            if (query.Count() > 0)
                throw new ConstraintException("Exclusão inválida (Existem modelos cadastrados com essa marca)");


            ctx.Marca.Remove(entity);
            ctx.SaveChanges();
        }

        public ICollection<Marca> FindAll()
        {
            return ctx.Marca
                            .Include(m => m.Modelos)
                            .ToList();
        }

        public Marca FindById(int id)
        {
            return ctx.Marca
                            .Include(m => m.Modelos)
                            .FirstOrDefault(i => i.Id == id);
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

        //não faz sentido ter um partial por ter só um atributo
        public void UpdatePartial(JsonPatchDocument patch, Marca entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
