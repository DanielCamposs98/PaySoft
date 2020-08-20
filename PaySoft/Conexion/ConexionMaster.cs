using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PaySoft.Conexion
{
    class ConexionMaster
    {
        public static string conn = Convert.ToString(Desencryptacion.checkServer());
        public static SqlConnection conectar = new SqlConnection(conn);
 
        public static void abrirConexion()
        {
            if(conectar.State == ConnectionState.Closed)
            {
                conectar.Open();
            }
        }

        public static void cerrarConexion()
        {
            if(conectar.State == ConnectionState.Open)
            {
                conectar.Close();
            }
        }
    }
}
