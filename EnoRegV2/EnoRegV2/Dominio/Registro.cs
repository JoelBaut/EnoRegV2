using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoregV2.Dominio { 
    internal class Registro
    {
        //declaracion de variables
        private string fecha;
        private Lote lote;
        private Double cantidad;
        private string observaciones;
        private Double cantidadLote;
        private double cantidadProducto;

        /// <summary>
        /// Constructor vacio de la clase Registros <see cref="Registro"/> class.
        /// </summary>
        public Registro() { }

        /// <summary>
        /// Constructor con parametros de la clase Registros <see cref="Registro"/> class.
        /// </summary>
        /// <param name="f"></param>
        /// <param name="l"></param>
        /// <param name="c"></param>
        /// <param name="o"></param>
        /// <param name="cantLote"></param>
        /// <param name="cantProducto"></param>
        public Registro(string f,Lote l,Double c,string o,Double cantLote,double cantProducto) { 
            fecha= f;
            lote = l;
            cantidad = c;
            observaciones = o;
            cantidadLote= cantLote;
            cantidadProducto= cantProducto;
        }

        //getters & setters
        public string Fecha { get => fecha; set => fecha = value; }
        public double Cantidad { get => cantidad; set => cantidad = value; }
        public string Observaciones { get => observaciones; set => observaciones = value; }
        public double CantidadLote { get => cantidadLote; set => cantidadLote = value; }
        public double CantidadProducto { get => cantidadProducto; set => cantidadProducto = value; }
        public Lote Lote { get => lote; set => lote = value; }
    }
}
