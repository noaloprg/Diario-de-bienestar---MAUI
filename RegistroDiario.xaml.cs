using Diario_bienestar.Respositories;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Diario_bienestar;

public partial class RegistroDiario : ContentPage
{
    private static string rutaDir = FileSystem.AppDataDirectory;

    private static string nombreFichero = "registros.json";

    private static RegistroRepository _repository;
    public RegistroDiario()
    {
        InitializeComponent();

        Inicializar();
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
        if (_repository.Add(CrearRegistro()))
        {
            string json = Serializar();
            //[chema] guardar en Preferences
            Preferences.Set("listaRegistros", json);

            /*
             * CORRECTO
             * await DisplayAlert("Correcto", "Registro diario guardado", "Volver");
             */

            //PRUEBAS
            string jsonContenido = File.ReadAllText(Path.Combine(rutaDir, nombreFichero));
            await DisplayAlert("Comprobar", $"{jsonContenido}", "volver");
        }
        else
            await DisplayAlert("Error", $"No se puedo guardar el registro," +
                $" ya hay un registro del día {DateOnly.FromDateTime(dpFecha.Date)}", "Volver");

    }

    private Registro CrearRegistro()
    {
        DateTime fecha = dpFecha.Date;
        string sentimientos = edPensamientos.Text;
        double nvActFisica = slActividad.Value;
        int nvEnergia = Convert.ToInt32(lblNivelEnergia.Text);

        return new Registro(fecha, sentimientos, nvActFisica, nvEnergia);
    }


    //serializar la lista del repositorio para guardarlo en Ficheros
    //[chema] guardar el string json en Preferences
    private static String Serializar()
    {
        string json = "[]";
        if (Directory.Exists(rutaDir))
        {
            json = JsonSerializer.Serialize(_repository.GetAll(), Opciones());
            //append a lo ya existente
            File.WriteAllText(Path.Combine(rutaDir, nombreFichero), json);
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
        lblNivelEnergia.Text = "0";
    }

    private static void Inicializar()
    {
        _repository = new RegistroRepository();

        //PRUEBAS borrar cada que se inicie la app, para no acumular
        if (File.Exists(Path.Combine(rutaDir, nombreFichero)))
        {
            File.Delete(Path.Combine(rutaDir, nombreFichero));
        }
    }

}