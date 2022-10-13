using LojaVeiculos.Context;
using LojaVeiculos.Interfaces;
using LojaVeiculos.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LojaVeiculos.Repositories
{
    public class CartaoRepository : ICartaoRepository
    {
        LojaVeiculosContext ctx;

        public CartaoRepository(LojaVeiculosContext _ctx)
        {
            ctx = _ctx;
        }


        public Cartao Insert(Cartao entity)
        {
            ctx.Cartao.Add(entity);

            ctx.SaveChanges();

            return entity;
        }
    }
}