using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace LojaVeiculos.Interfaces
{
    public interface IRepository<entity>
    {
        public ICollection<entity> FindAll();

        public entity FindById(int id);

        public entity Insert(entity entity);

        public void Update(entity entity);

        public void UpdatePartial(JsonPatchDocument patch, entity entity);

        public void Delete(entity entity);

    }
}

