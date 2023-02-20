
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoregV2.Dominio
{
    /// <summary>
    /// Clase Lote
    /// </summary>
    internal class Lote
    {
        //declaracion de variables
        private string codigoLote;
        private int idProducto;
        private string fechaCaducidad;

        /// <summary>
        /// Primer constructor de la clase Lote <see cref="Lote"/> class.
        /// </summary>
        /// <param name="codLote"></param>
        /// <param name="idProd"></param>
        /// <param name="fechaCaducidad"></param>
        public Lote(string codLote,int idProd, string fechaCaducidad)
        {
            this.codigoLote= codLote;
            this.idProducto = idProd;
            this.fechaCaducidad = fechaCaducidad;
        }

        /// <summary>
        /// Segundo constructor de la clase Lote <see cref="Lote"/> class.
        /// </summary>
        /// <param name="codLote"></param>
        /// <param name="idProd"></param>
        public Lote(string codLote,int idProd)
        {
            this.codigoLote= codLote;
            this.idProducto = idProd;
        }

        //getters & setters
        public int IdProducto { get => idProducto; set => idProducto = value; }
        public string CodigoLote { get => codigoLote; set => codigoLote = value; }
        public string FechaCaducidad { get => fechaCaducidad; set => fechaCaducidad = value; }
    }
}