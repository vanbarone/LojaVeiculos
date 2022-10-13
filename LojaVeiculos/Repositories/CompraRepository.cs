using LojaVeiculos.Context;
using LojaVeiculos.Interfaces;
using LojaVeiculos.Models;
using LojaVeiculos.Utils;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Transactions;
using System.Xml.Schema;

namespace LojaVeiculos.Repositories
{
    public class CompraRepository : IRepository<Compra>
    {
        LojaVeiculosContext ctx;

        public CompraRepository(LojaVeiculosContext _ctx)
        {
            ctx = _ctx;
        }


        public void Delete(Compra entity)
        {
            throw new System.NotImplementedException();
        }

        public ICollection<Compra> FindAll()
        {
            return ctx.Compra
                            .Include(c => c.Cliente).ThenInclude(u => u.Usuario).ThenInclude(t => t.TipoUsuario)
                            .Include(i => i.ItensCompra).ThenInclude(v => v.Veiculo).ThenInclude(m => m.Modelo).ThenInclude(a => a.Marca)
                            .ToList();
        }

        public Compra FindById(int id)
        {
            return ctx.Compra.Include(c => c.Cliente).ThenInclude(u => u.Usuario).ThenInclude(t => t.TipoUsuario)
                            .Include(i => i.ItensCompra).ThenInclude(v => v.Veiculo).ThenInclude(m => m.Modelo).ThenInclude(a => a.Marca)
                            .FirstOrDefault(v => v.Id == id);
        }

        public Compra Insert(Compra entity)
        {
            decimal total = 0;

            IVeiculoRepository repoVeiculo = new VeiculoRepository(ctx);


            //Verifica se o cliente existe no BD
            IRepository<Cliente> repoCliente = new ClienteRepository(ctx);

            if (repoCliente.FindById(entity.IdCliente) == null)
            {
                throw new ConstraintException("Cliente não cadastrado");
            }


            //Verifica se todos veiculos existem no BD
            foreach (ItemCompra i in entity.ItensCompra)
            {
                Veiculo veiculo = repoVeiculo.FindById(i.IdVeiculo);

                if (veiculo == null)
                {
                    throw new ConstraintException($"Veículo[{i.IdVeiculo}] não cadastrado");
                }else if (veiculo.Status == Util.VeiculoStatus_Vendido)
                {
                    throw new ConstraintException($"Veículo[{i.IdVeiculo}] já foi vendido");
                }
                else
                {
                    total += veiculo.Valor;
                }
            }

            //Verifica se os itensVenda estão repetidos
            var query = from item in entity.ItensCompra
                        group item by item.IdVeiculo into fileGroup
                        where fileGroup.Count() > 1
                        select fileGroup;
            if (query.Count() > 0)
                throw new ConstraintException("Veículos duplicados");


            //
            if (entity.CartaoMesVencimento < 1 && entity.CartaoMesVencimento > 12)
                throw new ConstraintException("Mês de vencimento do cartão inválido");

            //
            if (entity.CartaoAnoVencimento < DateTime.Now.Year)
                throw new ConstraintException("Ano de vencimento do cartão inválido");


            entity.Data = DateTime.Now;
            entity.FormaPagto = "Crédito";
            entity.VlTotal = total;


            //Inicia gravação
            var transaction = ctx.Database.BeginTransaction();

            try
            {
                ctx.Compra.Add(entity);

                ctx.SaveChanges();


                //Altera status do veiculo
                foreach (ItemCompra i in entity.ItensCompra)
                {
                    repoVeiculo.UpdateStatus(i.IdVeiculo);
                }


                //Salva cartao do cliente
                var cartao = SetarDadosCartao(entity);

                ICartaoRepository repoCartao = new CartaoRepository(ctx);
                repoCartao.Insert(cartao);


                //
                transaction.Commit();

                entity = FindById(entity.Id);

                return entity;
            }
            catch (Exception ex)
            {
                transaction.Rollback();

                throw new Exception("Erro de transação " + ex.Message + ex.InnerException?.Message);
            }
        }

        public void Update(Compra entity)
        {
            throw new System.NotImplementedException();
        }

        public void UpdatePartial(JsonPatchDocument patch, Compra entity)
        {
            throw new System.NotImplementedException();
        }


        private Cartao SetarDadosCartao(Compra entity)
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
