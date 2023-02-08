using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoregV2.Dominio
{
    internal class Producto
    {
        private int id;
        private string nombre;
        private string unidad;
        private Double stock;
        private Bitmap imagen;


        public Producto() { 
        
        }
        public Producto(int i,string nom,string un,Double st,Bitmap image)
        {
            id = i;
            nombre = nom;
            unidad = un;
            stock = st;
            imagen = image;
        }

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Unidad { get => unidad; set => unidad = value; }
        public double Stock { get => stock; set => stock = value; }
        public Bitmap Imagen { get => imagen; set => imagen = value; }
    }
}
