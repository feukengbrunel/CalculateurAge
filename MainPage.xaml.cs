using CalculateurAge.ViewModels;

namespace CalculateurAge;

public partial class MainPage : ContentPage
{
	// Le ViewModel est fourni par l injection de dependances
	// (voir MauiProgram.cs).
	public MainPage(CalculateurViewModel vm)
	{
		InitializeComponent();
		// Objet dans lequel tous les {Binding} de la page
		// vont chercher leurs valeurs.
		BindingContext = vm;
	}
}
