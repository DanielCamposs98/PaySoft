using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PaySoft.Modulos.Mesas_Salones
{
    public partial class Configurar_Salones : Form
    {

        private int idSalon;
        public Configurar_Salones()
        {
            InitializeComponent();
        }

        private void Configurar_Salones_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            txtSalon.Focus();

        }

        private void insertar_mesas_vacias()
        {
            for (int i = 1; i <= 80; i++)
            {
                try
                {
                    Conexion.ConexionMaster.abrirConexion();
                    SqlCommand cmd = new SqlCommand("SP_InsertarMesa",Conexion.ConexionMaster.conectar);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombre", "NULO");
                    cmd.Parameters.AddWithValue("@Salon", idSalon);
                    cmd.ExecuteNonQuery();
                    Conexion.ConexionMaster.cerrarConexion();
                }
                catch (Exception ex)
                {
                    Conexion.ConexionMaster.cerrarConexion();
                    MessageBox.Show(ex.StackTrace);
                }
            }
        }

        private void mostrar_id_salon_recien_ingresado()
        {
            SqlCommand com = new SqlCommand("SP_MostrarIdSalonRegistrado", Conexion.ConexionMaster.conectar);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Salon", txtSalon.Text);
            try
            {
                Conexion.ConexionMaster.abrirConexion();
                idSalon = Convert.ToInt32(com.ExecuteScalar());
                Conexion.ConexionMaster.cerrarConexion();
            }
            catch (Exception ex)
            {
                Conexion.ConexionMaster.cerrarConexion();
                MessageBox.Show(ex.StackTrace);
            }
        }

        private void insertarSalon()
        {
            try
            {

                Conexion.ConexionMaster.abrirConexion();
                SqlCommand cmd = new SqlCommand("SP_InsertarSalon", Conexion.ConexionMaster.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Salon", txtSalon.Text);
                cmd.ExecuteNonQuery();
                mostrar_id_salon_recien_ingresado();
                insertar_mesas_vacias();
                Conexion.ConexionMaster.cerrarConexion();
                Close();
            }
            catch (Exception e)
            {
                Conexion.ConexionMaster.cerrarConexion();
                MessageBox.Show(e.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            insertarSalon();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
