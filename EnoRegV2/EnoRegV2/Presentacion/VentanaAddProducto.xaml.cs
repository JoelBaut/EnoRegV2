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
        byte[] imagenBytes = null;
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
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos de imagen (*.bmp, *.jpg, *.jpeg, *.png)|*.bmp;*.jpg;*.jpeg;*.png";

            if (openFileDialog.ShowDialog() == true)
            {
                BitmapImage imagenBitmap = new BitmapImage(new Uri(openFileDialog.FileName));
                ImgImagen.Source = imagenBitmap;
                imagenBytes = null;

                using (MemoryStream stream = new MemoryStream())
                {
                    BitmapEncoder encoder = new JpegBitmapEncoder(); // O cualquier otro tipo de codificador de imagen
                    encoder.Frames.Add(BitmapFrame.Create(imagenBitmap));
                    encoder.Save(stream);
                    imagenBytes = stream.ToArray();
                }

                // Aquí puedes hacer lo que necesites con el arreglo de bytes de la imagen
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
                try
                {                    
                    pe = new Producto(txbNombreProducto.Text, cbUnidades.SelectedValue.ToString(), 0, imagenBytes);                   
                    if (pe != null)
                    {
                        p.InsertarProducto(pe);
                    }
                }
                catch (NullReferenceException es)
                {
                    pe = new Producto(txbNombreProducto.Text, cbUnidades.SelectedValue.ToString(), 0);
                }
                v.CargaDataGrid();
                Close();                
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
