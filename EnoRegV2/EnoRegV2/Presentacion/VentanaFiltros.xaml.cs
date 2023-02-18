using EnoregV2.Dominio;
using MySql.Data.MySqlClient;
using Mysqlx.Cursor;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace EnoregV2
{
    /// <summary>
    /// Lógica de interacción para Filtros.xaml
    /// </summary>
    public partial class Filtros : Window
    {
        private ProductoDAO productoDAO;
        private VentanaRegistro ventanaRegistro;

        public Filtros(VentanaRegistro ventanaRegistro)
        {
            InitializeComponent();
            productoDAO = new ProductoDAO();
            this.ventanaRegistro = ventanaRegistro;
            cargarComboProductos();
            cargarComboRegistros();
            dpFecha.SelectedDate = new DateTime(2000, 1, 1);
            dpFechafinal.SelectedDate = new DateTime(2050, 1, 1);
        }

        private void cargarComboRegistros()
        {
            cmbEntradaSalida.Items.Insert(0, new { id = 0, nombre = "Entradas/Salidas" });
            cmbEntradaSalida.Items.Insert(1, new { id = 1, nombre = "Entradas" });
            cmbEntradaSalida.Items.Insert(2, new { id = 2, nombre = "Salidas" });

            cmbEntradaSalida.DisplayMemberPath = "nombre";
            cmbEntradaSalida.SelectedValuePath = "id";
            cmbEntradaSalida.SelectedIndex = 0;
        }

        private void cargarComboProductos()
        {
            MySqlDataReader dr = productoDAO.Cargarproductos();

            while (dr.Read())
            {
                cmbProducto.Items.Add(new
                {
                    id = dr["id_producto"].ToString(),
                    nombre = dr["nombre"].ToString()
                });
            }
            cmbProducto.DisplayMemberPath = "nombre";
            cmbProducto.SelectedValuePath = "id";

            cmbProducto.Items.Insert(0, new { id = 0, nombre = "<Cualquiera>" });
            cmbProducto.SelectedIndex = 0;
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            string fecha = null;
            string Fechafinal = null;
            // obetener fecha
            DateTime? selectedDate = dpFecha.SelectedDate;
            DateTime? selectedDateCaducidad = dpFechafinal.SelectedDate;
            if (selectedDate.HasValue)
            {
                fecha = selectedDate.Value.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            }
            if (selectedDateCaducidad.HasValue)
            {
                Fechafinal = selectedDateCaducidad.Value.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            }

            DataTable dt = new DataTable();
            dt.Load(productoDAO.CargarFiltro(cmbEntradaSalida.Text, fecha, Fechafinal, cmbProducto.Text, txtDestino.Text, txtLote.Text));
            ventanaRegistro.dtgprincipal.ItemsSource = dt.DefaultView;

            ventanaRegistro.dtgprincipal.UpdateLayout();
            ventanaRegistro.recorrerjlist();
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtDestino_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!txtDestino.Text.Equals(""))
            {
                cmbEntradaSalida.SelectedIndex = 2;
            }
            else
            {
                cmbEntradaSalida.SelectedIndex = 0;
            }
        }
    }
}
