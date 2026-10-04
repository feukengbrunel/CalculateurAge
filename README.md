# CalculateurAge

TP1 .NET MAUI : Routing et MVVM. Application qui calcule l'âge à partir d'un nom et d'une date de naissance.

## Historique par phase

| Commit | Contenu |
|---|---|
| Étape 0 | Projet .NET MAUI créé sans le contenu d'exemple |
| Phase A | Version code-behind (`x:Name`, `Clicked`) |
| Phase B | Seconde page `ResultatPage`, route Shell, `[QueryProperty]` |
| Phase C | Réécriture en MVVM : `BaseViewModel`, `RelayCommand`, `CalculateurViewModel` |
| Fonctionnalités 1 à 6 | Activité 6, une fonctionnalité par commit |

## Fonctionnalités ajoutées (activité 6)

1. **Majeur / Mineur** : propriété `Statut` calculée dans le ViewModel.
2. **Refus d'une date future** : `CanExecute` grise le bouton Calculer et `ErreurDate` affiche un message.
3. **Jours avant le prochain anniversaire** : `ProchainAnniversaire`, avec gestion du 29 février.
4. **Commande Effacer** : `EffacerCommand` remet tous les champs à zéro.
5. **Historique des calculs** : `ObservableCollection<string>` affichée avec un `BindableLayout`.
6. **Navigation pilotée par le ViewModel** : `VoirDetailCommand` passe par `INavigationService`. `ResultatPage` a son propre `ResultatViewModel` (`IQueryAttributable`). Pages et ViewModels sont injectés via `MauiProgram.cs`.

Aucun ViewModel ne contient `Label`, `Entry`, `DisplayAlert` ni `Shell` : seul `ShellNavigationService` connaît Shell.

## Organisation

```
CalculateurAge/
  Services/      INavigationService.cs, ShellNavigationService.cs
  ViewModels/    BaseViewModel.cs, RelayCommand.cs, CalculateurViewModel.cs, ResultatViewModel.cs
  Views/         ResultatPage.xaml(.cs)
  MainPage.xaml(.cs), AppShell.xaml(.cs), MauiProgram.cs
```

## Lancer

Ouvrir `CalculateurAge.csproj` dans Visual Studio 2026 (.NET 10, workload MAUI) et démarrer sur l'émulateur Android,
ou en ligne de commande : `dotnet build -t:Run -f net10.0-android`.
