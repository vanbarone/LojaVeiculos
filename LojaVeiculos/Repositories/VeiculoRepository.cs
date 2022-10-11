using LojaVeiculos.Context;
using LojaVeiculos.Enuns;
using LojaVeiculos.Interfaces;
using LojaVeiculos.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace LojaVeiculos.Repositories
{
    public class VeiculoRepository : IVeiculoRepository
    {
        LojaVeiculosContext ctx;

        public VeiculoRepository(LojaVeiculosContext _ctx)
        {
            ctx = _ctx;
        }


        public void Delete(Veiculo entity)
        {
            ctx.Veiculo.Remove(entity);

            ctx.SaveChanges();
        }

        public ICollection<Veiculo> FindAll()
        {
            return ctx.Veiculo
                            .Include(c => c.Concessionaria)
                            .Include(m => m.Modelo).ThenInclude(a => a.Marca)
                            .ToList();
        }

        public Veiculo FindById(int id)
        {
            return ctx.Veiculo
                            .Include(c => c.Concessionaria)
                            .Include(m => m.Modelo).ThenInclude(a => a.Marca)
                            .FirstOrDefault(v => v.Id == id);
        }

        public Veiculo FindByPlaca(string placa)
        {
            return ctx.Veiculo
                            .Include(c => c.Concessionaria)
                            .Include(m => m.Modelo).ThenInclude(a => a.Marca)
                            .FirstOrDefault(v => v.Placa == placa);
        }

        public Veiculo Insert(Veiculo entity)
        {
            ////Verifica se o modelo existe
            //IRepository<Modelo> repoModelo = new ModeloRepository(ctx);

            //if (repoModelo.FindById(entity.IdModelo) == null)
            //{
            //    throw new ConstraintException("Modelo não cadastrado");
            //}

            ////Verifica se a concessionária existe
            //IRepository<Concessionaria> repoConcessionaria = new ConcessionariaRepository(ctx);

            //if (repoConcessionaria.FindById(entity.IdConcessionaria) == null)
            //{
            //    throw new ConstraintException("Concessionária não cadastrada");
            //}

            entity.Status = (int)VeiculoEnum.Status.EmEstoque;

            ctx.Veiculo.Add(entity);

            ctx.SaveChanges();

            entity = FindById(entity.Id);

            return entity;
        }

        public void Update(Veiculo entity)
        {
            ////Verifica se o modelo existe
            //IRepository<Modelo> repoModelo = new ModeloRepository(ctx);

            //if (repoModelo.FindById(entity.IdModelo) == null)
            //{
            //    throw new ConstraintException("Modelo não cadastrado");
            //}

            ////Verifica se a concessionária existe
            //IRepository<Concessionaria> repoConcessionaria = new ConcessionariaRepository(ctx);

            //if (repoConcessionaria.FindById(entity.IdConcessionaria) == null)
            //{
            //    throw new ConstraintException("Concessionária não cadastrada");
            //}

            ctx.Veiculo.Update(entity);

            ctx.SaveChanges();
        }

        public void UpdatePartial(JsonPatchDocument patch, Veiculo entity)
        {
            ////Verifica se o modelo existe
            //IRepository<Modelo> repoModelo = new ModeloRepository(ctx);

            //if (repoModelo.FindById(entity.IdModelo) == null)
            //{
            //    throw new ConstraintException("Modelo não cadastrado");
            //}

            ////Verifica se a concessionária existe
            //IRepository<Concessionaria> repoConcessionaria = new ConcessionariaRepository(ctx);

            //if (repoConcessionaria.FindById(entity.IdConcessionaria) == null)
            //{
            //    throw new ConstraintException("Concessionária não cadastrada");
            //}

            patch.ApplyTo(entity);

            ctx.Entry(entity).State = EntityState.Modified;

            ctx.SaveChanges();
        }


        public void UpdateStatus(int id)
        {
            Veiculo entity = FindById(id);

            entity.Status = (int)VeiculoEnum.Status.Vendido;

            ctx.Entry(entity).Property(s => s.Status).IsModified = true;

            ctx.SaveChanges();
        }
    }
}
