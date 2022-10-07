using LojaVeiculos.Context;
using LojaVeiculos.Interfaces;
using LojaVeiculos.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LojaVeiculos.Repositories
{
    public class VendaRepositorie : IRepository<Venda>
    {
        LojaVeiculosContext ctx;

        public VendaRepositorie(LojaVeiculosContext _ctx)
        {
            ctx = _ctx;
        }


        public void Delete(Venda entity)
        {
            throw new System.NotImplementedException();
        }

        public ICollection<Venda> FindAll()
        {
            return ctx.Venda
                            .Include(c => c.Cliente).ThenInclude(u => u.Usuario).ThenInclude(t => t.TipoUsuario)
                            .Include(i => i.ItensVenda).ThenInclude(v => v.Veiculo).ThenInclude(m => m.Modelo).ThenInclude(a => a.Marca)
                            .ToList();
        }

        public Venda FindById(int id)
        {
            return ctx.Venda.FirstOrDefault(v => v.Id == id);
        }

        public Venda Insert(Venda entity)
        {
            ////Verifica se o cliente existe
            //IRepository<Cliente> repoCliente = new ModeloRepository(ctx);

            //if (repoCliente.FindById(entity.idCliente) == null)
            //{
            //    throw new ConstraintException("Cliente não cadastrado");
            //}

            ctx.Venda.Add(entity);

            ctx.SaveChanges();

            entity = FindById(entity.Id);

            return entity;
        }

        public void Update(Venda entity)
        {
            throw new System.NotImplementedException();
        }

        public void UpdatePartial(JsonPatchDocument patch, Venda entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
