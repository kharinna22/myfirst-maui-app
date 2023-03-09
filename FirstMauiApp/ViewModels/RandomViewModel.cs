using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FirstMauiApp.Models;
using FirstMauiApp.Views;
using System.Windows.Input;

namespace FirstMauiApp.ViewModels;
internal class RandomViewModel : ObservableObject
{
    public int OneToTwo { get; set; }
    public int OneToThree { get; set; }
    public int OneToFour { get; set; }
    public int OneToFive { get; set; }
    public int OneToSix { get; set; }
    public int OneToEight { get; set; }
    public int OneToNine { get; set; }
    public int OneToThreeHundred { get; set; }
    public ICommand ActualizarCommand { get; set; }

    public RandomViewModel()
    {
        Randomizar();
        ActualizarCommand = new RelayCommand(Actualizar);
    }

    private async void Actualizar()
    {
        while (true) { 
            await Refrescar(4000);
            Randomizar();
        }
    }

    async public static Task Refrescar(int intervalo = 500)
    {
        await Task.Delay(intervalo);
    }

    private void Randomizar()
    {
        OneToTwo = new Random().Next(1, 3);
        OneToThree = new Random().Next(1, 4);
        OneToFour = new Random().Next(1, 5);
        OneToFive = new Random().Next(1, 6);
        OneToSix = new Random().Next(1, 7);
        OneToEight = new Random().Next(1, 9);
        OneToNine = new Random().Next(1, 10);
        OneToThreeHundred = new Random().Next(1, 301);

        OnPropertyChanged(nameof(OneToTwo));
        OnPropertyChanged(nameof(OneToThree));
        OnPropertyChanged(nameof(OneToFour));
        OnPropertyChanged(nameof(OneToFive));
        OnPropertyChanged(nameof(OneToSix));
        OnPropertyChanged(nameof(OneToEight));
        OnPropertyChanged(nameof(OneToNine));
        OnPropertyChanged(nameof(OneToThreeHundred));
    }

}

