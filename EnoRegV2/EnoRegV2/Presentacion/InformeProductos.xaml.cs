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
using CrystalDecisions.ReportAppServer;
using MySql.Data.MySqlClient;

namespace EnoregV2.Presentacion
{
    /// <summary>
    /// Lógica de interacción para InformeProductos.xaml
    /// </summary>
    public partial class InformeProductos : Window
    {
        /// <summary>
        /// Constructor de la ventana InformeProductos <see cref="InformeProductos"/> class.
        /// </summary>
        public InformeProductos()
        {
            InitializeComponent();
            reportViewer.Owner = this;
        }

        /// <summary>
        /// Metodo cargarCombo, el cual cargara el nombre de los productos
        /// disponibles en la base de datos en ese momento
        /// </summary>
        private void cargarCombo()
        {
            //declaracion de variables
            MySqlConnection dbconn = null;
            string server = "localhost";
            string database = "bodega_registro";
            string uid = "root";
            string password = "";
            string connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            dbconn = new MySqlConnection(connectionString);

            // Abrimos la conexión
            try
            {
                dbconn.Open();
                string query = "select id_producto, nombre from producto";
                //Creamos el comando (sentencia SQL)
                MySqlCommand cmd = new MySqlCommand(query, dbconn);
                //Creamos un datareader y ejecutamos el comando
                MySqlDataReader dataReader = cmd.ExecuteReader();
                //Leemos los datos y los almacenamos en un combo
                cmbProductos.Items.Add(new
                {
                    id_producto = 0,
                    nombre = "TODOS"
                });
                while (dataReader.Read())
                {  
                    // Añadimos un nuevo elemento
                    cmbProductos.Items.Add(new
                    {
                        id_producto = dataReader["id_producto"].ToString(),
                        nombre = dataReader["nombre"].ToString()
                    });
                }
                // Indicamos qué campo será el mostrado y cuál el que tenga el valor
                cmbProductos.SelectedValuePath = "id_producto";
                cmbProductos.DisplayMemberPath = "nombre";
                //Cerramos Data Reader
                dataReader.Close();
                // Cerramos la conexión
                dbconn.Close();

                cmbProductos.SelectedIndex = 0;
            }
            catch (MySqlException)
            {
                MessageBox.Show("Compruebe que la base de datos está disponible", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Evento Loaded de la ventana, el cual cargara el combo de productos 
        /// al abrir la ventana
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"> <see cref="RoutedEventArgs"/></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cargarCombo();            
        }

        /// <summary>
        /// Evento onClick del boton Cargar, el cual cargara el informe
        /// segun el producto seleccionado en el combo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"> <see cref="RoutedEventArgs"/></param>
        private void btnCargar_Click(object sender, RoutedEventArgs e)
        {
            if (cmbProductos.SelectedValue.ToString().Equals("0"))
            {
                CrystalReport1 crystalReport1 = new CrystalReport1();
                reportViewer.ViewerCore.ReportSource = crystalReport1;
            }
            else
            {
                CrystalReport2 cargarReport2 = new CrystalReport2();                
                cargarReport2.SetParameterValue("PARAM_ID", cmbProductos.SelectedValue);
                reportViewer.ViewerCore.ReportSource = cargarReport2;
            }
        }
    }
}
