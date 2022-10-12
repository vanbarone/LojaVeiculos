using LojaVeiculos.Models;
using LojaVeiculos.Interfaces;
using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch;
using LojaVeiculos.Context;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Data;

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
            return ctx.Modelo
                            .Include(m => m.Marca)
                            .Include(v => v.Veiculos)
                            .ToList();
        }

        public Modelo FindById(int id)
        {
            return ctx.Modelo
                            .Include(m => m.Marca)
                            .Include(v => v.Veiculos)
                            .FirstOrDefault(m => m.Id == id);
        }

        public Modelo Insert(Modelo modelo)
        {
            ////Verifica se o modelo existe
            IRepository<Marca> repoMarca = new MarcaRepository(ctx);

            if (repoMarca.FindById(modelo.IdMarca) == null)
            {
                throw new ConstraintException("Marca não cadastrada");
            }

            //
            ctx.Modelo.Add(modelo);
            
            ctx.SaveChanges();

            modelo = FindById(modelo.Id);
            
            return modelo;
        }

        public void Update(Modelo entity)
        {
            ////Verifica se o modelo existe
            IRepository<Marca> repoMarca = new MarcaRepository(ctx);

            if (repoMarca.FindById(entity.IdMarca) == null)
            {
                throw new ConstraintException("Marca não cadastrada");
            }

            //
            ctx.Entry(entity).State = EntityState.Modified;
            
            ctx.SaveChanges();
        }

        
        public void UpdatePartial(JsonPatchDocument patch, Modelo entity)
        {
            ////Verifica se o modelo existe
            IRepository<Marca> repoMarca = new MarcaRepository(ctx);

            if (repoMarca.FindById(entity.IdMarca) == null)
            {
                throw new ConstraintException("Marca não cadastrada");
            }

            //
            patch.ApplyTo(entity);
            
            ctx.Entry(entity).State = EntityState.Modified;
            
            ctx.SaveChanges();
        }
    }
}
