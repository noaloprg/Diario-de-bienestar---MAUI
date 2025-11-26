using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diario_bienestar.Respositories
{
    public interface IRepository<Registro>
    {
        IEnumerable<Registro> GetAll();
        Registro GetById(int id);
        bool Add(Registro producto);
        bool Update(Registro producto);
        bool Delete(int id);
    }
}
