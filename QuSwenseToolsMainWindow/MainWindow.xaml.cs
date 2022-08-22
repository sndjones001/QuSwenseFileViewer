using QuSwenseToolsMainWindow.ViewModel;
using System.Windows;

namespace QuSwenseToolsMainWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new WindowThemeViewModel(this);
        }
    }
}
