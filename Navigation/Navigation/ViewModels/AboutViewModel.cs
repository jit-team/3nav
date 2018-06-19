using System.Windows.Input;
using Xamarin.Forms;

namespace Navigation.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        private string _firstParagraph;
        public string FirstParagraph
        {
            get => _firstParagraph;
            set { _firstParagraph = value; RaisePropertyChanged(); }
        }

        private string _secondParagraph;
        public string SecondParagraph
        {
            get => _secondParagraph;
            set { _secondParagraph = value; RaisePropertyChanged(); }
        }

        public AboutViewModel()
        {
            FirstParagraph = "    Mogłoby się wydawać, że 3Nav, jak większość aplikacji, została wykonana przez zmęczonych" +
                " życiem korpo-programistów. Nic bardziej mylnego! Aplikacja, dzięki której można poruszać się po najlepszym" +
                " liceum w województwie pomorskim powstała dzięki współpracy aktywnych uczniów tej szkoły z programistami JitSolutions!"; 
            SecondParagraph = "Uczniów III Liceum Ogólnokształcącego cechuje to, że chwytają się różnych możliwości," +
                " by się rozwinąć. Tak naprawdę w szkole uczniowie mogą realizować własne projekty - programistyczne," +
                " społeczne, a także artystyczne. W przypadku aplikacji 3Nav, inicjatywa wyszła ze strony uczniów," +
                " jednak nie możliwe byłoby osiągnięcie danego celu, gdyby nie warsztaty oraz ogólnodostępne wsparcie" +
                " ze strony firmy JitSolutions.";
        }
    }
}
