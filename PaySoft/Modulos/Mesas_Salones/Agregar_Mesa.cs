using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaySoft.Modulos.Mesas_Salones
{
    public partial class Agregar_Mesa : Form
    {
        public Agregar_Mesa()
        {
            InitializeComponent();
        }

        private void Agregar_Mesa_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            txtMesa.Text = Configurar_Mesas.nombreMesa;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(txtMesa.Text != "")
            {
                editar_Mesa();
            }
        }

        private void editar_Mesa()
        {
            try
            {
                Conexion.ConexionMaster.abrirConexion();
                SqlCommand cmd = new SqlCommand("SP_EditarMesa",Conexion.ConexionMaster.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre", txtMesa.Text);
                cmd.Parameters.AddWithValue("@ID_Mesa", Configurar_Mesas.idMesa);
                cmd.ExecuteNonQuery();
                Conexion.ConexionMaster.cerrarConexion();
                Close();
            }catch(Exception e)
            {
                MessageBox.Show(e.Message);
                Conexion.ConexionMaster.cerrarConexion();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
