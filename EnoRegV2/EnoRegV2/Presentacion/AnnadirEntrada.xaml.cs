using EnoregV2.Dominio;
using MySql.Data.MySqlClient;
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
using System.Windows.Shapes;
using VentanaRegistros;

namespace EnoReV2
{
    /// <summary>
    /// Lógica de interacción para AnnadirEntrada.xaml
    /// </summary>
    public partial class AnnadirEntrada : Window
    {
        LoteDao loteDao = new LoteDao();
        ProductoDAO productoDao = new ProductoDAO();
        string fecha;
        string fechaCaducidad;
        VentanaRegistro v = null;
        public AnnadirEntrada(VentanaRegistro vr)
        {
            InitializeComponent();
            CargarComboProductos();
            v = vr;
            dtpFechaEntrada.SelectedDate = DateTime.Now;
            dtpCaducidad.SelectedDate = DateTime.Now.AddMonths(1);
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
        }

        private void btnAceptarEntrada_Click(object sender, RoutedEventArgs e)
        {
            Boolean valor = false;
            string cantidad = "";
            double cantidaNumerico = 0;
            string mensaje = "Tienes que rellenar o seleccionar:";
            if (dtpFechaEntrada.SelectedDate == DateTime.Now.Date)
            {
                if (mensaje.Length > 34)
                {
                    mensaje += ",";
                }
                mensaje += " la fecha de entrada";
                dtpFechaEntrada.Focus();
                valor = true;
            }
            if (cmbProductoEntrada.SelectedIndex.Equals(-1))
            {
                if (mensaje.Length > 34)
                {
                    mensaje += ",";
                }
                mensaje += " Producto";
                cmbProductoEntrada.Focus();
                cmbProductoEntrada.Background = Brushes.LightCoral;
                valor = true;
            }
            else
            {
                cmbProductoEntrada.Background = Brushes.White;
            }
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
            if (dtpCaducidad.SelectedDate == DateTime.Now.Date)
            {
                if (mensaje.Length > 34)
                {
                    mensaje += ",";
                }
                mensaje += " la fecha de caducidad";
                dtpCaducidad.Focus();
                valor = true;
            }
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
                // obtener stock del lote, si el lote  no existe se creara uno nuevo
                Double stockLote = loteDao.ObtenerStockLote(lote);
                if (stockLote == -1) {
                    Lote loteAInsertar = new Lote(txbLoteEntrada.Text,
                        Int32.Parse(cmbProductoEntrada.SelectedValue.ToString()),fechaCaducidad);

                    loteDao.InsertarLote(loteAInsertar, Double.Parse(txbCantidadEntrada.Text));

                    stockLote = 0;
                }
                // obtener stock del producto
                Double stockProducto = productoDao.ObtenerStockProducto(cmbProductoEntrada.Text);

                // crear entrada e insertarla
                Entrada entrada = new Entrada(fecha,lote,
                    Double.Parse(txbCantidadEntrada.Text
                    ) 
                    ,txbObservacionesEntrada.Text,stockLote,stockProducto,txbProveedor.Text,fechaCaducidad,txbAlbaran.Text);
               
                loteDao.InsertarEntrada(entrada);
                v.CargarDataGrid();
                v.dtgprincipal.UpdateLayout();
                v.recorrerjlist();
                this.Close();
            }
            MessageBox.Show(mensaje + ".", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnCancelarentrada_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
