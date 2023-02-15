using EnoregV2.Dominio;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VentanaRegistros;
using static System.Net.Mime.MediaTypeNames;

namespace EnoregV2
{
    /// <summary>
    /// Lógica de interacción para VentanaAddProducto.xaml
    /// </summary>
    public partial class VentanaAddProducto : Window
    {
        ProductoDAO p = null;
        MySqlDataReader dr;
        VentanaRegistro v = null;
        BitmapImage imagen = null;
        public VentanaAddProducto(VentanaRegistro a)
        {
            InitializeComponent();
            p = new ProductoDAO();
            dr = p.CargarUnidad();
            // añadir opcion cualquier producto
            cbUnidades.Items.Insert(0, "Cualquiera" );
            cbUnidades.Items.Insert(1, "Kg");
            cbUnidades.Items.Insert(2, "G");
            cbUnidades.Items.Insert(3, "L");
            cbUnidades.Items.Insert(4, "ml");
            cbUnidades.SelectedIndex = 0;
            v = a;
        }

        private void btnimagen_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();

            openFileDialog.Filter = "Archivos de imagen (*.png, *.jpg)|*.png;*.jpg";

            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
                imagen = new BitmapImage();
                imagen.BeginInit();
                imagen.UriSource = new Uri(openFileDialog.FileName);
                ImgImagen.Source = imagen;
                imagen.EndInit();                
            }
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            String mensaje = "";
            Boolean cumple = false;
            if (txbNombreProducto.Text == "") {
                mensaje = "Introduce un nombre de producto correcto"; 
                cumple = true;
            }
            if (p.CompruebaProductos(txbNombreProducto.Text)) 
            {
                mensaje = "Introduce un nombre que no exista";
                cumple = true;
            }
            if (cbUnidades.SelectedIndex ==  0)
            {
                if (mensaje.Length > 10) 
                {
                    mensaje += " y ";
                }
                mensaje += "Selecciona una Unidad";
                cumple = true;
            }
            if (cumple)
            {
                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else 
            {
                Producto pe = null;
                if (imagen == null)
                {
                    pe = new Producto(txbNombreProducto.Text, cbUnidades.SelectedValue.ToString(), 0);
                }
                else 
                {
                    Stream stream = imagen.StreamSource;
                    Byte[] buffer = null;
                    if (stream != null && stream.Length > 0)
                    {
                        using (BinaryReader br = new BinaryReader(stream))
                        {
                            buffer = br.ReadBytes((Int32)stream.Length);
                        }
                    }
                    pe = new Producto(txbNombreProducto.Text, cbUnidades.SelectedValue.ToString(), 0, buffer);
                   
                }
                if (pe != null)
                {
                    p.InsertarProducto(pe);
                }
                v.CargaDataGrid();
                Close();                
            }
        }
    }
}
