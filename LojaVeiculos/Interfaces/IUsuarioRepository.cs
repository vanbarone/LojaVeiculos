using LojaVeiculos.Models;
using System.Collections.Generic;

namespace LojaVeiculos.Interfaces
{
    public interface IUsuarioRepository
    {
        public void Delete(Usuario entity);

        public Usuario FindById(int id);
    }
}
