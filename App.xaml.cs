using Diario_bienestar.Respositories;
using Diario_bienestar.Servicios;

namespace Diario_bienestar
{
    public partial class App : Application
    {
        public static RegistroRepository repo {  get; set; }
        public App()
        {
            InitializeComponent();
            repo = new RegistroRepository();
            repo.AddAll(JsonRegistrosService.Deserializar().ToList());
            MainPage = new AppShell();
        }
    }
}