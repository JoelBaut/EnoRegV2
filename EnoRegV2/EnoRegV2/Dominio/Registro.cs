using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoregV2.Dominio { 
    internal class Registro
    {
        private string fecha;
        private Lote lote;
        private Double cantidad;
        private string observaciones;
        private Double cantidadLote;
        private double cantidadProducto;

        public Registro() { }
        public Registro(string f,Lote l,Double c,string o,Double cantLote,double cantProducto) { 
            fecha= f;
            lote = l;
            cantidad = c;
            observaciones = o;
            cantidadLote= cantLote;
            cantidadProducto= cantProducto;
        }
        public string Fecha { get => fecha; set => fecha = value; }
        public double Cantidad { get => cantidad; set => cantidad = value; }
        public string Observaciones { get => observaciones; set => observaciones = value; }
        public double CantidadLote { get => cantidadLote; set => cantidadLote = value; }
        public double CantidadProducto { get => cantidadProducto; set => cantidadProducto = value; }
        public Lote Lote { get => lote; set => lote = value; }
    }
}
