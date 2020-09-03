using MrJuerga.Entity;
using System.Collections.Generic;

namespace MrJuerga.Repository
{
    public interface IUsuarioRepository: IRepository<Usuario>
    {
          IEnumerable<Usuario> FetchUsuariobyName (string name);
    }
}