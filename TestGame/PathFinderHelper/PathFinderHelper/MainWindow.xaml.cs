using Microsoft.Win32;
using System;
using System.Collections.Generic;
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

namespace PathFinderHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SubWindow subWindow;       
        private OpenFileDialog dlg;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            this.dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".png";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                FileNameTextBox.Text = filename;
            }
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void FileNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Show_Click(object sender, RoutedEventArgs e)
        {
            int subWinHeight = 0;
            int subWinWidth = 0;
            int subWinDivider = 0;

            try
            {
                subWinHeight = int.Parse(this.picHeight.Text);
                subWinWidth = int.Parse(this.picWidth.Text);
                subWinDivider = int.Parse(this.picDivider.Text);
            }
            catch (Exception)
            {
                throw;
            }            

            this.subWindow = new SubWindow(subWinHeight, subWinWidth, subWinDivider, dlg);
            subWindow.Show();

            this.Close();
        }

        private void picWidth_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
    }
}
