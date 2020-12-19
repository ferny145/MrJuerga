using System.Collections.Generic;
using System.Linq;
using MrJuerga.Entity;
using MrJuerga.Repository.dbcontext;

namespace MrJuerga.Repository.implementation
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private ApplicationDbContext context;

        public CategoriaRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public bool Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public Categoria Get(int id)
        {
           var result = new Categoria();
            try
            {
                result = context.Categorias.Single(x => x.Id == id);
            }

            catch (System.Exception)
            {

                throw;
            }
            return result;
        }

        public IEnumerable<Categoria> GetAll()
        {
          var result = new List<Categoria>();
            try
            {
                result = context.Categorias.ToList();
            }

            catch (System.Exception)
            {

                throw;
            }
            return result;
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