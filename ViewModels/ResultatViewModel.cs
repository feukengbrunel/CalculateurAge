using CalculateurAge.Services;

namespace CalculateurAge.ViewModels;

// Remplace [QueryProperty] + OnAppearing : Shell appelle
// ApplyQueryAttributes avec les parametres de navigation.
public class ResultatViewModel : BaseViewModel, IQueryAttributable
{
	private readonly INavigationService _navigation;

	private string _message = "";
	public string Message
	{
		get => _message;
		set => SetField(ref _message, value);
	}

	private string _statut = "";
	public string Statut
	{
		get => _statut;
		set => SetField(ref _statut, value);
	}

	private string _prochainAnniversaire = "";
	public string ProchainAnniversaire
	{
		get => _prochainAnniversaire;
		set => SetField(ref _prochainAnniversaire, value);
	}

	public RelayCommand RetourCommand { get; }

	public ResultatViewModel(INavigationService navigation)
	{
		_navigation = navigation;
		RetourCommand = new RelayCommand(Retour);
	}

	public void ApplyQueryAttributes(IDictionary<string, object> query)
	{
		Message = $"{query["nom"]}, vous avez {query["age"]} ans";
		Statut = (string)query["statut"];
		ProchainAnniversaire = (string)query["anniversaire"];
	}

	private async void Retour() => await _navigation.Retour();
}
