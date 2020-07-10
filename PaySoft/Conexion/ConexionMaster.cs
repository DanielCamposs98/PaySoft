using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PaySoft.Conexion
{
    class ConexionMaster
    {
        public static string conn = Convert.ToString(Desencryptacion.checkServer());
    }
}
