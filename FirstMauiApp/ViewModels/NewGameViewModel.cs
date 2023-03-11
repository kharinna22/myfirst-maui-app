using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Maui.Alerts;
using FirstMauiApp.Models;
using FirstMauiApp.Views;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Windows.Input;
using CommunityToolkit.Maui.Core;

namespace FirstMauiApp.ViewModels;
internal class NewGameViewModel : ObservableObject
{
    int MinGuess = 0;
    int MaxGuess = 30;
    Int64 MaxChances = 5;
    public string MinGuessEntry { get; set; } = "0";
    public string MaxGuessEntry { get; set; } = "30";
    public string MaxChancesEntry { get; set; } = "5";
    public bool IsMaxChancesEditable { get; set; }
    public bool IsMinGuessEmpty { get; set; }
    public bool IsMaxGuessEmpty { get; set; }
    public bool IsMaxChancesErrorVisible { get; set; }
    public bool IsStartButtonEnabled { get; set; } = true;
    public bool AreNumbersRangeEqual { get; set; }
    public string MaxChancesErrorLabel { get; set; } = "";
    public ICommand StartGameCommand { get; private set; }
    public ICommand EnableChancesCommand { get; private set; }
    public ICommand VerifyNumbersRangeCommand { get;private set; }
    public ICommand VerifyNumberChancesCommand { get; private set; }
    //public ICommand CalculateMaxChancesCommand { get; private set; }

    public NewGameViewModel()
    {
        StartGameCommand = new AsyncRelayCommand(StartGame);

        EnableChancesCommand = new Command<View>((view) => { EnableChances(view); });
        VerifyNumbersRangeCommand = new RelayCommand(VerifyNumbersRange);
        VerifyNumberChancesCommand = new RelayCommand(VerifyNumberChances);
        //CalculateMaxChancesCommand = new RelayCommand(CalculateMaxChances);
    }

    private async Task StartGame()
    {

        if (MinGuess > MaxGuess)
        { 
            int aux = MinGuess;
            MinGuess = MaxGuess;
            MaxGuess = aux;

            MinGuessEntry = MinGuess.ToString();
            MaxGuessEntry = MaxGuess.ToString();

            OnPropertyChanged(nameof(MinGuessEntry));
            OnPropertyChanged(nameof(MaxGuessEntry));
        }

        await Shell.Current.GoToAsync($"{nameof(Views.GuessTheNumberPage)}?minguess={MinGuessEntry}&maxguess={MaxGuessEntry}&maxchances={MaxChancesEntry}");
    }

    private void EnableChances(View view)
    {
        IsMaxChancesEditable = true;
        OnPropertyChanged(nameof(IsMaxChancesEditable));
        view.Focus();
    }

    private void VerifyNumbersRange()
    {
        MinGuessEntry = Regex.Replace(MinGuessEntry, "[^0-9]", "");
        MaxGuessEntry = Regex.Replace(MaxGuessEntry, "[^0-9]", "");
        
        try 
        { 
            MinGuess = MinGuessEntry != ""? int.Parse(MinGuessEntry) : MinGuess;
            MaxGuess = MaxGuessEntry != ""? int.Parse(MaxGuessEntry) : MaxGuess;
        }
        catch (OverflowException ex)
        {
            Int64 minGuessAux = Int64.Parse(MinGuessEntry);
            Int64 maxGuessAux = Int64.Parse(MaxGuessEntry);

            MinGuessEntry = minGuessAux > int.MaxValue ? int.MaxValue.ToString() : MinGuessEntry ;
            MaxGuessEntry = maxGuessAux > int.MaxValue ? int.MaxValue.ToString() : MaxGuessEntry ;

            MinGuess = int.Parse(MinGuessEntry);
            MaxGuess = int.Parse(MaxGuessEntry);

            MaxNumberToastAlert();
        }

        VerifyNumbersEmpty();
        VerifyError();

        OnPropertyChanged(nameof(MinGuessEntry));
        OnPropertyChanged(nameof(MaxGuessEntry));

        if (MinGuessEntry != "" && MaxGuessEntry != "")
        {
            if (MinGuess != MaxGuess)
            {
                AreNumbersRangeEqual = false;
                OnPropertyChanged(nameof(AreNumbersRangeEqual));
                
                CalculateMaxChances();
                return;
            }

            AreNumbersRangeEqual = true;
            OnPropertyChanged(nameof(AreNumbersRangeEqual));
        }
    }

    private void CalculateMaxChances()
    {
        int range = Math.Abs(MaxGuess - MinGuess);
        MaxChances = (range / 6) < 1 ? 1 : (range / 6);

        MaxChancesEntry = MaxChances.ToString();
        OnPropertyChanged(nameof(MaxChancesEntry));
    }

    private void VerifyNumbersEmpty()
    {
        IsMinGuessEmpty = MinGuessEntry == "";
        IsMaxGuessEmpty = MaxGuessEntry == "";
        
        OnPropertyChanged(nameof(IsMinGuessEmpty));
        OnPropertyChanged(nameof(IsMaxGuessEmpty));
    }


    private void VerifyNumberChances()
    {
        MaxChancesEntry = Regex.Replace(MaxChancesEntry, "[^0-9]", "");
        try 
        { 
            MaxChances = MaxChancesEntry != "" ? Int64.Parse(MaxChancesEntry) : MaxChances;
        }
        catch { }

        Int64 maxChancesAvailable = 1;
        if (MinGuessEntry != "" && MaxGuessEntry != "" && MaxGuessEntry != MinGuessEntry)
            maxChancesAvailable = Math.Abs(Int64.Parse(MaxGuessEntry) - Int64.Parse(MinGuessEntry)) + 1;

        IsMaxChancesErrorVisible = MaxChancesEntry == "";
        if (IsMaxChancesErrorVisible)
            MaxChancesErrorLabel = "Debe ingresar un número.";

        if (!IsMaxChancesErrorVisible)
        {
            if (MaxChances < 1)
            {
                MaxChancesErrorLabel = "Los intentos deben ser mayor o igual a 1";
                IsMaxChancesErrorVisible = true;
            }

            if (MaxChances > maxChancesAvailable)
            {
                MaxChancesErrorLabel = $"El número máximo de intentos es {maxChancesAvailable}";
                IsMaxChancesErrorVisible = true;
            }
        }

        VerifyError();
        OnPropertyChanged(nameof(MaxChancesEntry));
        OnPropertyChanged(nameof(IsMaxChancesErrorVisible));
        OnPropertyChanged(nameof(MaxChancesErrorLabel));
    }

    private void VerifyError()
    {
        IsStartButtonEnabled = true;
        if (IsMinGuessEmpty || IsMaxGuessEmpty || IsMaxChancesErrorVisible || AreNumbersRangeEqual)
            IsStartButtonEnabled = false;

        OnPropertyChanged(nameof(IsStartButtonEnabled));
    }

    private async void MaxNumberToastAlert()
    {
        // Toast Alert 
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        string text = "El número máximo posible es 2.147.483.647";
        ToastDuration duration = ToastDuration.Long;
        double fontSize = 14;
        var toast = Toast.Make(text, duration, fontSize);
        await toast.Show(cancellationTokenSource.Token);
    }
}

