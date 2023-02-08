using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoregV2.Persistencia
{
    internal class Lote
    {
        private int idProducto;
        private Date fechaCaducidad;
        private double stock;

        public Lote(int idProducto, Date fechaCaducidad, double stock)
        {
            this.idProducto = idProducto;
            this.fechaCaducidad = fechaCaducidad;
            this.stock = stock;
        }

        public int IdProducto { get => idProducto; set => idProducto = value; }
        public Date FechaCaducidad { get => fechaCaducidad; set => fechaCaducidad = value; }
        public double Stock { get => stock; set => stock = value; }
    }
}