using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace FirstMauiApp.ViewModels;
internal class MainViewModel : ObservableObject
{
    public ICommand NewGameCommand { get; private set; }
    public ICommand DefaultGameCommand { get; private set; }

    public MainViewModel()
    {
        NewGameCommand = new AsyncRelayCommand(NewGame);
        DefaultGameCommand = new AsyncRelayCommand(DefaultGame);
    }

    private async Task NewGame()
    {
        await Shell.Current.GoToAsync(nameof(Views.NewGamePage));
    }

    private async Task DefaultGame()
    {
        await Shell.Current.GoToAsync(nameof(Views.GuessTheNumberPage));
    }

}

