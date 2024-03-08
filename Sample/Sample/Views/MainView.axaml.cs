using Avalonia.Controls;
using Sample.ViewModels;

namespace Sample.Views
{
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}