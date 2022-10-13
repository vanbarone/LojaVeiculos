using LojaVeiculos.Models;

namespace LojaVeiculos.Interfaces
{
    public interface ICartaoRepository
    {
        public Cartao Insert(Cartao entity);
    }
}
