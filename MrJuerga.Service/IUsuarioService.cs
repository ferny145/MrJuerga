using MrJuerga.Entity;
using System.Collections.Generic;

namespace MrJuerga.Service
{
    public interface IUsuarioService : IService<Usuario>
    {
         IEnumerable<Usuario> FetchUsuariobyName (string name);
    }
}