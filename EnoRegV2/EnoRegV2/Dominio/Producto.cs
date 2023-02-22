using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoregV2.Dominio
{
    /// <summary>
    /// Clase Producto
    /// </summary>
    public class Producto
    {
        //declaracion de variables
        private int id;
        private string nombre;
        private string unidad;
        private Double stock;
        private byte[] imagen;


        /// <summary>
        /// Constructor vacio de la clase Producto <see cref="Producto"/> class.
        /// </summary>
        public Producto() { 
        
        }

        /// <summary>
        /// Constructor donde le pasamos el nombre <see cref="Producto"/> class.
        /// </summary>
        /// <param name="nom"></param>
        public Producto(String nom)
        {
            this.nombre = nom;
        }

        /// <summary>
        /// Consstructor <see cref="Producto"/> class.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="nom"></param>
        /// <param name="un"></param>
        /// <param name="st"></param>
        /// <param name="image"></param>
        public Producto(int i, string nom, string un, Double st, byte[] image)
        {

        }

        /// <summary>
        /// Constructor donde le pasamos el id y el nombre <see cref="Producto"/> class.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <param name="nom">The nom.</param>
        public Producto(int ids,string nom)
        {
            id = ids;
            nombre = nom;
        }

        /// <summary>
        /// Constructor <see cref="Producto"/> class.
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="un"></param>
        /// <param name="st"></param>
        /// <param name="image"></param>
       public Producto(string nom,string un,Double st, byte[] image)

        {
            nombre = nom;
            unidad = un;
            stock = st;
            imagen = image;
        }

        /// <summary>
        /// Constructor <see cref="Producto"/> class.
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="un"></param>
        /// <param name="st"</param>
        public Producto(string nom, string un, Double st)
        {
            nombre = nom;
            unidad = un;
            stock = st;
        }

        //getters & setters
        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Unidad { get => unidad; set => unidad = value; }
        public double Stock { get => stock; set => stock = value; }
        public byte[] Imagen 
        { 
            get => imagen; 
            set => imagen = value; 
        }

        public Bitmap ByteArrayToImage(byte[] source)
        {
            using (var ms = new MemoryStream(source))
            {
                return new Bitmap(ms);
            }
        }
        public void SetImagenAByte(Image img)
        { 
            ImageConverter converter = new ImageConverter();
            imagen=(byte[])converter.ConvertTo(img, typeof(byte[]));
        }
    }
}
