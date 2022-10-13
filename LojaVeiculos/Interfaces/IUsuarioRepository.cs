using LojaVeiculos.Models;
using System.Collections.Generic;

namespace LojaVeiculos.Interfaces
{
    public interface IUsuarioRepository
    {
        public void Delete(Usuario entity);

        public Usuario FindById(int id);

        public Usuario FindByEmail(string email);

        public Usuario FindByEmail(string email, int id);
    }
}
