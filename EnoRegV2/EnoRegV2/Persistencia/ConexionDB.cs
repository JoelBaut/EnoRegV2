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

        public ConexionDB()
        {

        }
        public void crearConexion()
        {
            conn = new MySqlConnection(Properties.Settings.Default.ConexionDB);
            conn.Open();
        }
        public void cerrarConexion()
        {
            conn.Close();
        }
        public MySqlDataReader Select(string sentenciaSQL)
        {
            crearConexion();
            MySqlCommand cmd = new MySqlCommand(sentenciaSQL, conn);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            return dataReader;
        }
        public void Insertar(String sentenciaSQL)
        {
            crearConexion();
            MySqlCommand cmd = new MySqlCommand(sentenciaSQL, conn);
            cmd.ExecuteNonQuery();
        }
        public void InsertarProducto(String sentenciaSQL, byte[] image)
        {
            crearConexion();
            MySqlCommand cmd = new MySqlCommand(sentenciaSQL, conn);
            cmd.Parameters.AddWithValue("@pic", image);
            cmd.ExecuteNonQuery();
        }
        public void Update(String sentenciaSQL)
        {
            crearConexion();
            MySqlCommand cmd = new MySqlCommand(sentenciaSQL, conn);
            cmd.ExecuteNonQuery();
        }
        internal void InsertarProducto(string sentenciaSQL)
        {
            crearConexion();
            MySqlCommand cmd = new MySqlCommand(sentenciaSQL, conn);
            cmd.ExecuteNonQuery();
        }
    }
}
