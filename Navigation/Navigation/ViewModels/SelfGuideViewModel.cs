using System.Collections.ObjectModel;
using System.Windows.Input;
using Navigation.Models;
using Xamarin.Forms;

namespace Navigation.ViewModels
{
    public class SelfGuideViewModel : BaseViewModel
    {
        public ICommand SkipButtonClickedCommand { get; private set; }
        private void SkipButtonClicked()
        {
            MessagingCenter.Send(this, Messages.ChangeCurrentListElement, 0);
        }

        private ObservableCollection<CarouselViewData> _myItemsSource;
        public ObservableCollection<CarouselViewData> MyItemsSource
        {
            get => _myItemsSource;
            set { _myItemsSource = value; RaisePropertyChanged(); }
        }

        public SelfGuideViewModel()
        {
            SkipButtonClickedCommand = new Command(SkipButtonClicked);
            MyItemsSource = new ObservableCollection<CarouselViewData>()
            {
                new CarouselViewData("tutorialbluetoothalertt.png", "Do prawidłowego działania aplikacji potrzebny jest włączony moduł Bluetooth. \nPo kliknięciu na 'Tak' włączy Bluetooth w urządzeniu. \n"
                                     + "Po kliknięciu na 'Nie', aplikacja uruchomi się, lecz wymagane będzie ręczne włączenie Bluetooth."),
                new CarouselViewData("tutorialnavigationalert.png", "Aplikacja przy uruchomieniu sprawdza Twoją lokalizację. Jeśli nie znajdujesz się w pobliżu szkoły pojawi się powyższe okno dialogowe. \n"
                                    + "Po kliknięciu 'Tak', uruchomi się domyślna aplikacja map, która zaprowadzi Cię do szkoły."
                                    + "\nPo kliknięciu 'Nie' aplikacja przejdzie dalej, a do szkoły będziesz musiał dostać się sam."),
                new CarouselViewData("tutorialmainmenu.jpeg", "Główna belka aplikacji. \n1. Za pomocą tej ikony wysuniesz Menu. \n"
                                    + "2. To wyszukiwarka sal. Wpisz nazwę sali, a rozwinie się lista sal o podobnej nazwie. Możesz kliknąć na wybraną salę, a zostanie ona zaznaczona na mapie. \n"
                                    + "3. Ikona służąca do chowania listy i usuwania zaznaczenia sali."),
                new CarouselViewData("listviewtutorial.png", "Lista służąca do wyboru sali. \nPo wybraniu szukanej sali, zostanie ona zaznaczona na mapie za pomocą niebieskiego koła. \n"),
                new CarouselViewData("tutorialmainview.jpeg", "Mapa z naniesionymi wskaźnikami. \nNiebieskie koło pokazuje położenie szukanej sali. \n"
                                    + "Zielone koło pokazuje przybliżoną pozycję użytkownika w budynku. Strzałka wewnątrz koła, pokazuję stronę, w którą zwrócony jest użytkownik. \n")
            };
        }
    }
}
