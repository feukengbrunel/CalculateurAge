using CalculateurAge.ViewModels;

namespace CalculateurAge.Views;

public partial class ResultatPage : ContentPage
{
	// Le ViewModel est fourni par l injection de dependances.
	public ResultatPage(ResultatViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}
