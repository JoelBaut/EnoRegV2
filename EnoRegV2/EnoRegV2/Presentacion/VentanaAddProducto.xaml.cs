using EnoregV2.Dominio;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.Drawing;
=======
using System.Data;
using System.Drawing;
using System.IO;
>>>>>>> 1ff12ff7f6e04265dbbdbdfb7305aae15af63291
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
<<<<<<< HEAD
        private Font Font;

        public VentanaAddProducto()
=======
        //declaracion de variables
        ProductoDAO p = null;
        MySqlDataReader dr;
        VentanaRegistro v = null;
        byte[] imagenBytes = null;

        /// <summary>
        /// Constructor de VentanaAddProductos <see cref="VentanaAddProducto"/> class.
        /// </summary>
        /// <param name="a"></param>
        public VentanaAddProducto(VentanaRegistro a)
>>>>>>> 1ff12ff7f6e04265dbbdbdfb7305aae15af63291
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

        /// <summary>
        /// Evento onClick del boton Buscar Imagen, donde añadimos una imagen
        /// al nuevo producto que vamos a insertar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"> <see cref="RoutedEventArgs"/></param>
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

        /// <summary>
        /// Evento onClick del boton Aceptar añadir producto, donde revisamos
        /// que los campos se rellenen correctamente y finalmente insertamos 
        /// esos datos del nuevo producto en la base de datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"> <see cref="RoutedEventArgs"/></param>
        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            //declaracion de variables
            String mensaje = "";
            Boolean cumple = false;

            //revisamos que rellena el campo con un nombre
            if (txbNombreProducto.Text == "") {
                mensaje = "Introduce un nombre de producto correcto"; 
                cumple = true;
            }

            //si el nombre ya existe en la base de datos, lo indicamos
            if (p.CompruebaProductos(txbNombreProducto.Text)) 
            {
                mensaje = "Introduce un nombre que no exista";
                cumple = true;
            }

            //revisamo que introduce la unidad del producto
            if (cbUnidades.SelectedIndex ==  0)
            {
                if (mensaje.Length > 10) 
                {
                    mensaje += " y ";
                }
                mensaje += "Selecciona una Unidad";
                cumple = true;
            }

            //indicamos el error si lo hubiese
            if (cumple)
            {
                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            //sino, insertamos el nuevo producto en la base de datos
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
    }
}
