using LojaVeiculos.Context;
using LojaVeiculos.Interfaces;
using LojaVeiculos.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Transactions;

namespace LojaVeiculos.Repositories
{
    public class VendaRepository : IRepository<Venda>
    {
        LojaVeiculosContext ctx;

        public VendaRepository(LojaVeiculosContext _ctx)
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
            return ctx.Venda.Include(c => c.Cliente).ThenInclude(u => u.Usuario).ThenInclude(t => t.TipoUsuario)
                            .Include(i => i.ItensVenda).ThenInclude(v => v.Veiculo).ThenInclude(m => m.Modelo).ThenInclude(a => a.Marca)
                            .FirstOrDefault(v => v.Id == id);
        }

        public Venda Insert(Venda entity)
        {
            IVeiculoRepository repoVeiculo = new VeiculoRepository(ctx);


            ////Verifica se o cliente existe no BD
            IRepository<Cliente> repoCliente = new ClienteRepository(ctx);

            if (repoCliente.FindById(entity.IdCliente) == null)
            {
                throw new ConstraintException("Cliente não cadastrado");
            }


            //Verifica se todos veiculos existem no BD
            foreach (ItemVenda i in entity.ItensVenda)
            {
                if (repoVeiculo.FindById(i.IdVeiculo) == null)
                {
                    throw new ConstraintException($"Veículo[{i.IdVeiculo}] não cadastrado");
                }
            }

            //Verifica se os itensVenda estão repetidos




            var transaction = ctx.Database.BeginTransaction();

            try
            {
                ctx.Venda.Add(entity);

                ctx.SaveChanges();


                //Altera status do veiculo
                foreach (ItemVenda i in entity.ItensVenda)
                {
                    //IVeiculoRepository repoVeiculo = new VeiculoRepositorie(ctx);
                    repoVeiculo.UpdateStatus(i.IdVeiculo);
                }


                //Salva cartao do cliente
                var cartao = SetarDadosCartao(entity);

                IRepository<Cartao> repoCartao = new CartaoRepository(ctx);
                repoCartao.Insert(cartao);


                //
                transaction.Commit();

                entity = FindById(entity.Id);

                return entity;
            }
            catch (Exception ex)
            {
                transaction.Rollback();

                throw new Exception("Erro de transação");
            }
        }

        public void Update(Venda entity)
        {
            throw new System.NotImplementedException();
        }

        public void UpdatePartial(JsonPatchDocument patch, Venda entity)
        {
            throw new System.NotImplementedException();
        }


        private Cartao SetarDadosCartao(Venda entity)
        {
            Cartao cartao = new Cartao();

            cartao.IdCliente = entity.IdCliente;
            cartao.Numero = entity.CartaoCpf;
            cartao.Titular = entity.CartaoTitular;
            cartao.Bandeira = entity.CartaoBandeira;
            cartao.Cpf = entity.CartaoCpf;
            cartao.MesVencimento = entity.CartaoMesVencimento;
            cartao.AnoVencimento = entity.CartaoAnoVencimento;
            cartao.CodSeguranca = entity.CartaoCodSeguranca;

            return cartao;
        }
    }
}
