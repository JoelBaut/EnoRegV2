using EnoregV2.Dominio;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
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
<<<<<<< HEAD
using Brushes = System.Windows.Media.Brushes;
=======
>>>>>>> 1ff12ff7f6e04265dbbdbdfb7305aae15af63291

namespace EnoReV2
{
    /// <summary>
    /// Lógica de interacción para AnnadirEntrada.xaml
    /// </summary>
    public partial class AnnadirEntrada : Window
    {
<<<<<<< HEAD
=======
        //declaracion de variables
>>>>>>> 1ff12ff7f6e04265dbbdbdfb7305aae15af63291
        LoteDao loteDao = new LoteDao();
        ProductoDAO productoDao = new ProductoDAO();
        string fecha;
        string fechaCaducidad;
        VentanaRegistro v = null;
<<<<<<< HEAD
=======

        /// <summary>
        /// Constructor de la clase AnnadirEntrada <see cref="AnnadirEntrada"/> class.
        /// </summary>
        /// <param name="vr">The vr.</param>
>>>>>>> 1ff12ff7f6e04265dbbdbdfb7305aae15af63291
        public AnnadirEntrada(VentanaRegistro vr)
        {
            InitializeComponent();
            CargarComboProductos();
            v = vr;
<<<<<<< HEAD
        }

        private void CargarComboProductos() {

            MySqlDataReader dr = productoDao.Cargarproductos();

            while (dr.Read())
            {
                cmbProductoEntrada.Items.Add(new
                {
                    id = dr["id_producto"].ToString(),
                    nombre = dr["nombre"].ToString()
                });
            }
            cmbProductoEntrada.DisplayMemberPath = "nombre";
            cmbProductoEntrada.SelectedValuePath = "id";
=======
            dtpFechaEntrada.SelectedDate = DateTime.Now;
            dtpCaducidad.SelectedDate = DateTime.Now.AddMonths(1);
>>>>>>> 1ff12ff7f6e04265dbbdbdfb7305aae15af63291
        }

        /// <summary>
        /// Metodo CargaComboProductos, donde rellenamos el combo con el nombre
        /// de los productos disponibles en la base de datos
        ///</summary>
        private void CargarComboProductos() {

            MySqlDataReader dr = productoDao.Cargarproductos();

            while (dr.Read())
            {
                cmbProductoEntrada.Items.Add(new
                {
                    id = dr["id_producto"].ToString(),
                    nombre = dr["nombre"].ToString()
                });
            }
            cmbProductoEntrada.DisplayMemberPath = "nombre";
            cmbProductoEntrada.SelectedValuePath = "id";
        }

        ///<summary>
        /// Evento onClick del boton Aceptar Entrada, el cual,
        /// primero revisa que todos los campos se hayan rellenado correctamente,
        /// sino es asi, informara del error. Después, si todos los campos estan correctamente 
        /// se insertaran los datos de la entrada en la base de datos
        ///</summary>
        ///<param name="sender"></param>
        ///<param name="e"></param>
        private void btnAceptarEntrada_Click(object sender, RoutedEventArgs e)
        {
            // declaracion de variables
            Boolean valor = false;
            string cantidad = "";
            double cantidaNumerico = 0;
            string mensaje = "Tienes que rellenar o seleccionar:";


            //revisamos que seleccionen un producto del combo
            if (cmbProductoEntrada.SelectedIndex.Equals(-1))
            {
                if (mensaje.Length > 34)
                {
                    mensaje += ",";
                }
                mensaje += " Producto";
                cmbProductoEntrada.Focus();
                cmbProductoEntrada.Background = System.Windows.Media.Brushes.LightCoral;
                valor = true;
            }
            else
            {
                cmbProductoEntrada.Background = Brushes.White;
            }

            //revisamos que se rellena el campo lote
            if (string.IsNullOrEmpty(txbLoteEntrada.Text))
            {
                if (mensaje.Length > 34)
                {
                    mensaje += ",";
                }
                mensaje += " Lote";
                txbLoteEntrada.Focus();
                txbLoteEntrada.Background = Brushes.LightCoral;
                valor = true;
            }
            else
            {
                txbLoteEntrada.Background = Brushes.White;
            }

            if (string.IsNullOrEmpty(txbCantidadEntrada.Text) || !int.TryParse(txbCantidadEntrada.Text, out int cant) || cant < 1)
            {
                if (mensaje.Length > 34)
                {
                    mensaje += ",";
                }
                mensaje += " Cantidad";
                txbCantidadEntrada.Focus();
                txbCantidadEntrada.Background = Brushes.LightCoral;
                valor = true;
            }
            else
            {
                cantidad = txbCantidadEntrada.Text;
                cantidad = cantidad.Replace(",", ".");
                if (!double.TryParse(cantidad, out cantidaNumerico))
                {
                    mensaje += "Cantidad [ Recuerde introducir solo numeros ]";
                    valor = true;
                }
                else
                {
                    txbCantidadEntrada.Background = Brushes.White;
                }
            }

            //revisamos que rellena el campo del proveedor
            if (string.IsNullOrEmpty(txbProveedor.Text))
            {
                if (mensaje.Length > 34)
                {
                    mensaje += ",";
                }
                mensaje += " Proveedor";
                txbProveedor.Focus();
                txbProveedor.Background = Brushes.LightCoral;
                valor = true;
            }
            else
            {
                txbProveedor.Background = Brushes.White;
            }

            //revisamos que rellena el campo del albaran
            if (string.IsNullOrEmpty(txbAlbaran.Text))
            {
                if (mensaje.Length > 34)
                {
                    mensaje += ",";
                }
                mensaje += " Albaran";
                txbAlbaran.Focus();
                txbAlbaran.Background = Brushes.LightCoral;
                valor = true;
            }
            else
            {
                txbAlbaran.Background = Brushes.White;
            }

            //si todos los campos estan correctamente
            if (valor == false)
            {
                mensaje = "Entrada introducida correctamente";
                // crear lote
                Lote lote = new Lote(txbLoteEntrada.Text,Int32.Parse(cmbProductoEntrada.SelectedValue.ToString()));

                // obetener fecha
                DateTime? selectedDate = dtpFechaEntrada.SelectedDate;
                DateTime? selectedDateCaducidad = dtpCaducidad.SelectedDate;
                if (selectedDate.HasValue)
                {
                   fecha = selectedDate.Value.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                }
                if (selectedDateCaducidad.HasValue)
                {
                    fechaCaducidad = selectedDateCaducidad.Value.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                }
<<<<<<< HEAD
=======
                double stockEntrada = Double.Parse(txbCantidadEntrada.Text);
>>>>>>> 1ff12ff7f6e04265dbbdbdfb7305aae15af63291
                // obtener stock del lote, si el lote  no existe se creara uno nuevo
                Double stockLote = loteDao.ObtenerStockLote(lote);
                if (stockLote == -1) {
                    Lote loteAInsertar = new Lote(txbLoteEntrada.Text,
                        Int32.Parse(cmbProductoEntrada.SelectedValue.ToString()),fechaCaducidad);

<<<<<<< HEAD
                    loteDao.InsertarLote(loteAInsertar, Double.Parse(txbCantidadEntrada.Text));
=======
                    loteDao.InsertarLote(loteAInsertar, 0);
>>>>>>> 1ff12ff7f6e04265dbbdbdfb7305aae15af63291

                    stockLote = 0;
                }
                // obtener stock del producto
                Double stockProducto = productoDao.ObtenerStockProducto(cmbProductoEntrada.Text);

                // crear entrada e insertarla
<<<<<<< HEAD
                Entrada entrada = new Entrada(fecha,lote,
                    Double.Parse(txbCantidadEntrada.Text
                    ) 
                    ,txbObservacionesEntrada.Text,stockLote,stockProducto,txbProveedor.Text,fechaCaducidad,txbAlbaran.Text);
=======
                

                Entrada entrada = new Entrada(fecha,lote,stockEntrada
                    ,txbObservacionesEntrada.Text,stockLote+stockEntrada,stockProducto+stockEntrada,txbProveedor.Text,fechaCaducidad,txbAlbaran.Text);
>>>>>>> 1ff12ff7f6e04265dbbdbdfb7305aae15af63291
               
                loteDao.InsertarEntrada(entrada);
                v.CargarDataGrid();
                v.dtgprincipal.UpdateLayout();
                v.recorrerjlist();
                this.Close();
            }
            MessageBox.Show(mensaje + ".", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Information);
        }

<<<<<<< HEAD
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
        
=======
        /// <summary>
        /// Evento onClick del boton Cancelar, el cual cerrara la ventana de Entradas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelarentrada_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmbProductoEntrada_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string n = "";
            n = (sender as ComboBox).SelectedItem.ToString();

            string patron = @"nombre\s*=\s*(\w+)";
            Match match = Regex.Match(n, patron);
            if (match.Success)
            {
                string nombreProducto = match.Groups[1].Value.Trim();
                MySqlDataReader dr = null;
                String unidad = "";

                dr = productoDao.ObtenerUnidad(nombreProducto);
                while (dr.Read())
                {
                    unidad = dr.GetString(0);
                }
                lblUnidad.Content = unidad;
            }
        }
>>>>>>> 1ff12ff7f6e04265dbbdbdfb7305aae15af63291
    }
}
