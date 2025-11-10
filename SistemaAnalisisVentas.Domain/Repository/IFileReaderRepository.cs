using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAnalisisVentas.Domain.Repository
{
    public interface IFileReaderRepository
    {
        Task<IEnumerable<T>> ReadAsync<T>(string pathOrEndpoint);
    }
}
