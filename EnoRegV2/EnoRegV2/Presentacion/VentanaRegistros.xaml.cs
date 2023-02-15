using EnoregV2;
using EnoregV2.Dominio;
using EnoReV2;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
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
namespace VentanaRegistros
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    /// 
    public partial class VentanaRegistro : Window
    {
        public ProductoDAO productoDAO;
        public VentanaRegistro()
        {
            InitializeComponent();
            btnRegistro.IsEnabled = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            productoDAO = new ProductoDAO();
            CargarDataGrid();
            dtgprincipal.UpdateLayout();
            recorrerjlist();
        }

        private void recorrerjlist()
        {
            for (int i = 0; i < dtgprincipal.Items.Count; i++)
            {
                DataGridRow fila = (DataGridRow)dtgprincipal.ItemContainerGenerator.ContainerFromIndex(i);
                if (fila != null)
                {

                    DataGridCell celda1 = dtgprincipal.Columns[1].GetCellContent(fila).Parent as DataGridCell;
                    TextBlock textBlock = celda1.Content as TextBlock;

                    DataGridCell celda6 = dtgprincipal.Columns[6].GetCellContent(fila).Parent as DataGridCell;
                    TextBlock textBlock6 = celda6.Content as TextBlock;

                    DataGridCell celda7 = dtgprincipal.Columns[7].GetCellContent(fila).Parent as DataGridCell;
                    TextBlock textBlock7 = celda7.Content as TextBlock;

                    string nombre = textBlock.Text;
                    MessageBox.Show(nombre);

                }
                /*
                while (dr.Read())
                {
                    unidad = dr.GetString(0);
                }
                if (!textBlock6.Text.Equals("-"))
                {
                    textBlock6.Text += " " + unidad;
                    //dtgprincipal.Rows[i].DefaultCellStyle.BackColor = Color.FromRgb(218, 255, 202);
                }
                if (!textBlock7.Text.Equals("-"))
                {
                    textBlock7.Text += " " + unidad;
                    //dtgprincipal.Rows[i].DefaultCellStyle.BackColor = Color.FromRgb(255, 192, 192);
                }
                //dtgprincipal.Rows[i].Cells[8].Value += " " + unidad;*/
            }
        }

        private void CargarDataGrid()
        {
            // cargar datos 
            DataTable dt = new DataTable();
            dt.Load(productoDAO.CargarTodo());
            dtgprincipal.ItemsSource = dt.DefaultView;
            productoDAO.cerrarConexion();


            // añadir unidad a los valores y colores
            /*
            string nombre;
            string unidad = "";
            MySqlDataReader dr;
            DataGridRow fila = (DataGridRow)dtgprincipal.ItemContainerGenerator.ContainerFromIndex(1);
            if (fila == null)
            {
                dtgprincipal.UpdateLayout();
                dtgprincipal.ScrollIntoView(dtgprincipal.Items[1]);
                fila = (DataGridRow)dtgprincipal.ItemContainerGenerator.ContainerFromIndex(1); 
            }
            DataGridCell celda1 = dtgprincipal.Columns[1].GetCellContent(fila).Parent as DataGridCell;
            TextBlock textBlock = celda1.Content as TextBlock;
            MessageBox.Show(textBlock.Text);

            */

               
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
            VentanaAddProducto ventanaAddProducto = new VentanaAddProducto();
            ventanaAddProducto.Show();
        }

        private void btnExportarInforme_Click(object sender, RoutedEventArgs e)
        {
            recorrerjlist();
        }

        private void GridDtgdRegistro_Loaded(object sender, RoutedEventArgs e)
        {
            CargarDataGrid();
            
        }
    }
}
