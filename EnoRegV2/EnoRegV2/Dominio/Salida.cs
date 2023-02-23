using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoregV2.Dominio
{
<<<<<<< HEAD
    internal class Salida : Registro
    {
=======
    /// <summary>
    /// Clase Salida
    /// </summary>
    /// <seealso cref="EnoregV2.Dominio.Registro" />
    internal class Salida : Registro
    {
        //declaracion de variable
        private string destino;

        /// <summary>
        /// Constructor con parametros de la claase Salida <see cref="Salida"/> class.
        /// </summary>
        /// <param name="f"></param>
        /// <param name="l"></param>
        /// <param name="c"></param>
        /// <param name="o"></param>
        /// <param name="cantLote"></param>
        /// <param name="cantProducto"></param>
        /// <param name="des"></param>
        public Salida(string f, Lote l, Double c, string o, Double cantLote, double cantProducto,string des) : base(f, l, c, o, cantLote, cantProducto)
        {
            destino = des;
        }

        //getters & setters
        public string Destino { get => destino; set => destino = value; }
>>>>>>> 1ff12ff7f6e04265dbbdbdfb7305aae15af63291
    }
}
