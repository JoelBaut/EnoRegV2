﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoregV2.Dominio
{
    public class Producto
    {
        private int id;
        private string nombre;
        private string unidad;
        private Double stock;
        private byte[] imagen;


        public Producto() { 
        
        }
        public Producto(string nom,string un,Double st, byte[] image)
        {
            nombre = nom;
            unidad = un;
            stock = st;
            imagen = image;
        }

        public Producto(string nom, string un, Double st)
        {
            nombre = nom;
            unidad = un;
            stock = st;
        }
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
