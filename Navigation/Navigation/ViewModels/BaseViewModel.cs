using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace Navigation.ViewModels
{
    public class BaseViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand OpenMenuCommand { get; private set; }

        protected BaseViewModel()
        {
            OpenMenuCommand = new Command(OpenMenu);
        }

        protected void RaisePropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        protected void OpenMenu()
        {
            var mdp = (MasterDetailPage)Application.Current.MainPage;
            mdp.IsPresented = true;
        }
    }
}
