using System;
using System.Collections.Generic;
using System.Drawing;
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

namespace EnoregV2
{
    /// <summary>
    /// Lógica de interacción para VentanaAddProducto.xaml
    /// </summary>
    public partial class VentanaAddProducto : Window
    {
        private Font Font;

        public VentanaAddProducto()
        {
            InitializeComponent();
        }

        private void btnimagen_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();

            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
                // Obtener nombre de archivo seleccionado
                string fileName = openFileDialog.FileName;

                // Hacer algo con el archivo seleccionado
                // ...
            }
        }

        //Properties
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //tipo de fuente
            System.Drawing.Font tipoLetra = EnoregV2.Properties.Settings.Default.Font;
            System.Windows.Media.FontFamily tipoFuente = new System.Windows.Media.FontFamily(tipoLetra.Name);

            //fondo
            System.Drawing.Color fondo = EnoregV2.Properties.Settings.Default.ColorFondo;
            System.Windows.Media.Brush brushF = new SolidColorBrush
                (System.Windows.Media.Color.FromArgb
                (fondo.A, fondo.R, fondo.G, fondo.B));

            this.FontFamily = tipoFuente;
            this.Background = brushF;
        }
    }
}
