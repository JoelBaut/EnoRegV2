using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoregV2.Dominio
{
    internal class Entrada : Registro
    {
        private string proveedor;
        private string caducidad;
        private string albaran;
        public Entrada(string f, Lote l, Double c, string o, Double cantLote, double cantProducto,string prov,string cad,string alba) : base(f, l, c, o, cantLote, cantProducto)
        { 
            proveedor = prov; 
            caducidad = cad;
            albaran = alba;
        }

        public string Proveedor { get => proveedor; set => proveedor = value; }
        public string Caducidad { get => caducidad; set => caducidad = value; }
        public string Albaran { get => albaran; set => albaran = value; }
    }
}
