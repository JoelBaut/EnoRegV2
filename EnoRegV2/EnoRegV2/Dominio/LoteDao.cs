<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
=======
﻿using EnoregV2.Persistencia;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
>>>>>>> 1ff12ff7f6e04265dbbdbdfb7305aae15af63291

namespace EnoregV2.Dominio
{
    internal class LoteDao
    {
<<<<<<< HEAD
=======
        ConexionDB conexionDB = new ConexionDB();
        public LoteDao() { }

        /// <summary>
        /// metodo para insertar un lote.
        /// </summary>
        /// <param name="lote">The lote.</param>
        /// <param name="stock">The stock.</param>
        public void InsertarLote(Lote lote,Double stock)
        {
            String sql = "insert into lote values(null," + lote.IdProducto + ",'"+lote.CodigoLote+"','"+lote.FechaCaducidad+"',"+stock+")";
            conexionDB.Insertar(sql);
        }
        /// <summary>
        /// metodo para buscar los lotes de un producto.
        /// </summary>
        /// <param name="p">The productoDAO.</param>
        /// <returns>DataTable. con los datos de los lotes</returns>
        public DataTable BuscarLotePProducto(Producto p)
        {
            
            String sql = "SELECT * from lote where id_producto = (SELECT id_producto from producto where nombre = '"+p.Nombre+"' )";
            MySqlDataReader dataReader = conexionDB.BuscarLotesPorProducto(sql);

            DataTable dt = new DataTable();
            dt.Load(dataReader);

            conexionDB.cerrarConexion();

            return dt;
        }
        /// <summary>
        /// metodo para insertar una salida
        /// </summary>
        /// <param name="s">The s.</param>
        public void InsertarSalida(Salida s,Boolean liquidar) 
        {
            // obtener id del lote
            String BuscarIdLote = "select id_lote from lote l where l.lote = '" + s.Lote.CodigoLote + "' group by id_lote";
            MySqlDataReader dataReader = conexionDB.Select(BuscarIdLote);

            string idLote = null;
            while (dataReader.Read())
            {
                idLote = dataReader.GetString(0);
            }

            String sql = null;
            if (liquidar)
            {
                s.Cantidad = s.CantidadLote;
                s.CantidadLote = 0;
                s.CantidadProducto-=s.Cantidad;
            }
                sql = ("insert into producto_salida values(null," + idLote + ",'" + s.Fecha + "'," +
                "" + s.Cantidad + "," + s.CantidadLote + "," + s.CantidadProducto
                + ",'" + s.Destino + "','" + s.Observaciones + "')");

            

            conexionDB.Insertar(sql);

            // actualizar stock de  producto y lote

            String actualizarLote = "update lote set stock = " + s.CantidadLote+ " where id_lote = " + idLote;
            String actualizarProducto = "update producto set stock = " + s.CantidadProducto + " where id_producto = " + s.Lote.IdProducto;
            conexionDB.Update(actualizarLote);
            conexionDB.Update(actualizarProducto);
        }
        /// <summary>
        /// Metodo para insertar una entrada
        /// </summary>
        /// <param name="e">The e.</param>
        public void InsertarEntrada( Entrada e)
        {
            // obtener id del lote
            String BuscarIdLote = "select id_lote from lote l where l.lote = '" + e.Lote.CodigoLote + "' group by id_lote"; 
            MySqlDataReader dataReader = conexionDB.Select(BuscarIdLote);

            string idLote = null;
            while (dataReader.Read())
            {
                idLote = dataReader.GetString(0);
            }
               
            String sql = ("insert into producto_entrada values(null,"+idLote+",'"+e.Fecha+"'," +
                "'"+e.Albaran+"','"+e.Proveedor+"',"+e.Cantidad+","+e.CantidadLote+","+e.CantidadProducto+",'"+e.Observaciones+"')");
            
            
            conexionDB.Insertar(sql);

            // actualizar stock de  producto y lote

            String actualizarLote = "update lote set stock = "+e.CantidadLote+" where id_lote = "+idLote;
            String actualizarProducto = "update producto set stock = " + e.CantidadProducto + " where id_producto = " + e.Lote.IdProducto;
            conexionDB.Update(actualizarLote);
            conexionDB.Update(actualizarProducto);
        }
        public void LiquidarLotes(Lote l) { 
        
        }
        /// <summary>
        /// metodo para obtener el stock del lote.
        /// </summary>
        /// <param name="l">The l.</param>
        /// <returns></returns>
        public Double ObtenerStockLote(Lote l)
        {
            String sql = "select l.stock from lote l where l.lote = '"+l.CodigoLote+"'";
            
            String stock = null;
            MySqlDataReader dataReader = conexionDB.Select(sql);
            while (dataReader.Read())
            {
                stock = dataReader.GetString(0);
            }            

            if (stock == null) {
                return -1;
            }
       
            return Double.Parse(stock);
        }
        public MySqlDataReader CargarLotes(String id) {

            string sql = "Select l.id_lote,l.lote from lote l where id_producto = "+id;

            MySqlDataReader dataReader = conexionDB.Select(sql);    
            return dataReader;
        }
>>>>>>> 1ff12ff7f6e04265dbbdbdfb7305aae15af63291
    }
}
