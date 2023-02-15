
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoregV2.Dominio
{
    internal class Lote
    {
        private string codigoLote;
        private int idProducto;
        private string fechaCaducidad;
        public Lote(string codLote,int idProd, string fechaCaducidad)
        {
            this.codigoLote= codLote;
            this.idProducto = idProd;
            this.fechaCaducidad = fechaCaducidad;
        }

        public Lote(string codLote,int idProd)
        {
            this.codigoLote= codLote;
            this.idProducto = idProd;
        }


        public int IdProducto { get => idProducto; set => idProducto = value; }
        public string CodigoLote { get => codigoLote; set => codigoLote = value; }
        public string FechaCaducidad { get => fechaCaducidad; set => fechaCaducidad = value; }
    }
}