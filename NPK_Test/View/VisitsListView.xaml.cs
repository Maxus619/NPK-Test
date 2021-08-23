using System.Windows.Controls;

namespace NPK_Test.View
{
    public partial class VisitsListView : Page
    {
        public VisitsListView()
        {
            InitializeComponent();
            DataContext = new ViewModel.VisitsListViewModel();
        }
    }
}
