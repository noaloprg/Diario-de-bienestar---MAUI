using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diario_bienestar.Respositories
{
    public class Registro
    {
        public DateTime fecha { get; set; }
        public string sentimientos { get; set; }
        public double nivelActividadFisica { get; set; }
        public int nivelEnergia { get; set; }

        public Registro() { }

        public Registro(DateTime fecha, string sentimientos, double nivelActividadFidisca, int nivelEnergia)
        {
            this.fecha = fecha;
            this.sentimientos = sentimientos;
            this.nivelActividadFisica = nivelActividadFidisca;
            this.nivelEnergia = nivelEnergia;
        }

        public override bool Equals(object? obj)
        {
            return obj is Registro registro &&
                   fecha == registro.fecha;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(fecha);
        }

        public override string ToString()
        {
            return $"{fecha}, {nivelActividadFisica}, {nivelEnergia}";
        }
    }
}
