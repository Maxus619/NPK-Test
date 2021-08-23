using System.Windows.Controls;

namespace NPK_Test.View
{
    public partial class PatientsListView : Page
    {
        public PatientsListView()
        {
            InitializeComponent();
            DataContext = new ViewModel.PatientsListViewModel();
        }
    }
}
