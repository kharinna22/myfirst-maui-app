using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace FirstMauiApp.ViewModels;
internal class MainViewModel : ObservableObject
{
    public ICommand NewGameCommand { get; private set; }

    public MainViewModel()
    {
        NewGameCommand = new AsyncRelayCommand(NewGame);
    }

    private async Task NewGame()
    {
        await Shell.Current.GoToAsync(nameof(Views.NewGamePage));
    }

}

