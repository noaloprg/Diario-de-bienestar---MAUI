using Diario_bienestar.Respositories;
using Diario_bienestar.Servicios;
using System.Text.Json;

namespace Diario_bienestar;

public partial class ResumenSemanal : ContentPage
{
    //ficheros
    private static string rutaDir = FileSystem.AppDataDirectory;
    private static string nombreFichero = "registros.json";

    //fechas
    private const string FORMATO_FECHA = "dd/MM/yyyy";
    private static DateOnly fechaFinSemana;
    private static DateOnly fechaInicioSemana;
    private const int DIAS_SEMANA = 7;

    //colores
    Color colorVerde = (Color)Application.Current.Resources["Verde"];
    Color colorRojo = (Color)Application.Current.Resources["Rojo"];

    public ResumenSemanal()
    {
        InitializeComponent();
        PonerFechas();
        AsignrResultados();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        PonerFechas();
        AsignrResultados();
    }
    private void PonerFechas()
    {
        //obtener el dia de la semana pero solo el ordinal
        int diaSemana = (int)DateTime.Now.DayOfWeek;

        if (diaSemana == 0) diaSemana = 7;

        //cuantos dias falta para el fin de semana
        int finSemana = DIAS_SEMANA - diaSemana;
        //+1 porque son 7 días pero se cuenta el 1º, si no se va al dia anterior al 1
        int inicioSemana = finSemana - DIAS_SEMANA + 1;

        //obtiene fechas en formato DateOnly para asi poder reutilizar en las Operaciones
        fechaFinSemana = DateOnly.FromDateTime(DateTime.Now.AddDays(finSemana));
        fechaInicioSemana = DateOnly.FromDateTime(DateTime.Now.AddDays(inicioSemana));

        //se asigna al label y ya se convierten a string
        lblRangoSemanal.Text = $"{fechaInicioSemana.ToString(FORMATO_FECHA)} - {fechaFinSemana.ToString(FORMATO_FECHA)}";
    }

    private void AsignrResultados()
    {
        double totalActividad = 0;
        double totalEnergia = 0;

        //solo los registros que sean de esta semana
        var filtradosSemanal = App.repo.GetAll()
            .Where(rg =>
            DateOnly.FromDateTime(rg.fecha) >= fechaInicioSemana &&
            DateOnly.FromDateTime(rg.fecha) <= fechaFinSemana).ToList();

        //solo si hay dias de la semana actual que se calcule
        if (filtradosSemanal.Count > 0)
        {
            foreach (var registro in filtradosSemanal)
            {
                totalActividad += registro.nivelActividadFidisca;
                totalEnergia += registro.nivelEnergia;
            }

            //hacemos media solo con la cantidad que hay, por si no etsan rellenos los 7 dias 
            var promedioActividad = totalActividad / filtradosSemanal.Count * 10;
            var promedioEnergia = totalEnergia / filtradosSemanal.Count;

            CambiarColores(promedioActividad, promedioEnergia);
            lblActividadPromedio.Text = Math.Round(promedioActividad, 2).ToString();
            pbActividadFisica.Progress = promedioActividad / 10;
            lblEnergiaPromedio.Text = Math.Round(promedioEnergia, 2).ToString();
        }
        //si no hay registros semanales se pondra todo a "0"
        else Limpiar();
    }

    private void Limpiar()
    {
        lblActividadPromedio.Text = "0";
        lblEnergiaPromedio.Text = "0";
        pbActividadFisica.Progress = 0;
    }

    //cambiar los colores segun el resultado
    private void CambiarColores(double promedioActividad, double promedioEnergia)
    {
        if (promedioActividad > 5)
        {
            lblActividadPromedio.TextColor = colorVerde;
            pbActividadFisica.ProgressColor = colorVerde;
        }
        else
        {
            lblActividadPromedio.TextColor = colorRojo;
            pbActividadFisica.ProgressColor = colorRojo;

        }

        if (promedioEnergia > 2.5) lblEnergiaPromedio.TextColor = colorVerde;
        else lblEnergiaPromedio.TextColor = colorRojo;

    }

}