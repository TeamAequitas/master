using Microsoft.Win32;
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
using System.Windows.Shapes;

namespace PathFinderHelper
{
    /// <summary>
    /// Interaction logic for SubWindow.xaml
    /// </summary>
    public partial class SubWindow : Window
    {
        private static readonly Color unClickedRec = Color.FromArgb(70, 250, 180, 255);
        private static readonly Color clickedRec = Color.FromArgb(0, 250, 180, 255);
        private static readonly Color recBorder = Color.FromArgb(150, 255, 255, 255);

        private const int MaxWinWidth = 1500;
        private const int MaxWinHeight = 1000;
        private const int PicMargeHorizontal = 20;
        private const int PicMargeVertical = 40;
        private const string GameName = "AequitasGame";
        private const string PathToOutputFile = "Resources/Images/MainMap/Text";
        private const string OutpuFileName = "ImgMatrixInfo";
        private const string FileExtension = "txt";

        private readonly int picHeight;
        private readonly int picWidth;
        private readonly int picDivider;
        private readonly OpenFileDialog dlg;

        private int dispStartPointX = 0;
        private int dispStartPointY = 0;

        private bool[,] matrix;

        public SubWindow()
        {
            InitializeComponent();
        }

        public SubWindow(int picHeight, int picWidth, int picDivider, OpenFileDialog dlg)
        {
            this.picHeight = picHeight;
            this.picWidth = picWidth;
            this.picDivider = picDivider;
            this.dlg = dlg;

            InitializeComponent();

            DetermineWinDimensions();
            InitializeMatrix();                       
            InitializeImage();
        }

        private void InitializeImage()
        {
            scroller.Margin = new Thickness(PicMargeVertical, PicMargeHorizontal, PicMargeVertical, PicMargeHorizontal);

            Test.Margin = new Thickness(this.dispStartPointX, this.dispStartPointY, this.dispStartPointX, this.dispStartPointY);
            Test.Width = this.picWidth;
            Test.Height = this.picHeight;
            Test.Background = new ImageBrush(new BitmapImage(new Uri(this.dlg.FileName, UriKind.Relative)));
            
            UniGrid.Rows = matrix.GetLength(0);
            UniGrid.Columns = matrix.GetLength(1);
            //var transform = new RotateTransform(-25.3);//rotated grid
            //transform.CenterX = 1555;
            //transform.CenterY = 330;
            //UniGrid.RenderTransform = transform;


            for (int row = 0; row < UniGrid.Rows; row++)
            {
                for (int col = 0; col < UniGrid.Columns; col++)
                {
                    var rectangle = new Rectangle
                    {
                        Fill = new SolidColorBrush(unClickedRec),
                        Stroke = new SolidColorBrush(recBorder),
                        StrokeThickness = 1,
                        Name = "x" + row + "x" + col,
                        Width = this.picDivider,
                        Height = this.picDivider
                    };
                    rectangle.MouseLeftButtonDown += Rectangle_MouseLeftButtonDown;

                    UniGrid.Children.Add(rectangle);
                }
            }
        }

        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var rect = sender as Rectangle;
            var data = rect.Name.Split(new[] { 'x' }, StringSplitOptions.RemoveEmptyEntries);

            int row = int.Parse(data[0]);
            int col = int.Parse(data[1]);

            if (matrix[row, col])
            {
                rect.Fill = new SolidColorBrush(unClickedRec);
                matrix[row, col] = false;
            }
            else
            {
                rect.Fill = new SolidColorBrush(clickedRec);
                matrix[row, col] = true;
            }
        }

        private void DetermineWinDimensions()
        {
            int winHeight = this.picHeight + 2 * PicMargeVertical;
            int winWidth = this.picWidth + 2 * PicMargeHorizontal;

            if (MaxWinHeight < winHeight)
                winHeight = MaxWinHeight;

            if (MaxWinWidth < winWidth)
                winWidth = MaxWinWidth;

            this.Height = winHeight;
            this.Width = winWidth;
        }

        private void InitializeMatrix()
        {
            int rows = this.picHeight / this.picDivider;
            int columns = this.picWidth / this.picDivider;

            this.dispStartPointX = (this.picWidth - columns * this.picDivider) / 2;
            this.dispStartPointY = (this.picHeight - rows * this.picDivider) / 2;

            this.matrix = new bool[rows, columns];
        }

        private void GenerateMatrix_Click(object sender, RoutedEventArgs e)
        {
            string filePath = $"../../../../{GameName}/{PathToOutputFile}/{OutpuFileName}.{FileExtension}";
            string[] lines = {System.IO.Path.GetFileName(this.dlg.FileName), this.picWidth + "x" + this.picHeight, $"{this.matrix.GetLength(0)}x{this.matrix.GetLongLength(0)}" };
            // WriteAllLines creates a file, writes a collection of strings to the file,
            // and then closes the file.  You do NOT need to call Flush() or Close().
            System.IO.File.WriteAllLines(filePath, lines);

            using (System.IO.StreamWriter file = File.AppendText(filePath))
            {
                for (int row = 0; row < matrix.GetLength(0); row++)
                {
                    string line = null;
                    for (int col = 0; col < matrix.GetLength(1); col++)
                    {
                        line += matrix[row, col] == true ? "O" : "X";
                    }

                    file.WriteLine(line);
                }
            }
        }
    }
}
