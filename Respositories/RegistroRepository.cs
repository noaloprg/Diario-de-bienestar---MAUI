using Diario_bienestar.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diario_bienestar.Respositories
{
    public class RegistroRepository
    {
        private static List<Registro> listaRegistros = new List<Registro>();

        public RegistroRepository()
        {
            //que obtenga todos los valores del fichero al cargar el repositorio
            AddAll(JsonRegistrosService.Deserializar());
        }
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

        public bool AddAll(IList<Registro> listaPasada)
        {
            if (listaPasada.Count > 0)
            {
                listaRegistros = listaPasada.ToList();
                return true;
            }
            else return false;
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
