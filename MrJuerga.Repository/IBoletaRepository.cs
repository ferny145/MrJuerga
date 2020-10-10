using System.Collections.Generic;
using MrJuerga.Entity;

namespace MrJuerga.Repository
{
    public interface IBoletaRepository: IRepository<Boleta>
    {
         IEnumerable<BoletaDTO> FetchTop5Customers ();
         IEnumerable<DetalleBoletaDTO> FetchTop5Products(string inicio, string fin);
         IEnumerable<Boleta> FetchByStatus(string estado);
          
    }
}