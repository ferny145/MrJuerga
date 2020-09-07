using MrJuerga.Entity;
using System.Collections.Generic;

namespace MrJuerga.Service
{
    public interface IProductoService : IService<Producto>
    {
          IEnumerable<Producto> FetchProductobyName (string name);
          IEnumerable<Producto> FetchProductobyCategory (string name);
    }
}