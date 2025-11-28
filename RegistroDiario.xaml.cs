using Diario_bienestar.Respositories;
using Diario_bienestar.Servicios;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Diario_bienestar;

public partial class RegistroDiario : ContentPage
{
    private static string rutaDir = FileSystem.AppDataDirectory;

    private static string nombreFichero = "registros.json";

    private static string nombreFicheroCompleto = Path.Combine(rutaDir, nombreFichero);

    public RegistroDiario()
    {
        InitializeComponent();

        Listeners();
    }

    private async void Listeners()
    {
        //cuando el valor del slider cambie
        slActividad.ValueChanged += async (s, e) =>
        {
            //se actualize el progrssbar
            await pbActividadFisica.ProgressTo(e.NewValue, 500, Easing.SinInOut);
        };

        stNivelEnergia.ValueChanged += (s, e) =>
        {
            lblNivelEnergia.Text = e.NewValue.ToString();
        };
    }

    private async void OnGuardarRegistroClick(object sender, EventArgs e)
    {
        //cada que se guarda un registro se añade al repositorio y se añade al fichero
        if (App.repo.Add(CrearRegistro()))
        {
            string json = JsonRegistrosService.Serializar(App.repo.GetAll().ToList());
            //[chema] guardar en Preferences
            Preferences.Set("listaRegistros", json);

            /*
             * CORRECTO
             * await DisplayAlert("Correcto", "Registro diario guardado", "Volver");
             */

            //PRUEBAS
            string jsonContenido = File.ReadAllText(Path.Combine(nombreFicheroCompleto));
            await DisplayAlert("Comprobar", $"{jsonContenido}", "volver");
        }
        else
            await DisplayAlert("Error", $"No se puedo guardar el registro," +
                $" ya hay un registro del día {DateOnly.FromDateTime(dpFecha.Date)}", "Volver");

    }

    private Registro CrearRegistro()
    {
        DateTime fecha = dpFecha.Date;
        string sentimientos = edPensamientos.Text.ToLower();
        double nvActFisica = slActividad.Value;
        int nvEnergia = Convert.ToInt32(lblNivelEnergia.Text);

        return new Registro(fecha, sentimientos, nvActFisica, nvEnergia);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Limpiar();
    }

    private void Limpiar()
    {
        dpFecha.Date = DateTime.Now;
        edPensamientos.Text = string.Empty;
        slActividad.Value = 0;
        lblNivelEnergia.Text = "1";
    }

   
}