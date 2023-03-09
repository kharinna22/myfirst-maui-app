using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FirstMauiApp.Models;
using FirstMauiApp.Views;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Input;

namespace FirstMauiApp.ViewModels;
internal class NewGameViewModel : ObservableObject
{
    public int MinGuess { get; set; } = 0;
    public int MaxGuess { get; set; } = 30;
    public int MaxChances { get; set; } = 5;
    public bool IsMaxChancesEditable { get; set; } = false;
    public ICommand StartGameCommand { get; private set; }
    public ICommand EnableChancesCommand { get; private set; }
    public ICommand CalculateMaxChancesCommand { get; private set; }

    public NewGameViewModel()
    {
        StartGameCommand = new AsyncRelayCommand(StartGame);

        EnableChancesCommand = new Command<View>((view) => { EnableChances(view); });
        CalculateMaxChancesCommand = new RelayCommand(CalculateMaxChances);
    }

    private async Task StartGame()
    {
        await Shell.Current.GoToAsync($"{nameof(Views.GuessTheNumberPage)}?minguess={MinGuess}&maxguess={MaxGuess}&maxchances={MaxChances}");
    }

    private void EnableChances(View view)
    {
        IsMaxChancesEditable = true;
        OnPropertyChanged(nameof(IsMaxChancesEditable));
        view.Focus();
        
    }

    private void CalculateMaxChances()
    {
        int range = Math.Abs(MaxGuess - MinGuess);
    
        MaxChances = (range/6) < 1 ? 1 : (range/6);

        OnPropertyChanged(nameof(MaxChances));
    }

}

