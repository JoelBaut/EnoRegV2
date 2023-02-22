using EnoregV2.Persistencia;

using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using Mysqlx.Cursor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EnoregV2.Dominio
{

    /// <summary>
    /// Clase que se encarga de enlazar la Base de datos con las vistas y que hace de intermedio para ejecutar las consultas
    /// </summary>
    public class ProductoDAO
    {
        //declaracion de variables
        ConexionDB conexionDB = new ConexionDB();
        String registroFiltros = "Entradas/Salidas";
        String param = "";
        String paramFechaEntrada = "";
        String paramFechaSalida = "";
        String paramDestino = "";

        //getters & setters
        public string RegistroFiltros { get => registroFiltros; set => registroFiltros = value; }
        public string Param { get => param; set => param = value; }
        public string ParamFechaEntrada { get => paramFechaEntrada; set => paramFechaEntrada = value; }
        public string ParamFechaSalida { get => paramFechaSalida; set => paramFechaSalida = value; }
        public string ParamDestino { get => paramDestino; set => paramDestino = value; }

        /// <summary>
        /// Constructor vacio de la clase ProductoDAO <see cref="ProductoDAO"/> class.
        /// </summary>
        public ProductoDAO()
        {
           
        }

        /// <summary>
        /// Metodo cerrarConexion, el cual cerrara la conexion
        /// con la base de datos
        /// </summary>
        public void cerrarConexion()
        {
            conexionDB.cerrarConexion();
        }

        /// <summary>
        /// Añadido el stock_lote y stock_cantidad
        /// </summary>
        /// <returns>devuelve un MySqlDataReader con todos los datos</returns>
        public MySqlDataReader CargarTodo()
        {
            string sql = "select fecha_entrada Fecha, nombre Nombre, proveedor Proveedor, lote Lote, fecha_caducidad Caducidad, albaran Albaran,REPLACE(REPLACE(REPLACE(FORMAT(cantidad,3),',','|'),'.',','),'|','.') Entrada,'-' Salida, REPLACE(REPLACE(REPLACE(FORMAT(stock_lote,3),',','|'),'.',','),'|','.') StockLote,REPLACE(REPLACE(REPLACE(FORMAT(stock_producto,3),',','|'),'.',','),'|','.') CantidadTotal, '-' Destino, observaciones Observaciones" +
                " from producto_entrada, producto, lote" +
                " where lote.id_producto = producto.id_producto and producto_entrada.id_lote = lote.id_lote" +
                " Union" +
                " select fecha_salida Fecha, nombre Nombre, '-' Proveedor, lote Lote, '-'Caducidad, '-'Albaran,'-'Entrada,REPLACE(REPLACE(REPLACE(FORMAT(cantidad, 3), ',', '|'), '.', ','), '|', '.') Salida, REPLACE(REPLACE(REPLACE(FORMAT(stock_lote, 3), ',', '|'), '.', ','), '|', '.') StockLote,REPLACE(REPLACE(REPLACE(FORMAT(stock_producto, 3), ',', '|'), '.', ','), '|', '.') CantidadTotal, destino Destino, observaciones Observaciones" +
                " from producto_salida, producto,lote " +
                "where lote.id_producto = producto.id_producto and producto_salida.id_lote = lote.id_lote" +
                " order by fecha DESC;";

            return conexionDB.Select(sql);
        }
        /// <summary>
        /// Metodo para obtener las unidades introducidas en la BD
        /// </summary>
        /// <param name="nombreProducto"></param>
        /// <returns>devuelve un MySqlDataReader con todos los datos</returns>
        public MySqlDataReader ObtenerUnidad(string nombreProducto)
        {
            string sql = "select unidad from producto where nombre='" + nombreProducto + "'";
            return conexionDB.Select(sql);
        }
        /// <summary>
        /// METODO SIN USO POR EL MOMENTO
        /// </summary>
        /// <param name="nombreProducto">The nombre producto.</param>
        /// <returns></returns>
        public Decimal ObtenerStock(String nombreProducto)
        {
            Decimal stock = -1;
            string sql = "select stock from producto where nombre='" + nombreProducto + "';";
            MySqlDataReader rd = conexionDB.Select(sql);
            while (rd.Read())
            {
                stock = rd.GetDecimal(0);
            }
            return stock;
        }
        /// <summary>
        /// Metodo comprobado, Se modifica la insercion a insertar mediante un objeto de tipo producto. Si el producto tiene una imagen
        /// se inserta con ella si no no.
        /// </summary>
        /// <param name="p">Parametro con un objeto de tipo Producto</param>       
        public void InsertarProducto(Producto p)
        {
            string sql = "INSERT INTO `producto`(`nombre`, `unidad`, `imagen`) VALUES ('" + p.Nombre + "', '" + p.Unidad + "',@pic);";           
                conexionDB.InsertarProducto(sql, p.Imagen);            
        }
        internal void InsertarProducto(string nombre, string unidad)
        {

            string sql = "INSERT INTO `producto`(`nombre`, `unidad`) VALUES ('" + nombre + "', '" + unidad + "');";

            conexionDB.InsertarProducto(sql);
        }
        /// <summary>
        /// Metodo para Cargar productos a la hora de tener una lista de productos
        /// </summary>
        /// <returns>devuelve un MySqlDataReader con todos los datos</returns>
        public MySqlDataReader CargarListaProductos()
        {
            string sql = "Select id_producto 'Id Producto',nombre 'Nombre',unidad 'Unidad',stock 'Stock de Producto' from producto";
            return conexionDB.Select(sql);

        }
        /// <summary>
        /// Metodo para cargar los nombre de producto
        /// </summary>
        /// <param name="p"></param>
        /// <returns>devuelve un MySqlDataReader con todos los datos</returns>
        public MySqlDataReader CargarNombres()
        {
            string sql = "Select id,nombre,unidad,stock from producto";
            return conexionDB.Select(sql);

        }
        public MySqlDataReader CargarUnidad()
        {
            string sql = "Select unidad from producto";
            return conexionDB.Select(sql);

        }
        public MySqlDataReader CargarProductos(Producto p)
        {
            string sql = "Select nombre from producto where nombre='" + p.Nombre + "'";
            return conexionDB.Select(sql);

        }
        public MySqlDataReader CargarLotes(Producto p)
        {
            string sql = "Select id_lote ID,id_producto 'ID Producto',lote 'Numero Lote',stock 'Stock Lote' from lote where id_producto='" + p.Id + "'";
            return conexionDB.Select(sql);

        }
        public Boolean CompruebaProductos(String nombre)
        {
            Boolean existe = false;
            string sql = "Select nombre from producto where nombre='" + nombre + "'";
            MySqlDataReader datos = conexionDB.Select(sql);
            if (datos.Read())
            {
                existe = true;
            }
            else
            {
                existe = false;
            }
            return existe;

        }

        /// <summary>
        /// Metodo para cargar la imagen de un producto
        /// </summary>
        /// <param name="p"></param>
        /// <returns>devuelve un MySqlDataReader con todos los datos</returns>
        public MySqlDataReader CargarImagen(Producto p)
        {
            string sql = "Select imagen from producto where nombre = '" + p.Nombre + "'";
            return conexionDB.Select(sql);
        }

        /// <summary>
        /// PENDIENTE DE HACER FILTROS
        /// </summary>
        /// <param name="registro"></param>
        /// <param name="fechaInicial"></param>
        /// <param name="FechaFinal"></param>
        /// <param name="Producto"></param>
        /// <param name="Destino"></param>
        /// <param name="Lote"></param>
        /// <returns></returns>
        internal IDataReader CargarFiltro(string registro, string fechaInicial, string FechaFinal, string Producto, string Destino, string Lote)
        {
            string sql;
            string consultaStringProducto;
            string consultaStringDestino;
            string consultaStringFechaSalida;
            string consultaStringFechaEntrada;
            string consultaStringLote;
            if (Producto.Equals("<Cualquiera>"))
            {
                consultaStringProducto = "";
            }
            else
            {
                consultaStringProducto = "and producto.id_producto = (SELECT id_producto from producto WHERE nombre = '" + Producto + "') ";
            }

            if (Destino.Equals(""))
            {
                consultaStringDestino = "";
            }
            else
            {
                consultaStringDestino = "and `destino` =  '" + Destino + "' ";
            }
            if (Lote.Equals(""))
            {
                consultaStringLote = "";
            }
            else
            {
                consultaStringLote = "and `lote` =  '" + Lote + "' ";
            }

            consultaStringFechaEntrada = "and `fecha_entrada` between '" + fechaInicial + "' and '" + FechaFinal + "' ";
            consultaStringFechaSalida = "and `fecha_salida` between '" + fechaInicial + "' and '" + FechaFinal + "' ";


            if (registro.Equals("Entradas/Salidas"))
            {
                sql = "select fecha_entrada Fecha, nombre Nombre, proveedor Proveedor, lote Lote, fecha_caducidad Caducidad, albaran Albaran,REPLACE(REPLACE(REPLACE(FORMAT(cantidad,3),',','|'),'.',','),'|','.') Entrada,'-' Salida, REPLACE(REPLACE(REPLACE(FORMAT(stock_lote,3),',','|'),'.',','),'|','.') StockLote,REPLACE(REPLACE(REPLACE(FORMAT(stock_producto,3),',','|'),'.',','),'|','.') CantidadTotal, '-' Destino, observaciones Observaciones" +
                   " from producto_entrada, producto, lote" +
                   " where lote.id_producto = producto.id_producto and producto_entrada.id_lote = lote.id_lote " + consultaStringProducto + consultaStringFechaEntrada + consultaStringLote +
                   " Union" +
                   " select fecha_salida Fecha, nombre Nombre, '-' Proveedor, lote Lote, '-'Caducidad, '-'Albaran,'-'Entrada,REPLACE(REPLACE(REPLACE(FORMAT(cantidad, 3), ',', '|'), '.', ','), '|', '.') Salida, REPLACE(REPLACE(REPLACE(FORMAT(stock_lote, 3), ',', '|'), '.', ','), '|', '.') StockLote,REPLACE(REPLACE(REPLACE(FORMAT(stock_producto, 3), ',', '|'), '.', ','), '|', '.') CantidadTotal, destino Destino, observaciones Observaciones" +
                   " from producto_salida, producto, lote" +
                   " where lote.id_producto = producto.id_producto and producto_salida.id_lote = lote.id_lote " + consultaStringProducto + consultaStringFechaSalida + consultaStringLote +
                   " order by fecha DESC;";

            }
            else if (registro.Equals("Entradas"))
            {
                sql = "select fecha_entrada Fecha, nombre Nombre, proveedor Proveedor, lote Lote, fecha_caducidad Caducidad, albaran Albaran,REPLACE(REPLACE(REPLACE(FORMAT(cantidad,3),',','|'),'.',','),'|','.') Entrada,'-' Salida, REPLACE(REPLACE(REPLACE(FORMAT(stock_lote,3),',','|'),'.',','),'|','.') StockLote,REPLACE(REPLACE(REPLACE(FORMAT(stock_producto,3),',','|'),'.',','),'|','.') CantidadTotal, '-' Destino, observaciones Observaciones" +
                   " from producto_entrada, producto, lote" +
                   " where lote.id_producto = producto.id_producto and producto_entrada.id_lote = lote.id_lote " + consultaStringProducto + consultaStringFechaEntrada + consultaStringLote +
                   " order by fecha DESC;";
            }
            else
            {
                sql = "select fecha_salida Fecha, nombre Nombre, '-' Proveedor, lote Lote, '-'Caducidad, '-'Albaran,'-'Entrada,REPLACE(REPLACE(REPLACE(FORMAT(cantidad, 3), ',', '|'), '.', ','), '|', '.') Salida, REPLACE(REPLACE(REPLACE(FORMAT(stock_lote, 3), ',', '|'), '.', ','), '|', '.') StockLote,REPLACE(REPLACE(REPLACE(FORMAT(stock_producto, 3), ',', '|'), '.', ','), '|', '.') CantidadTotal, destino Destino, observaciones Observaciones" +
                   " from producto_salida, producto,lote" +
                   " where lote.id_producto = producto.id_producto and producto_salida.id_lote = lote.id_lote " + consultaStringProducto+ consultaStringDestino + consultaStringFechaSalida + consultaStringDestino + consultaStringLote +
                   " order by fecha DESC;";
            }

            paramFechaEntrada = consultaStringFechaEntrada;
            paramFechaSalida = consultaStringFechaSalida;
            registroFiltros = registro;
            param = consultaStringProducto + consultaStringLote;
            paramDestino = consultaStringDestino;
            return conexionDB.Select(sql);
        }
        /// <summary>
        /// Metodo para Cargar productos a la hora de tener una lista de productos
        /// </summary>
        /// <returns>devuelve un MySqlDataReader con todos los datos</returns>
        public MySqlDataReader Cargarproductos()
        {
            string sql = "Select id_producto,nombre from producto";
            return conexionDB.Select(sql);

        }        
        /// <summary>
        /// metodo para obtener el stock de los productos
        /// </summary>
        /// <param name="nombreProducto">The nombre producto.</param>
        /// <returns></returns>
        public Double ObtenerStockProducto(String nombreProducto) {
            String sql = "select p.stock from producto p where p.nombre = '" + nombreProducto + "'";

            String stock = null;
            MySqlDataReader dataReader = conexionDB.Select(sql);
            while (dataReader.Read())
            {
                stock = dataReader.GetString(0);
            }
            return Double.Parse(stock);
        }
    }
}