using EnoregV2;
using EnoregV2.Dominio;
using EnoReV2;
using MySql.Data.MySqlClient;

using MySql.Data.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Windows.Media;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Windows.Controls.Image;
using Color = System.Windows.Media.Color;
using System.Collections.Specialized;
using System.ComponentModel;
using EnoregV2.Presentacion;

namespace VentanaRegistros
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    /// 
    public partial class VentanaRegistro : Window
    {
        //declaracion de variables
        ProductoDAO productoDAO = null;
        bool isSorting;

        /// <summary>
        /// Constructor de VentanaRegistro <see cref="VentanaRegistro"/> class.
        /// </summary>
        public VentanaRegistro()
        {
            InitializeComponent();
            btnRegistro.IsEnabled = false;
            productoDAO = new ProductoDAO();
        }

        /// <summary>
        /// Evento Loaded de la ventana de regsitros, donde cargamos y actualizamos el data grid principal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"> <see cref="RoutedEventArgs"/></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargarDataGrid();
            dtgprincipal.UpdateLayout();
            recorrerjlist();

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

        /// <summary>
        /// Metodo Recorrerjlists.
        /// </summary>
        public void recorrerjlist()
        {
            MySqlDataReader dr;
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

                    DataGridCell celda8 = dtgprincipal.Columns[8].GetCellContent(fila).Parent as DataGridCell;
                    TextBlock textBlock8 = celda8.Content as TextBlock;

                    DataGridCell celda9 = dtgprincipal.Columns[9].GetCellContent(fila).Parent as DataGridCell;
                    TextBlock textBlock9 = celda9.Content as TextBlock;

                    string nombre = textBlock.Text;
                    string unidad = "";
                    dr = productoDAO.ObtenerUnidad(nombre);
                     
                    while (dr.Read())
                    {
                        unidad = dr.GetString(0);
                                            }
                    if (!textBlock6.Text.Equals("-"))
                    {
                        textBlock6.Text += " " + unidad;
                        fila.Background = new SolidColorBrush(Color.FromRgb(218, 255, 202));

                    }
                    if (!textBlock7.Text.Equals("-"))
                    {
                        textBlock7.Text += " " + unidad;
                        fila.Background = new SolidColorBrush(Color.FromRgb(255, 192, 192));
                    }
                    textBlock8.Text += " " + unidad;
                    textBlock9.Text += " " + unidad;
                }
            }
        }

        /// <summary>
        /// Metodo CargarDataGrid, donde cargamos los productos
        /// en el data grid
        /// </summary>
        public void CargarDataGrid()
        {
            // cargar datos 
            DataTable dt = new DataTable();
            dt.Load(productoDAO.CargarTodo());
           // dtgprincipal.ItemsSource = null;
            dtgprincipal.ItemsSource = dt.DefaultView;
            productoDAO.cerrarConexion();
               
        }

        /// <summary>
        /// Evento onClick del boton de productos, que ocultara los botones y 
        /// data grid de los registros y mostraras los de productos. Tambien 
        /// deshabilita el boton de productos y habilita el de registros
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"> <see cref="RoutedEventArgs"/></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            gridRegistroBotones.Visibility = Visibility.Hidden;
            GridDtgdRegistro.Visibility = Visibility.Hidden;

            gridProductoBotones.Visibility = Visibility.Visible;
            GridDtgdProducto.Visibility = Visibility.Visible;

            btnRegistro.IsEnabled= true;
            btnProductos.IsEnabled = false;

            CargaDataGrid();
        }

        /// <summary>
        /// Evento onClick del boton de registros, que ocultara los botones y 
        /// data grid de los productos y mostraras los de registros. Tambien 
        /// deshabilita el boton de registros y habilita el de productos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"> <see cref="RoutedEventArgs"/></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            gridRegistroBotones.Visibility = Visibility.Visible;
            GridDtgdRegistro.Visibility= Visibility.Visible;

            gridProductoBotones.Visibility = Visibility.Hidden;
            GridDtgdProducto.Visibility = Visibility.Hidden;

            btnRegistro.IsEnabled = false;
            btnProductos.IsEnabled= true;
        }

        /// <summary>
        /// Evento del GridSplitter, que nos permitira moverlo 
        /// el GridSplitter dentro de los limites marcados 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"> <see cref="MouseEventArgs"/></param>
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

        /// <summary>
        /// Evento onClick del boton de Filtros, el cual abrira
        /// la ventana de filtros
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"> <see cref="RoutedEventArgs"/></param>
        private void btnFiltros_Click(object sender, RoutedEventArgs e)
        {
            Filtros v = new Filtros(this, productoDAO);
            v.Show();
        }

        /// <summary>
        /// Evento onClick del boton de Entradas, el cual abrira
        /// la ventana de entradas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"> <see cref="RoutedEventArgs"/></param>
        private void btnEntrada_Click(object sender, RoutedEventArgs e)
        {
            AnnadirEntrada en = new AnnadirEntrada(this);
            en.Show();
        }

        /// <summary>
        /// Evento onClick del boton de Salidas, el cual abrira
        /// la ventana de salidas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"> <see cref="RoutedEventArgs"/></param>
        private void btnSalida_Click(object sender, RoutedEventArgs e)
        {
            AnnadirSalida sa = new AnnadirSalida(this);
             sa.Show();

        }

        /// <summary>
        /// Evento onClick del boton de Añadir Productos, el cual abrira
        /// la ventana de añadir productos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"> <see cref="RoutedEventArgs"/></param>
        private void btnannadirProducto_Click(object sender, RoutedEventArgs e)
        {
            VentanaAddProducto ventanaAddProducto = new VentanaAddProducto(this);
            ventanaAddProducto.Show();
        }

        /// <summary>
        /// Evento Loaded del data grid de productos, donde 
        /// cargaremos los datos de los productos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"> <see cref="RoutedEventArgs"/></param>
        private void dtgProductos_Loaded(object sender, RoutedEventArgs e)
        {
            CargaDataGrid();
        }

        /// <summary>
        /// Metodo CargaDataGrid, donde cargamos los productos disponibles
        /// en la base de datos en un data table
        /// </summary>
        public void CargaDataGrid() 
        {
            DataTable dt = new DataTable();
            dt.Load(productoDAO.CargarListaProductos());
            dtgProductos.ItemsSource = dt.DefaultView;
            productoDAO.cerrarConexion();
        }

        /// <summary>
        /// Evento de seleccion de elemento en el data grid de productos, que
        /// cuando hacemos clic en unos de los productos, aparece su imagen asociada 
        /// en la ventana
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"> <see cref="SelectionChangedEventArgs"/></param>
        private void dtgProductos_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            //declaracion de variables
            string text="";
            string ids = "";
            int row = dtgProductos.SelectedIndex;            
            DataGridRow columna = (DataGridRow)dtgProductos.ItemContainerGenerator.ContainerFromIndex(row);                
            text = ((TextBlock)dtgProductos.Columns[1].GetCellContent(columna)).Text;
            ids= ((TextBlock)dtgProductos.Columns[0].GetCellContent(columna)).Text;
            int id = int.Parse(ids);
            Producto pro = new Producto(id,text);     
            
            //si la fila seleccionada esta vacia, lo indicamos
            if (pro == null)
            {
                MessageBox.Show("La fila seleccionada no es un producto válido.");
                return;
            }

            //aqui cargamos la imagenes de los productos
            MySqlDataReader data = productoDAO.CargarImagen(pro);            
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
            //se cargan los lotes y se muestran en el data table
            try
            {
                DataTable dt = new DataTable();
                dt.Load(productoDAO.CargarLotes(pro));
                dtgLotes.ItemsSource = dt.DefaultView;
                productoDAO.cerrarConexion();
            }
            catch (MySqlConversionException es)
            {
                
            }
    }
        /// <summary>
        /// Evento onClick del boton de exportar informe, el cual llama al 
        /// metodo recorrerjlist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"> <see cref="RoutedEventArgs"/></param>
        private void btnExportarInforme_Click(object sender, RoutedEventArgs e)
        {
            InformeRegistros registros= new InformeRegistros(productoDAO);
            registros.Show();
        }

        private void dtgprincipal_Sorting(object sender, DataGridSortingEventArgs e)
        {
            var dataGrid = sender as DataGrid;

            if (dataGrid != null)
            {
               /* dataGrid.Items.SortDescriptions.Clear();
                if (!string.IsNullOrEmpty(e.Column.SortMemberPath) && e.Column.SortDirection != null)
                {
                    dataGrid.Items.SortDescriptions.Add(new SortDescription(e.Column.SortMemberPath, e.Column.SortDirection.Value));
                }
                dataGrid.Items.Refresh();*/

                isSorting = true;
            }
        }
            private void dtgprincipal_LayoutUpdated(object sender, EventArgs e)
            {
                if (isSorting)
                {
                    isSorting = false;

                recorrerjlist();
                }
            }

        private void btnInformeProducto_Click(object sender, RoutedEventArgs e)
        {
            InformeProductos informe = new InformeProductos();
            informe.Show();
        }
    }
}