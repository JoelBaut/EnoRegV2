using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Lógica de interacción para AnnadirSalida.xaml
    /// </summary>
    public partial class AnnadirSalida : Window
    {
        public AnnadirSalida()
        {
            InitializeComponent();
        }

        private void btnAceptarSalida_Click(object sender, RoutedEventArgs e)
        {
            string mensaje = "Tienes que rellenar o seleccionar:";
            Boolean valor = false;
            double cantidaNumerico = 0;
            string cantidad = "";
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
            if (cmbLoteSalida.SelectedIndex.Equals(-1))
            {
                if (mensaje.Length > 34)
                {
                    mensaje += ",";
                }
                mensaje += " Producto";
                cmbLoteSalida.Focus();
                cmbLoteSalida.Background = Brushes.LightCoral;
                valor = true;
            }
            else
            {
                cmbLoteSalida.Background = Brushes.White;
            }
            if (string.IsNullOrEmpty(txbCantidadSalida.Text))
            {
                if (mensaje.Length > 34)
                {
                    mensaje += ",";
                }
                mensaje += " Cantidad";
                txbCantidadSalida.Focus();
                txbCantidadSalida.Background = Brushes.LightCoral;
                valor = true;
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
            /*
            if (valor == false && (productoDAO.ObtenerStock(cmbProductoSalida.Text) - Convert.ToDecimal(cantidad, CultureInfo.InvariantCulture)) <= 0)
            {
                MessageBox.Show("Se pretende sacar mas productos de los disponibles.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Information);
                valor = true;
            }
            if (valor == false)
            {
                mensaje = "Salida introducida correctamente";
                //productoDAO.InsertarSalida(cmbProductos.Text, dtpFechaSalida.Value.ToString("yyyy-MM-dd"), txbLote.Text, cantidad, txbDestino.Text, txbObservaciones.Text);
                //productoDAO.cerrarConexion();
                //DialogResult = DialogResult.OK;
                //this.Close();
            }
            */
            if (mensaje.Length > 34)
            {
                MessageBox.Show(mensaje + ".", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Information);
            }            
        }
    }
}
