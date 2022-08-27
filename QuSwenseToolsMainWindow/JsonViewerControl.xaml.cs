using Microsoft.Win32;
using QuSwenseToolsMainWindow.JsonModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuSwenseToolsMainWindow
{
    /// <summary>
    /// Interaction logic for JsonViewerControl.xaml
    /// </summary>
    public partial class JsonViewerControl : UserControl
    {
        public JsonViewerControl()
        {
            InitializeComponent();
        }

        private void BrowseFileBtn_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Json files (*.json)|*.json"
            };

            if(openFileDialog.ShowDialog() == true)
            {
                jsonFilePathTxtBox.Text = openFileDialog.FileName;

                var jsonReader = new JsonReader();
                jsonReader.Reader(File.ReadAllText(jsonFilePathTxtBox.Text));
            }
        }
    }
}
