using Diario_bienestar.Respositories;
using System.Threading.Tasks;

namespace Diario_bienestar;

public partial class ListaRegistros : ContentPage
{
    public ListaRegistros()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        PonerLista();
    }


    private void PonerLista()
    {
        if (App.repo.GetAll().ToList().Count > 0)
        {
            cvRegistros.ItemsSource = App.repo.GetAll().OrderByDescending(rg => rg.fecha);
        }

    }

    //cada que se pulsa uno de los botones
    private async void OnBotonClicked(object sender, EventArgs e)
    {
        var boton = sender as Button;
        //obtine el registro
        Registro registro = boton?.CommandParameter as Registro;

        //si hay alguna seleccion actualmente
        if (registro != null)
        {
            bool esBorrado = await DisplayAlert("Borrado", $"¿Seguro que desea eliminar el registro del día {registro.fecha}?"
                , "Aceptar", "Cancelar");

            if (esBorrado)
            {
                if (App.repo.GetAll().Contains(registro))
                {
                    if (App.repo.Delete(registro))
                    {
                        await DisplayAlert("Eliminado", $"El registro del día {registro.fecha} has sido eliminado", "volver");
                        ActualizarLista();
                    }
                }
                else await DisplayAlert("Error", "No se pudo eliminar", "volver");
            }
        }

    }

    private void ActualizarLista()
    {
        PonerLista();
    }

}
