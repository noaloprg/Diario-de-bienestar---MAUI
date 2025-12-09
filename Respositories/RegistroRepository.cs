using Diario_bienestar.Servicios;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diario_bienestar.Respositories
{
    public class RegistroRepository
    {
        private static ObservableCollection<Registro> listaRegistros = new ObservableCollection<Registro>();

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
                JsonRegistrosService.Serializar(listaRegistros);
            }
            else esCorrecto = false;

            return esCorrecto;
        }

        public bool Delete(Registro reg)
        {
            bool esEliminado;
            if (listaRegistros.Contains(reg))
            {
                listaRegistros.Remove(reg);
                esEliminado = true;
                JsonRegistrosService.Serializar(listaRegistros);
            }
            else esEliminado = false;

            return esEliminado;
        }

        public ObservableCollection<Registro> GetAll()
        {
            return listaRegistros;
        }

        public bool AddAll(IList<Registro> listaPasada)
        {
            if (listaPasada.Count > 0)
            {
                listaRegistros = new ObservableCollection<Registro>(listaPasada);
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
