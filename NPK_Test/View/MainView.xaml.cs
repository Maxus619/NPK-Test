using System.Windows;

namespace NPK_Test.View
{
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = new ViewModel.MainViewModel();
        }
    }
}
