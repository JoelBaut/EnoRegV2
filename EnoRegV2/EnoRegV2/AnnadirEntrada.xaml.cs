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

namespace EnoReV2
{
    /// <summary>
    /// Lógica de interacción para AnnadirEntrada.xaml
    /// </summary>
    public partial class AnnadirEntrada : Window
    {
        public AnnadirEntrada()
        {
            InitializeComponent();
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
            if (string.IsNullOrEmpty(txbCantidadEntrada.Text))
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
                //productoDAO.InsertarEntrada(cmbProductoEntrada.Text, dtpFechaEntrada.Value.ToString("yyyy-MM-dd"), txbLoteEntrada.Text, txbAlbaran.Text, txbProveedor.Text, dtpCaducidad.Value.ToString("yyyy-MM-dd"), cantidad);
                //productoDAO.cerrarConexion();
                //DialogResult = DialogResult.OK;
                //this.Close();
            }
            MessageBox.Show(mensaje + ".", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
