using Diario_bienestar.Respositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Diario_bienestar.Servicios
{
    internal class JsonRegistrosService
    {
        private static string rutaDir = FileSystem.AppDataDirectory;

        private static string nombreFichero = "registros.json";

        private static string nombreFicheroCompleto = Path.Combine(rutaDir, nombreFichero);
     
        //obtiene una lista de registros que proviene de un fichero json
        public static List<Registro> Deserializar()
        {
            List<Registro> listaJson = new List<Registro>();

            if (File.Exists(nombreFicheroCompleto))
            {
                string json = File.ReadAllText(nombreFicheroCompleto);

                if (!string.IsNullOrWhiteSpace(json))
                {
                    listaJson = JsonSerializer.Deserialize<List<Registro>>(json);
                }
            }
            return listaJson;
        }

        //almacena una lista de registros en un fichero
        public static String Serializar(List<Registro> listaGuardar)
        {
            string json = "[]";
            if (Directory.Exists(rutaDir))
            {
                json = JsonSerializer.Serialize(listaGuardar, Opciones());

                File.WriteAllText(nombreFicheroCompleto, json);
            }
            return json;
        }

        //opciones json
        private static JsonSerializerOptions Opciones()
        {
            var opc = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            return opc;
        }
    }
}
