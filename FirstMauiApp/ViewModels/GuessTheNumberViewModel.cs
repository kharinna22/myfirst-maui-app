using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FirstMauiApp.Views;
using Microsoft.Maui.ApplicationModel;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FirstMauiApp.ViewModels;
internal class GuessTheNumberViewModel : ObservableObject
{
    public int MinGuess => 0;
    public int MaxGuess => 30;
    public int MaxChances => 5;
    public int RemainingChances { get; set; }
    
    private string _gameInfoLabel;
    public string GameInfoLabel
    {
        get
        {
            return _gameInfoLabel;
        }
        set
        {
            _gameInfoLabel = $"Adivina el número entre {MinGuess} y {MaxGuess}\nIntentos {RemainingChances}/{MaxChances}";
        }
    }

    public string StateInfoLabel { get; set; } = "";
    public string AttemptsLabel { get; set; } = "";
    public string ErrorLabel { get; set; }
    public bool GameStatus { get; set; }
    public bool IsGameCompleted { get; set; }
    public bool IsErrorLabelVisible { get; set; }
    public int NumberToBeGuessed { get; set; }
    public int? NumberInput { get;set; }

    private List<string> Temperatures = new ()
        {
            "¡¡ARDIENDO EN LLAMAS!!",
            "TIBIO",
            "NI FRIO NI CALIENTE",
            "FRIO",
            "CONGELADISIMOOO"
        };
    public ICommand ValidateNumberCommand { get; private set; }

    public GuessTheNumberViewModel()
    {
        NumberToBeGuessed = new Random().Next(MinGuess, MaxGuess);
        RemainingChances = MaxChances;
        GameInfoLabel = "";
        GameStatus = true;
        IsGameCompleted = false;
        IsErrorLabelVisible = false;
        ErrorLabel = $"Por favor, ingrese un número entre {MinGuess} y {MaxGuess}";
        ValidateNumberCommand = new RelayCommand(ValidateNumber);
    }

    public void ValidateNumber()
    {
        if (NumberInput == null)
            return;

        IsErrorLabelVisible = false;
        OnPropertyChanged(nameof(IsErrorLabelVisible));
        if (NumberInput > MaxGuess || NumberInput < MinGuess) // Por favor, ingrese número entre 0 y 30 xd 
        {
            IsErrorLabelVisible = true;
            OnPropertyChanged(nameof(IsErrorLabelVisible));
            return;
        }
        if (NumberInput == NumberToBeGuessed)
        {
            StateInfoLabel = $"<strong>¡Felicidades!</strong> Has adivinado el número con {RemainingChances} " +
                (RemainingChances == 1 ? "intento restante." : "intentos restantes.");
            GameStatus = false;
            NumberInput = null;
            IsGameCompleted = true;
            RefreshProperties();
            return;
        }

        int maxDistance = MaxGuess - MinGuess;
        int distance = Math.Abs(NumberToBeGuessed - (int)NumberInput);

        int porcentaje = ((distance * 5) / maxDistance);
        porcentaje = porcentaje > 4 ? 4 : porcentaje;

        StateInfoLabel = $"Número equivocado.<br><strong>{Temperatures[porcentaje]}</strong>";

        AttemptsLabel = AttemptsLabel == "" ? $"Intentos:\n{NumberInput}" : AttemptsLabel + $"\n{NumberInput}";

        RemainingChances -= 1;

        NumberInput = null;
        GameInfoLabel = "";
        
        if (RemainingChances <= 0)
        {
            StateInfoLabel = $"Número equivocado.<br>Se han acabado los intentos. El número era {NumberToBeGuessed}..";
            GameStatus = false;
            IsGameCompleted = true;
        }

        RefreshProperties();
    }

    private void RefreshProperties()
    {

        OnPropertyChanged(nameof(RemainingChances));
        OnPropertyChanged(nameof(GameInfoLabel));
        OnPropertyChanged(nameof(GameStatus));
        OnPropertyChanged(nameof(NumberInput));
        OnPropertyChanged(nameof(StateInfoLabel));
        OnPropertyChanged(nameof(AttemptsLabel));
        OnPropertyChanged(nameof(IsGameCompleted));
    }

}

