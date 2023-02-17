using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace EnoregV2.Persistencia
{
    internal class ConexionDB
    {
        MySqlConnection conn = null;

        /// <summary>
        /// Constructor vacio de la clase ConexionDB <see cref="ConexionDB"/> class.
        /// </summary>
        public ConexionDB()
        {

        }

        /// <summary>
        /// Metodo crearConexion, donde creamos la conexion y la abrimos
        /// </summary>
        public void crearConexion()
        {
            conn = new MySqlConnection(Properties.Settings.Default.ConexionDB);
            conn.Open();
        }

        /// <summary>
        /// Metodo cerrarConexion, donde cerramos la conexion
        /// </summary>
        public void cerrarConexion()
        {
            conn.Close();
        }

        /// <summary>
        /// Metodo Select, donde abrimos la conexion y le
        /// pasamos como parametro la consulta que 
        /// queremos realizar
        /// </summary>
        /// <param name="sentenciaSQL">Consulta a realizar</param>
        /// <returns>Retorna el dataReader</returns>
        public MySqlDataReader Select(string sentenciaSQL)
        {
            crearConexion();
            MySqlCommand cmd = new MySqlCommand(sentenciaSQL, conn);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            return dataReader;
        }

        /// <summary>
        /// Metodo Insertar, donde abrimos la conexion y pasamos 
        /// como parametro una inserccion
        /// </summary>
        /// <param name="sentenciaSQL">Inserccion a realizar</param>
        public void Insertar(String sentenciaSQL)
        {
            crearConexion();
            MySqlCommand cmd = new MySqlCommand(sentenciaSQL, conn);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Metodo InsertarProductos, donde abrimos la conexion
        /// para insertar los datos de un nuevo producto
        /// </summary>
        /// <param name="sentenciaSQL">Inserccion a realizar</param>
        /// <param name="image">Imagen del producto</param>
        public void InsertarProducto(String sentenciaSQL, byte[] image)
        {
            crearConexion();
            MySqlCommand cmd = new MySqlCommand(sentenciaSQL, conn);
            cmd.Parameters.AddWithValue("@pic", image);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Metodo Update, donde abrimos la conexion y pasamos
        /// como parametro la consulta para actualizar los datos indicados
        /// </summary>
        /// <param name="sentenciaSQL">Actualizacion a realizar</param>
        public void Update(String sentenciaSQL)
        {
            crearConexion();
            MySqlCommand cmd = new MySqlCommand(sentenciaSQL, conn);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Metodo InsertarProductos, donde abrimos la conexion y 
        /// pasamos como parametro la inserccion de los productos
        /// </summary>
        /// <param name="sentenciaSQL">Inserccion a realizar</param>
        public void InsertarProducto(string sentenciaSQL)
        {
            crearConexion();
            MySqlCommand cmd = new MySqlCommand(sentenciaSQL, conn);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Metodo aBuscarLotesPorProducto, donde abrimos la conexion
        /// y pasamos como parametro la consulta para la busqueda de
        /// los lotes
        /// </summary>
        /// <param name="sentenciaSQL">Consulta a realizar</param>
        /// <returns>Retorna el dataReader</returns>
        public MySqlDataReader BuscarLotesPorProducto(string sentenciaSQL) {
            crearConexion();
            MySqlCommand cmd = new MySqlCommand(sentenciaSQL, conn);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            return dataReader;
        }
    }
}
