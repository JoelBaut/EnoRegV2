
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoregV2.Dominio
{
    internal class Lote
    {
        private 
        private int idProducto;
        private DateTime fechaCaducidad;
        private double stock;

        public Lote(int idProducto, DateTime fechaCaducidad, double stock)
        {
            this.idProducto = idProducto;
            this.fechaCaducidad = fechaCaducidad;
            this.stock = stock;
        }

        public int IdProducto { get => idProducto; set => idProducto = value; }
        public DateTime FechaCaducidad { get => fechaCaducidad; set => fechaCaducidad = value; }
        public double Stock { get => stock; set => stock = value; }
    }
}