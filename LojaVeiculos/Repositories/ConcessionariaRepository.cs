using LojaVeiculos.Context;
using LojaVeiculos.Interfaces;
using LojaVeiculos.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json;

namespace LojaVeiculos.Repositories
{
    public class ConcessionariaRepository : IRepository<Concessionaria>
    {

        LojaVeiculosContext ctx;

        public ConcessionariaRepository(LojaVeiculosContext _ctx)
        {
            ctx = _ctx;
        }

        public void Delete(Concessionaria entity)
        {
            //Checa constraint - Não deixa excluir se tiver filhos
            var query = from item in ctx.Veiculo
                        where item.IdConcessionaria == entity.Id
                        select item.Id;
            if (query.Count() > 0)
                throw new ConstraintException("Exclusão inválida (Existem veículos cadastrados com essa concessionária)");


            //
            ctx.Concessionaria.Remove(entity);
            
            ctx.SaveChanges();
        }

        public ICollection<Concessionaria> FindAll()
        {
            return ctx.Concessionaria
                                .Include(v => v.Veiculos).ThenInclude(m => m.Modelo).ThenInclude(a => a.Marca)
                                .ToList();
        }

        public Concessionaria FindById(int id)
        {
            return ctx.Concessionaria
                                .Include(v => v.Veiculos).ThenInclude(m => m.Modelo).ThenInclude(a => a.Marca)
                                .FirstOrDefault(v => v.Id == id);
        }

        public Concessionaria Insert(Concessionaria entity)
        {
            ctx.Concessionaria.Add(entity);
            ctx.SaveChanges();
            return entity;
        }

        public void Update(Concessionaria entity)
        {
            ctx.Entry(entity).State= EntityState.Modified;
            ctx.SaveChanges();
           
        }

        

        public void UpdatePartial(JsonPatchDocument patch, Concessionaria entity)
        {
            patch.ApplyTo(entity);
            ctx.Entry(entity).State=EntityState.Modified;
            ctx.SaveChanges();
        }
    }
}
