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

<<<<<<< HEAD
=======
        /// <summary>
        /// Constructor vacio de la clase ConexionDB <see cref="ConexionDB"/> class.
        /// </summary>
>>>>>>> 1ff12ff7f6e04265dbbdbdfb7305aae15af63291
        public ConexionDB()
        {

        }
<<<<<<< HEAD
=======

        /// <summary>
        /// Metodo crearConexion, donde creamos la conexion y la abrimos
        /// </summary>
>>>>>>> 1ff12ff7f6e04265dbbdbdfb7305aae15af63291
        public void crearConexion()
        {
            conn = new MySqlConnection(Properties.Settings.Default.ConexionDB);
            conn.Open();
        }
<<<<<<< HEAD
=======

        /// <summary>
        /// Metodo cerrarConexion, donde cerramos la conexion
        /// </summary>
>>>>>>> 1ff12ff7f6e04265dbbdbdfb7305aae15af63291
        public void cerrarConexion()
        {
            conn.Close();
        }
<<<<<<< HEAD
=======

        /// <summary>
        /// Metodo Select, donde abrimos la conexion y le
        /// pasamos como parametro la consulta que 
        /// queremos realizar
        /// </summary>
        /// <param name="sentenciaSQL">Consulta a realizar</param>
        /// <returns>Retorna el dataReader</returns>
>>>>>>> 1ff12ff7f6e04265dbbdbdfb7305aae15af63291
        public MySqlDataReader Select(string sentenciaSQL)
        {
            crearConexion();
            MySqlCommand cmd = new MySqlCommand(sentenciaSQL, conn);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            return dataReader;
        }
<<<<<<< HEAD
=======

        /// <summary>
        /// Metodo Insertar, donde abrimos la conexion y pasamos 
        /// como parametro una inserccion
        /// </summary>
        /// <param name="sentenciaSQL">Inserccion a realizar</param>
>>>>>>> 1ff12ff7f6e04265dbbdbdfb7305aae15af63291
        public void Insertar(String sentenciaSQL)
        {
            crearConexion();
            MySqlCommand cmd = new MySqlCommand(sentenciaSQL, conn);
            cmd.ExecuteNonQuery();
        }
<<<<<<< HEAD
=======

        /// <summary>
        /// Metodo InsertarProductos, donde abrimos la conexion
        /// para insertar los datos de un nuevo producto
        /// </summary>
        /// <param name="sentenciaSQL">Inserccion a realizar</param>
        /// <param name="image">Imagen del producto</param>
>>>>>>> 1ff12ff7f6e04265dbbdbdfb7305aae15af63291
        public void InsertarProducto(String sentenciaSQL, byte[] image)
        {
            crearConexion();
            MySqlCommand cmd = new MySqlCommand(sentenciaSQL, conn);
            cmd.Parameters.AddWithValue("@pic", image);
            cmd.ExecuteNonQuery();
        }
<<<<<<< HEAD
=======

        /// <summary>
        /// Metodo Update, donde abrimos la conexion y pasamos
        /// como parametro la consulta para actualizar los datos indicados
        /// </summary>
        /// <param name="sentenciaSQL">Actualizacion a realizar</param>
>>>>>>> 1ff12ff7f6e04265dbbdbdfb7305aae15af63291
        public void Update(String sentenciaSQL)
        {
            crearConexion();
            MySqlCommand cmd = new MySqlCommand(sentenciaSQL, conn);
            cmd.ExecuteNonQuery();
        }
<<<<<<< HEAD
=======

        /// <summary>
        /// Metodo InsertarProductos, donde abrimos la conexion y 
        /// pasamos como parametro la inserccion de los productos
        /// </summary>
        /// <param name="sentenciaSQL">Inserccion a realizar</param>
>>>>>>> 1ff12ff7f6e04265dbbdbdfb7305aae15af63291
        public void InsertarProducto(string sentenciaSQL)
        {
            crearConexion();
            MySqlCommand cmd = new MySqlCommand(sentenciaSQL, conn);
            cmd.ExecuteNonQuery();
        }
<<<<<<< HEAD
=======

        /// <summary>
        /// Metodo aBuscarLotesPorProducto, donde abrimos la conexion
        /// y pasamos como parametro la consulta para la busqueda de
        /// los lotes
        /// </summary>
        /// <param name="sentenciaSQL">Consulta a realizar</param>
        /// <returns>Retorna el dataReader</returns>
>>>>>>> 1ff12ff7f6e04265dbbdbdfb7305aae15af63291
        public MySqlDataReader BuscarLotesPorProducto(string sentenciaSQL) {
            crearConexion();
            MySqlCommand cmd = new MySqlCommand(sentenciaSQL, conn);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            return dataReader;
        }
    }
}
