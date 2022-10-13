using LojaVeiculos.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace LojaVeiculos.Interfaces
{
    public interface IVeiculoRepository: IRepository<Veiculo>
    {
        public Veiculo FindByPlaca(string placa);

        public void UpdateStatus(int id);
    }
}

