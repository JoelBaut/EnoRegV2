using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoregV2.Dominio
{
    internal class Salida : Registro
    {
        private string destino;

        public Salida(string f, Lote l, Double c, string o, Double cantLote, double cantProducto,string des) : base(f, l, c, o, cantLote, cantProducto)
        {
            destino = des;
        }

        public string Destino { get => destino; set => destino = value; }
    }
}
