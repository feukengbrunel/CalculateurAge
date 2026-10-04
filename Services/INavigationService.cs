namespace CalculateurAge.Services;

// Le ViewModel demande une navigation sans connaitre Shell :
// il reste testable dans une application console.
public interface INavigationService
{
	Task AllerVers(string route,
	               IDictionary<string, object>? parametres = null);
	Task Retour();
}
