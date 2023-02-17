using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoregV2.Dominio
{
    /// <summary>
    /// Clase Entrada
    /// </summary>
    /// <seealso cref="EnoregV2.Dominio.Registro" />
    internal class Entrada : Registro
    {
        //declaracion de variables
        private string proveedor;
        private string caducidad;
        private string albaran;

        /// <summary>
        /// Constructor de la clase Entrada <see cref="Entrada"/> class.
        /// </summary>
        /// <param name="f"></param>
        /// <param name="l"></param>
        /// <param name="c"></param>
        /// <param name="o"></param>
        /// <param name="cantLote"></param>
        /// <param name="cantProducto"></param>
        /// <param name="prov"></param>
        /// <param name="cad"></param>
        /// <param name="alba"></param>
        public Entrada(string f, Lote l, Double c, string o, Double cantLote, double cantProducto,string prov,string cad,string alba) : base(f, l, c, o, cantLote, cantProducto)
        { 
            proveedor = prov; 
            caducidad = cad;
            albaran = alba;
        }

        //getters & setters
        public string Proveedor { get => proveedor; set => proveedor = value; }
        public string Caducidad { get => caducidad; set => caducidad = value; }
        public string Albaran { get => albaran; set => albaran = value; }
    }
}
