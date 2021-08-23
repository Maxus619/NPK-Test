using System.Windows.Controls;

namespace NPK_Test.View
{
    public partial class AddPatientView : Page
    {
        public AddPatientView()
        {
            InitializeComponent();
            DataContext = new ViewModel.AddPatientViewModel();
        }
    }
}
