using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace PaySoft.Modulos.Mesas_Salones
{
    public partial class Configurar_Mesas : Form
    {

        private int idSalon;
        private string estadoSalon;

        public Configurar_Mesas()
        {
            InitializeComponent();
        }

        private void Configurar_Mesas_Load(object sender, EventArgs e)
        {
            panelBienvenida.Dock = DockStyle.Fill;
            dibujarSalon();
        }

        private void dibujarSalon()
        {
            try
            {
                panelSalones.Controls.Clear();
                Conexion.ConexionMaster.abrirConexion();
                SqlCommand cmd = new SqlCommand("SP_MostrarSalon", Conexion.ConexionMaster.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Buscar", txtBuscador.Text);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Button b = new Button();
                    Panel panelBtn = new Panel();
                    b.Text = reader["Nombre"].ToString();
                    b.Name = reader["ID_Salon"].ToString();
                    b.Dock = DockStyle.Fill;
                    b.BackColor = Color.Transparent;
                    b.Font = new System.Drawing.Font("Microsoft Sans Serif", 12);
                    b.FlatStyle = FlatStyle.Flat;
                    b.FlatAppearance.BorderSize = 0;
                    b.FlatAppearance.MouseDownBackColor = Color.FromArgb(64, 64, 64);
                    b.FlatAppearance.MouseOverBackColor = Color.FromArgb(43, 43, 43);
                    b.TextAlign = ContentAlignment.MiddleCenter;
                    b.Tag = reader["Estado"].ToString();


                    panelBtn.Size = new System.Drawing.Size(250, 50);
                    panelBtn.Name = reader["ID_Salon"].ToString();
                    string estado;
                    estado = reader["Estado"].ToString();
                    if (estado.Equals("Eliminado"))
                    {
                        b.Text = reader["Nombre"].ToString() + " - Eliminado";
                        b.ForeColor = Color.FromArgb(213, 63, 67);
                    }
                    else
                    {
                        b.ForeColor = Color.WhiteSmoke;
                    }
                    panelBtn.Controls.Add(b);
                    panelSalones.Controls.Add(panelBtn);
                    b.Click += new EventHandler(ev_salon_button);

                }
                Conexion.ConexionMaster.cerrarConexion();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                Conexion.ConexionMaster.cerrarConexion();
            }
        }

        private void dibujarMesas()
        {
            try
            {
                panelMesas.Controls.Clear();
                Conexion.ConexionMaster.abrirConexion();
                SqlCommand cmd = new SqlCommand("SP_MostrarMesasSalon", Conexion.ConexionMaster.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID_Salon", idSalon);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Button b = new Button();
                    Panel p = new Panel();
                    int alto = Convert.ToInt32(reader["y"].ToString());
                    int ancho = Convert.ToInt32(reader["x"].ToString());
                    int tamano_letra = Convert.ToInt32(reader["tamano_letra"].ToString());

                    Point tamanio = new Point(ancho, alto);
                    p.BackgroundImage = Properties.Resources.mesa_vacia; 
                    p.BackgroundImageLayout = ImageLayout.Zoom;
                    p.Cursor = Cursors.Hand;
                    p.Tag = reader["ID_Mesa"].ToString();
                    p.Size = new System.Drawing.Size(tamanio);

                    b.Text = reader["Nombre"].ToString();
                    b.Name = reader["ID_Mesa"].ToString();

                    if (b.Text != "NULO")
                    {
                        b.Size = new System.Drawing.Size(tamanio);
                        b.BackColor = Color.FromArgb(5, 179, 90);
                        b.Font = new System.Drawing.Font("Microsoft Sans Serif", tamano_letra);
                        b.FlatStyle = FlatStyle.Flat;
                        b.FlatAppearance.BorderSize = 0;
                        b.ForeColor = Color.WhiteSmoke;
                        panelMesas.Controls.Add(b);
                    }
                    else
                    {
                        panelMesas.Controls.Add(p);
                    }

                   
                }
                Conexion.ConexionMaster.cerrarConexion();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace);
                Conexion.ConexionMaster.cerrarConexion();
            }
        }

        private void ev_salon_button(object sender, EventArgs e)
        {
            panelBienvenida.Visible = false;
            panelBienvenida.Dock = DockStyle.None;
            panelMesas.Visible = true;
            panelMesas.Dock = DockStyle.Fill;
            panelMesas.BackColor = Color.FromArgb(31,31,31);
            idSalon = Convert.ToInt32(((Button)sender).Name);
            estadoSalon = Convert.ToString(((Button)sender).Tag);
            dibujarMesas();
            foreach (Panel p in panelSalones.Controls)
            {
                if (p is Panel)
                {
                    foreach (Button b in p.Controls)
                    {
                        if (b is Button)
                        {
                            b.BackColor = Color.Transparent;
                            break;
                        }
                    }
                }
            }

            string nombre = Convert.ToString(((Button)sender).Name);

            foreach (Panel p in panelSalones.Controls)
            {
                if (p is Panel)
                {
                    foreach (Button b in p.Controls)
                    {
                        if (b is Button)
                        {
                            if (b.Name == nombre)
                            {
                                b.BackColor = Color.DarkOrange;
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void btnAgregarSalon_Click(object sender, EventArgs e)
        {
            Modulos.Mesas_Salones.Configurar_Salones frm = new Configurar_Salones();
            frm.FormClosed += new FormClosedEventHandler(frm_FormClosed);
            frm.ShowDialog();
        }

        public void frm_FormClosed(Object sender, FormClosedEventArgs e)
        {
            dibujarSalon();
        }
    }
}
