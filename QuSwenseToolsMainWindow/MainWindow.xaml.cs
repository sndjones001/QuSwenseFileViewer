using QuSwenseToolsMainWindow.ViewModel;
using System.Windows;

namespace QuSwenseToolsMainWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private JsonViewerControl _jsonViewerControl;
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new WindowThemeViewModel(this);
        }

        private void jsonViewerBtn_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
