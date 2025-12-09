using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAnalisisVentas.Application.Interfaces
{
    public interface IETLOrchestratorService
    {
        Task EjecutarETLAsync();
    }
}
