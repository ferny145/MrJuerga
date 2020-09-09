using System.Collections.Generic;
using MrJuerga.Entity;

namespace MrJuerga.Service
{
    public interface IBoletaService : IService<Boleta>
    {
        IEnumerable<BoletaDTO> FetchTop5Customers();
    }
}