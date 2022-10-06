using LojaVeiculos.Interfaces;
using LojaVeiculos.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace LojaVeiculos.Repositories
{
    public class VeiculoRepositorie : IRepository<Veiculo>
    {
        public void Delete(Veiculo entity)
        {
            throw new System.NotImplementedException();
        }

        public ICollection<Veiculo> FindAll()
        {
            throw new System.NotImplementedException();
        }

        public Veiculo FindById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Veiculo Insert(Veiculo entity)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Veiculo entity)
        {
            throw new System.NotImplementedException();
        }

        public void UpdatePartial(JsonPatchDocument patch, Veiculo entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
