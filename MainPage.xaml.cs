namespace Diario_bienestar
{
    public partial class MainPage : ContentPage
    {
        private static string claveNombreUsuario = "nombreUsuario";
        public MainPage()
        {
            InitializeComponent();
            Preferences.Clear();

        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(500);
            PedirNombre();

        }
        private void Personalizar()
        {
            //recuperarlo
            var nombre = Preferences.Get(claveNombreUsuario, "INVITADO").ToString();
            lblBienvenida.Text = string.Format("BIENVENID@ {0}", nombre);
        }
        private async Task PedirNombre()
        {
            //si no hay introducido una clave del nombre de usuario
            if (!Preferences.ContainsKey(claveNombreUsuario))
            {
                //crear cuadro en el que pide un nombre de usuario 
                string nombre = await DisplayPromptAsync("Configuracion", "Introduzca el nombre del usuario del diario:",
                     "Guardar", "Cancelar", placeholder: "Nombre", maxLength: 20);

                //si es nulo vuelve a pedir
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    await DisplayAlert("Error", "Debe introducir un nombre válido", "Volver");
                    await PedirNombre();
                }
                else
                {
                    //guardarlo
                    Preferences.Set(claveNombreUsuario, nombre);
                    Personalizar();
                }
            }
        }
    }
}
