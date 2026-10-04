namespace CalculateurAge.Services;

// Implementation concrete : la seule classe qui parle a Shell.
public class ShellNavigationService : INavigationService
{
	public Task AllerVers(string route,
	                      IDictionary<string, object>? parametres = null)
		=> parametres is null
			? Shell.Current.GoToAsync(route)
			: Shell.Current.GoToAsync(route, parametres);

	// ".." = revenir a la page precedente.
	public Task Retour() => Shell.Current.GoToAsync("..");
}
