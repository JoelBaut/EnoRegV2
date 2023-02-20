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
        //declaracion de variables
        private ProductoDAO productoDAO;
        private VentanaRegistro ventanaRegistro;

        /// <summary>
        /// Constructor con parametros de la ventana Filtros <see cref="Filtros"/> class.
        /// </summary>
        /// <param name="ventanaRegistro"></param>
        /// <param name="productoDAO"></param>
        public Filtros(VentanaRegistro ventanaRegistro, ProductoDAO productoDAO)
        {
            InitializeComponent();
            this.productoDAO = productoDAO;
            this.ventanaRegistro = ventanaRegistro;
            cargarComboProductos();
            cargarComboRegistros();
            dpFecha.SelectedDate = new DateTime(2000, 1, 1);
            dpFechafinal.SelectedDate = new DateTime(2050, 1, 1);
        }

        /// <summary>
        /// Metodo cargarComboRegistros, en el cuál se insertan 
        /// los tipos entradas, salidas y entradas/salidas
        /// </summary>
        private void cargarComboRegistros()
        {
            cmbEntradaSalida.Items.Insert(0, new { id = 0, nombre = "Entradas/Salidas" });
            cmbEntradaSalida.Items.Insert(1, new { id = 1, nombre = "Entradas" });
            cmbEntradaSalida.Items.Insert(2, new { id = 2, nombre = "Salidas" });

            cmbEntradaSalida.DisplayMemberPath = "nombre";
            cmbEntradaSalida.SelectedValuePath = "id";
            cmbEntradaSalida.SelectedIndex = 0;
        }

        /// <summary>
        /// Metodo cargarComboProductos, el cual carga el combo
        /// con los productos que hay disponibles en la base de datos
        /// </summary>
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

        /// <summary>
        /// Evento onClick del boton Aceptar, el cual confirmara
        /// los datos seleccionados en la ventana y cargara esos datos
        /// y los mostrara en el data table 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"> <see cref="RoutedEventArgs"/></param>
        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            //declaracion de variables
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

            //se cargan los datos en el data table
            DataTable dt = new DataTable();
            dt.Load(productoDAO.CargarFiltro(cmbEntradaSalida.Text, fecha, Fechafinal, cmbProducto.Text, txtDestino.Text, txtLote.Text));
            ventanaRegistro.dtgprincipal.ItemsSource = dt.DefaultView;

            ventanaRegistro.dtgprincipal.UpdateLayout();
            ventanaRegistro.recorrerjlist();
            this.Close();
        }

        /// <summary>
        /// Evento onClick del boton Cancelar, el cual cerrara la ventana
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"> <see cref="RoutedEventArgs"/></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Evento TextChanged del txtDestino, donde si el campo no esta vacío,
        /// indicamos que se filtran por salidas y si el campo esta rellenado,
        /// filtramos por entradas y salidas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"> <see cref="TextChangedEventArgs"/></param>
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
