using EnoregV2.Dominio;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VentanaRegistros;
using static System.Net.Mime.MediaTypeNames;

namespace EnoReV2
{
    /// <summary>
    /// Lógica de interacción para AnnadirSalida.xaml
    /// </summary>
    public partial class AnnadirSalida : Window
    {
        //declaracion de variables
        Double cantidadRestante = 0;
        ProductoDAO productoDao = new ProductoDAO();
        LoteDao loteDao = new LoteDao();
        Lote l = null;
        VentanaRegistro v = null;
        string fecha;


        /// <summary>
        /// Constructor de la clase AnnadirSalida <see cref="AnnadirSalida"/> class.
        /// </summary>
        /// <param name="vr">The vr.</param>

        public AnnadirSalida(VentanaRegistro vr)
        {
            InitializeComponent();
            CargarComboProductos();
            v = vr;
            dtpFechaSalida.SelectedDate = DateTime.Now;
        }

        /// <summary>
        /// Metodo CargaComboProductos, donde rellenamos el combo con el nombre
        /// de los productos disponibles en la base de datos
        ///</summary>
        private void CargarComboProductos()
        {

            MySqlDataReader dr = productoDao.Cargarproductos();

            while (dr.Read())
            {
                cmbProductoSalida.Items.Add(new
                {
                    id = dr["id_producto"].ToString(),
                    nombre = dr["nombre"].ToString()
                });
            }
            cmbProductoSalida.DisplayMemberPath = "nombre";
            cmbProductoSalida.SelectedValuePath = "id";
        }

        /// <summary>
        /// Metodo CargaComboLotes, donde rellenamos el combo con el nombre
        /// de los lotes disponibles en la base de datos
        ///</summary>
        private void CargarComboLotes() {
            cmbLoteSalida.Items.Clear();
            String idProductoSelecionado = cmbProductoSalida.SelectedValue.ToString();

            MySqlDataReader dr = loteDao.CargarLotes(idProductoSelecionado);

            while (dr.Read())
            {
                cmbLoteSalida.Items.Add(new
                {
                    id = dr["id_lote"].ToString(),
                    nombre = dr["lote"].ToString()
                });
            }
            cmbLoteSalida.DisplayMemberPath = "nombre";
            cmbLoteSalida.SelectedValuePath = "id";
        }

        /// <summary>
        /// Evento de seleccion del combo de productos, donde dependiendo del producto elegido
        /// se mostraran sus lotes disponibles en el combo de lotes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"> <see cref="SelectionChangedEventArgs"/></param>
        private void cmbProductoSalida_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CargarComboLotes();
            lblCantidadRestante.Content = "Cantidad Disponible: " + 0;
        }

        /// <summary>
        /// Evento de seleccion del combo de lotes, donde dependiendo de lote 
        /// seleccionado, nos mostrara la cantidad disponible del lote en un label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"> <see cref="SelectionChangedEventArgs"/></param>
        private void cmbLoteSalida_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try {
                string codigoLote = "";
                codigoLote =  (sender as ComboBox).SelectedItem.ToString();

                string patron = @"nombre\s*=\s*(\w+)";
                Match match = Regex.Match(codigoLote, patron);
                if (match.Success)
                {
                    string cod = match.Groups[1].Value.Trim();
                    String value = cmbProductoSalida.SelectedValue.ToString();

                    l = new Lote(cod, Int32.Parse(value));
                    cantidadRestante = loteDao.ObtenerStockLote(l);
                    if (cantidadRestante == -1)
                    {
                        cantidadRestante = 0;
                    }
                    MySqlDataReader dr = null;
                    String unidad=null;
                    dr = productoDao.ObtenerUnidad(cmbProductoSalida.Text);
                    while (dr.Read())
                    {
                        unidad= dr.GetString(0);
                    }
                    lblCantidadRestante.Content = "Cantidad Disponible: " + cantidadRestante.ToString()+""+unidad;
                }
               
            }  catch(Exception ex)
            {
                
            }       
        }

        /// <summary>
        /// Evento onClick del boton Aceptar Salida, donde primero revisamos
        /// que los campos se hayan rellenado correctamente, y una vez hecha la
        /// revision, se insertaran los datos de la salida en la base de datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"> <see cref="RoutedEventArgs"/></param>
        private void btnAceptarSalida_Click(object sender, RoutedEventArgs e)
        {
            //declaracion de variables
            string mensaje = "Tienes que rellenar o seleccionar:";
            Boolean valor = false;
            double cantidaNumerico = 0;
            string cantidad = "";

            //revisamos que seleccione la fecha de salida
            if (dtpFechaSalida.SelectedDate == DateTime.Now.Date)
            {
                if (mensaje.Length > 34)
                {
                    mensaje += ",";
                }
                mensaje += " la fecha de salida";
                dtpFechaSalida.Focus();
                valor = true;
            }

            //revisamos que seleccione un elemento del combo de productos
            if (cmbProductoSalida.SelectedIndex.Equals(-1))
            {
                if (mensaje.Length > 34)
                {
                    mensaje += ",";
                }
                mensaje += " Producto";
                cmbProductoSalida.Focus();
                cmbProductoSalida.Background = Brushes.LightCoral;
                valor = true;
            }
            else
            {
                cmbProductoSalida.Background = Brushes.White;
            }

            //revisamos que seleccione un elemento del combo de lotes
            if (cmbLoteSalida.SelectedIndex.Equals(-1))
            {
                if (mensaje.Length > 34)
                {
                    mensaje += ",";
                }
                mensaje += " Lote";
                cmbLoteSalida.Focus();
                cmbLoteSalida.Background = Brushes.LightCoral;
                valor = true;
            }
            else
            {
                cmbLoteSalida.Background = Brushes.White;
            }

            if (string.IsNullOrEmpty(txbCantidadSalida.Text) || !int.TryParse(txbCantidadSalida.Text, out int cant) || cant < 1)
            {
                if (chbLiquidar.IsChecked == false) {
                    if (mensaje.Length > 34)
                    {
                        mensaje += ",";
                    }
                    mensaje += " Cantidad";
                    txbCantidadSalida.Focus();
                    txbCantidadSalida.Background = Brushes.LightCoral;
                    valor = true;
                }      
            }
            else
            {
                cantidad = txbCantidadSalida.Text;
                cantidad = cantidad.Replace(",", ".");
                if (!double.TryParse(cantidad, out cantidaNumerico))
                {
                    mensaje += "Cantidad [ Recuerde introducir solo numeros ]";
                    valor = true;
                }
                else
                {
                    txbCantidadSalida.Background = Brushes.White;
                }
            }

            if (valor == false) {
                //si se marca el checkbox de liquidar, cambiamos el campo cantidad a 0
                if (chbLiquidar.IsChecked == true)
                {
                    txbCantidadSalida.Text = "0";
                }

                //si se introduce un numero mayor a la cantidad disponible, se informara al usuario
                if (Double.Parse(txbCantidadSalida.Text) > cantidadRestante && chbLiquidar.IsChecked == false) {
                    MessageBox.Show("Error, estas tratando de sacar mas stock del que hay disponible en el lote.\n(marca el boton de liquidar si quieres retirar todo el stock restante del lote)", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Information);
                    valor = true;
                }    
            }
            
            //si todos los campos son correctos, introducimos los datos en la base de datos
            if (valor == false)
            {
                mensaje = "Salida introducida correctamente";

                // recuperar cantidad lote
                Double stockLote = loteDao.ObtenerStockLote(l);

                // obtener stock del producto
                Double stockProducto = productoDao.ObtenerStockProducto(cmbProductoSalida.Text);

                // obtener Fecha
                DateTime? selectedDate = dtpFechaSalida.SelectedDate;
                fecha = selectedDate.Value.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);


                Salida s = new Salida(fecha,l,Double.Parse(txbCantidadSalida.Text), txbObservacionesSalida.Text,stockLote,stockProducto, txbDestino.Text);

                Boolean liquidar = (bool)chbLiquidar.IsChecked;
                loteDao.InsertarSalida(s,liquidar);

                v.CargarDataGrid();
               // v.CargaDataGrid();

                v.dtgprincipal.UpdateLayout();
             //   v.dtgProductos.UpdateLayout();

                v.recorrerjlist();

                MessageBox.Show(mensaje + ".", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            if (mensaje.Length > 34)
            {
                MessageBox.Show(mensaje + ".", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Information);
            }            
        }

        /// <summary>
        /// Evento onClick del boton Cancelar, el cual cerrara la ventana de Salidas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelarSalida_Click(object sender, RoutedEventArgs e)
        {
           this.Close();
        }

        /// <summary>
        /// Evento de marcar el checkbox de Liquidar, donde 
        /// deshabilitamos el texbox de cantidad para que no pueda
        /// introducir datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"> <see cref="RoutedEventArgs"/></param>
        private void chbLiquidar_Checked(object sender, RoutedEventArgs e)
        {
            txbCantidadSalida.IsEnabled= false;
            lblCantidadRestante.Content = "";
        }
    }
}
