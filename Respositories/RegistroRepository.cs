using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diario_bienestar.Respositories
{
    class RegistroRepository : IRepository<Registro>
    {
        private static List<Registro> listaRegistros = new List<Registro>();

        public bool Add(Registro reg)
        {
            bool esCorrecto;
            if (!listaRegistros.Contains(reg))
            {
                listaRegistros.Add(reg);
                esCorrecto = true;
            }
            else esCorrecto = false;

            return esCorrecto;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Registro> GetAll()
        {
            return listaRegistros;
        }

        public Registro GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Registro reg)
        {
            throw new NotImplementedException();
        }
    }
}
