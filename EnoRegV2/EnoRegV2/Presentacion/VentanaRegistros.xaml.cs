using EnoregV2;
using EnoregV2.Dominio;
using EnoReV2;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
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
using static System.Net.Mime.MediaTypeNames;
using Image = System.Windows.Controls.Image;

namespace VentanaRegistros
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class VentanaRegistro : Window
    {
        ProductoDAO p = null;
        public VentanaRegistro()
        {
            InitializeComponent();
            btnRegistro.IsEnabled = false;
            p = new ProductoDAO();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            gridRegistroBotones.Visibility = Visibility.Hidden;
            GridDtgdRegistro.Visibility = Visibility.Hidden;

            gridProductoBotones.Visibility = Visibility.Visible;
            GridDtgdProducto.Visibility = Visibility.Visible;

            btnRegistro.IsEnabled= true;
            btnProductos.IsEnabled = false;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            gridRegistroBotones.Visibility = Visibility.Visible;
            GridDtgdRegistro.Visibility= Visibility.Visible;

            gridProductoBotones.Visibility = Visibility.Hidden;
            GridDtgdProducto.Visibility = Visibility.Hidden;

            btnRegistro.IsEnabled = false;
            btnProductos.IsEnabled= true;
        }
        private void GridSplitter_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            GridSplitter splitter = sender as GridSplitter;
            Grid grid = splitter.Parent as Grid;

            // Obtén la columna donde se encuentra el splitter
            int column = Grid.GetColumn(splitter);

            // Establece los límites mínimo y máximo para la anchura de la columna
            double minWidth = grid.ActualWidth * 0.21;
            double maxWidth = grid.ActualWidth / 2.5;

            // Obtén la anchura actual de la columna
            ColumnDefinition columnDef = grid.ColumnDefinitions[column];
            double columnWidth = columnDef.ActualWidth;

            // Calcula la anchura resultante después del movimiento
            double newWidth = columnWidth + e.GetPosition(grid).X;

            // Verifica si la anchura resultante está dentro de los límites
            if (newWidth < minWidth)
            {
                e.Handled = true;
                return;
            }
            if (newWidth > maxWidth)
            {
                e.Handled = true;
                return;
            }
        }

        private void CambiarFormatoBoton(object sender, SizeChangedEventArgs e)
        {
            if (btnEntrada.ActualWidth < ActualWidth * 0.08)
            {
                txbEntrada.Visibility = Visibility.Collapsed;
                imgEntrada.SetValue(Grid.ColumnSpanProperty, 2);

                txbSalida.Visibility = Visibility.Collapsed;
                imgSalida.SetValue(Grid.ColumnSpanProperty, 2);

                txbFiltros.Visibility = Visibility.Collapsed;
                imgFiltros.SetValue(Grid.ColumnSpanProperty, 2);

                txbInformeRegistro.Visibility = Visibility.Collapsed;
                imgInformeRegistro.SetValue(Grid.ColumnSpanProperty, 2);

                txbInformeProducto.Visibility = Visibility.Collapsed;
                imgInformeProducto.SetValue(Grid.ColumnSpanProperty, 2);

                txbAnnadirProducto.Visibility = Visibility.Collapsed;
                imgAnnadirProducto.SetValue(Grid.ColumnSpanProperty, 2);
            }
            else {
                txbEntrada.Visibility = Visibility.Visible;
                imgEntrada.SetValue(Grid.ColumnSpanProperty, 1);
                imgEntrada.Margin = new Thickness(0, 0, 0, 0);

                txbSalida.Visibility = Visibility.Visible;
                imgSalida.SetValue(Grid.ColumnSpanProperty, 1);
                imgSalida.Margin = new Thickness(0, 0, 0, 0);

                txbFiltros.Visibility = Visibility.Visible;
                imgFiltros.SetValue(Grid.ColumnSpanProperty, 1);
                imgFiltros.Margin = new Thickness(0, 0, 0, 0);

                txbInformeRegistro.Visibility = Visibility.Visible;
                imgInformeRegistro.SetValue(Grid.ColumnSpanProperty, 1);
                imgInformeRegistro.Margin = new Thickness(0, 0, 0, 0);

                txbInformeProducto.Visibility = Visibility.Visible;
                imgInformeProducto.SetValue(Grid.ColumnSpanProperty, 1);
                imgInformeProducto.Margin = new Thickness(0, 0, 0, 0);

                txbAnnadirProducto.Visibility = Visibility.Visible;
                imgAnnadirProducto.SetValue(Grid.ColumnSpanProperty, 1);
                imgAnnadirProducto.Margin = new Thickness(0, 0, 0, 0);
            }
        }

        private void btnFiltros_Click(object sender, RoutedEventArgs e)
        {
            Filtros v = new Filtros();
            v.Show();
        }

        private void btnEntrada_Click(object sender, RoutedEventArgs e)
        {
            AnnadirEntrada en = new AnnadirEntrada();
            en.Show();
        }

        private void btnSalida_Click(object sender, RoutedEventArgs e)
        {
            AnnadirSalida sa = new AnnadirSalida();
            sa.Show();
        }

        private void btnannadirProducto_Click(object sender, RoutedEventArgs e)
        {
            VentanaAddProducto ventanaAddProducto = new VentanaAddProducto(this);
            ventanaAddProducto.Show();
        }

        private void dtgProductos_Loaded(object sender, RoutedEventArgs e)
        {
            CargaDataGrid();
        }
        public void CargaDataGrid() 
        {
            DataTable dt = new DataTable();
            dt.Load(p.CargarListaProductos());
            dtgProductos.ItemsSource = dt.DefaultView;
            p.cerrarConexion();
        }        
        private void dtgProductos_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            //CONTINUA; NO CARGA IMAGEN
            string text="";
            int row = dtgProductos.SelectedIndex;
            
            DataGridRow columna = (DataGridRow)dtgProductos.ItemContainerGenerator.ContainerFromIndex(row);                
            text = ((TextBlock)dtgProductos.Columns[1].GetCellContent(columna)).Text;
               

            //MessageBox.Show(nombre + " " + unidad, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            Producto pro = new Producto(text,null,0);
            
            if (pro == null)
            {
                MessageBox.Show("La fila seleccionada no es un producto válido.");
                return;
            }

            // Ahora 'selectedProducto' es un objeto de tipo 'Producto' que representa la fila seleccionada en el 'DataGrid'.
            // Puedes usarlo según tus necesidades.

            MySqlDataReader data = p.CargarImagen(pro);
            
            if (data.Read())
            {
                try
                {
                    byte[] imageBytes = (byte[])data["imagen"];
                    BitmapImage bitmapImage = new BitmapImage();
                    using (MemoryStream stream = new MemoryStream(imageBytes))
                    {
                        stream.Position = 0;
                        bitmapImage.BeginInit();
                        bitmapImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapImage.UriSource = null;
                        bitmapImage.StreamSource = stream;
                        bitmapImage.EndInit();
                    }
                    Image image = new Image();
                    imgProductos.Source = bitmapImage;
                }
                catch (System.InvalidCastException es) 
                {
                    imgProductos.Source = null;
                }
             
            }
        }
    }
}