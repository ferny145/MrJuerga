using System.Collections.Generic;
using MrJuerga.Entity;
using MrJuerga.Repository;

namespace MrJuerga.Service.implementation
{
    public class CategoriaService: ICategoriaService
    {
        private ICategoriaRepository categoriaRepository;
        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            this.categoriaRepository=categoriaRepository;
        }

        public bool Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public Categoria Get(int id)
        {
            return categoriaRepository.Get(id);
        }

        public IEnumerable<Categoria> GetAll()
        {
            return categoriaRepository.GetAll();
        }

        public bool Save(Categoria entity)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(Categoria entity)
        {
            throw new System.NotImplementedException();
        }
    }
}