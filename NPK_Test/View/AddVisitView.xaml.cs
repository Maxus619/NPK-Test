using System.Windows.Controls;

namespace NPK_Test.View
{
    public partial class AddVisitView : Page
    {
        public AddVisitView()
        {
            InitializeComponent();
            DataContext = new ViewModel.AddVisitViewModel();
        }
    }
}
