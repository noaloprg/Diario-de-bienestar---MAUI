namespace Diario_bienestar;

public partial class RegistroDiario : ContentPage
{
	public RegistroDiario()
	{
		InitializeComponent();

		slSlider.ValueChanged += (s, e) =>
		{
			lblValor.Text = e.NewValue.ToString();
		};
	}
}