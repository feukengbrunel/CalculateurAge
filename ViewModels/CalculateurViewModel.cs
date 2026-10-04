using System.Collections.ObjectModel;

namespace CalculateurAge.ViewModels;

// Contient l ETAT de l ecran et les ACTIONS possibles.
public class CalculateurViewModel : BaseViewModel
{
	// Champs prives : la vraie donnee.
	private string _nom = "";
	private DateTime _dateNaissance
		= DateTime.Today.AddYears(-20);
	private string _resultat = "";
	private bool _resultatVisible;

	// Proprietes publiques : ce que le XAML voit.
	public string Nom
	{
		get => _nom;
		set { if (SetField(ref _nom, value))
				CalculerCommand.Rafraichir(); }
	}

	public DateTime DateNaissance
	{
		get => _dateNaissance;
		set
		{
			if (!SetField(ref _dateNaissance, value)) return;
			// Fonctionnalite 2 : on refuse une date future.
			OnPropertyChanged(nameof(ErreurDate));
			OnPropertyChanged(nameof(DateInvalide));
			CalculerCommand.Rafraichir();
		}
	}

	// Vrai si la date choisie est dans le futur.
	public bool DateInvalide => DateNaissance.Date > DateTime.Today;

	// Message affiche sous le DatePicker quand la date est refusee.
	public string ErreurDate => DateInvalide
		? "La date de naissance ne peut pas etre dans le futur."
		: "";

	public string Resultat
	{
		get => _resultat;
		set => SetField(ref _resultat, value);
	}

	public bool ResultatVisible
	{
		get => _resultatVisible;
		set => SetField(ref _resultatVisible, value);
	}

	// Fonctionnalite 1 : "Majeur" ou "Mineur" selon l age.
	private string _statut = "";
	public string Statut
	{
		get => _statut;
		set => SetField(ref _statut, value);
	}

	// Fonctionnalite 3 : jours restants avant le prochain anniversaire.
	private string _prochainAnniversaire = "";
	public string ProchainAnniversaire
	{
		get => _prochainAnniversaire;
		set => SetField(ref _prochainAnniversaire, value);
	}

	// Fonctionnalite 5 : historique des calculs, le plus recent en tete.
	// ObservableCollection previent la vue a chaque ajout ou suppression.
	public ObservableCollection<string> Historique { get; } = new();

	// Liee a Button.Command dans le XAML.
	public RelayCommand CalculerCommand { get; }

	// Fonctionnalite 4 : remet tous les champs a zero.
	public RelayCommand EffacerCommand { get; }

	public CalculateurViewModel()
	{
		CalculerCommand = new RelayCommand(
			Calculer,
			() => !string.IsNullOrWhiteSpace(Nom) && !DateInvalide);
		EffacerCommand = new RelayCommand(Effacer);
	}

	private void Effacer()
	{
		Nom = "";
		DateNaissance = DateTime.Today.AddYears(-20);
		Resultat = "";
		Statut = "";
		ProchainAnniversaire = "";
		ResultatVisible = false;
	}

	// La logique metier : aucun controle d interface ici.
	private void Calculer()
	{
		int age = DateTime.Today.Year
		          - DateNaissance.Year;
		if (DateNaissance.Date >
		    DateTime.Today.AddYears(-age)) age--;

		Resultat = $"{Nom}, vous avez {age} ans";
		Statut = age >= 18 ? "Majeur" : "Mineur";

		int jours = JoursAvantAnniversaire(DateNaissance, age);
		ProchainAnniversaire = jours == 0
			? "Joyeux anniversaire !"
			: $"Prochain anniversaire dans {jours} jour(s)";

		Historique.Insert(0,
			$"{DateTime.Now:HH:mm} - {Nom} : {age} ans ({Statut})");

		ResultatVisible = true;
	}

	// AddYears gere le 29 fevrier (ramene au 28 les annees non bissextiles).
	private static int JoursAvantAnniversaire(DateTime naissance, int age)
	{
		DateTime prochain = naissance.Date.AddYears(age);
		if (prochain < DateTime.Today)
			prochain = naissance.Date.AddYears(age + 1);
		return (prochain - DateTime.Today).Days;
	}
}
