using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PaySoft.Modulos.Productos
{
    public partial class Productos_Rest : Form
    {
        public Productos_Rest()
        {
            InitializeComponent();
        }

        private void Registro_Products_Load(object sender, EventArgs e)
        {
            dibujarGrupos();
        }

        private void dibujarGrupos()
        {
            try
            {
                panelGrupos.Controls.Clear();
                Conexion.ConexionMaster.abrirConexion();
                SqlCommand cmd = new SqlCommand("SELECT * FROM GRUPO_PRODUCTO", Conexion.ConexionMaster.conectar);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Label lbl = new Label();
                    Panel p1 = new Panel();
                    Panel p2 = new Panel();
                    PictureBox pb = new PictureBox();

                    MenuStrip menu = new MenuStrip();
                    ToolStripMenuItem tsPrincipal = new ToolStripMenuItem();
                    ToolStripMenuItem tsEditar = new ToolStripMenuItem();
                    ToolStripMenuItem tsEliminar = new ToolStripMenuItem();
                    ToolStripMenuItem tsRestaurar = new ToolStripMenuItem();

                    lbl.Text = reader["Nombre"].ToString();
                    lbl.Name = reader["ID_Grupo"].ToString();
                    lbl.Size = new System.Drawing.Size(139, 25);
                    lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10);
                    lbl.BackColor = Color.Transparent;
                    lbl.ForeColor = Color.WhiteSmoke;
                    lbl.Dock = DockStyle.Fill;
                    lbl.TextAlign = ContentAlignment.MiddleCenter;
                    lbl.Cursor = Cursors.Hand;

                    p1.Size = new System.Drawing.Size(140, 133);
                    p1.BorderStyle = BorderStyle.FixedSingle;
                    p1.Dock = DockStyle.Bottom;
                    p1.BackColor = Color.FromArgb(43, 43, 43);
                    p1.Name = reader["ID_Grupo"].ToString();

                    p2.Size = new System.Drawing.Size(140, 25);
                    p2.Dock = DockStyle.Top;
                    p2.BackColor = Color.Transparent;
                    p2.BorderStyle = BorderStyle.None;

                    pb.Size = new System.Drawing.Size(140, 76);
                    pb.Dock = DockStyle.Top;
                    pb.BackgroundImage = null;
                    //byte[] bi = (byte[])reader["Icono"];
                    //MemoryStream ms = new MemoryStream(bi);
                    //pb.Image = Image.FromStream(ms);
                    //pb.SizeMode = PictureBoxSizeMode.Zoom;
                    //pb.Cursor = Cursors.Hand;
                    pb.Tag = reader["ID_Grupo"].ToString();

                    menu.Size = new System.Drawing.Size(25, 25);
                    menu.BackColor = Color.Transparent;
                    menu.AutoSize = false;
                    menu.Dock = DockStyle.Top;
                    menu.Name = reader["ID_Grupo"].ToString();
                    

                    tsPrincipal.Image = Properties.Resources.menuCajas_claro;
                    tsPrincipal.BackColor = Color.Transparent;

                    tsEditar.Text = "Editar";
                    tsEditar.Name = reader["Nombre"].ToString();
                    tsEditar.Tag = reader["ID_Grupo"].ToString();

                    tsEliminar.Text = "Eliminar";
                    tsEliminar.Tag = reader["ID_Grupo"].ToString();

                    tsRestaurar.Text = "Restaurar";
                    tsRestaurar.Name= reader["ID_Grupo"].ToString();

                    menu.Items.Add(tsPrincipal);
                    tsPrincipal.DropDownItems.Add(tsEditar);
                    tsPrincipal.DropDownItems.Add(tsEliminar);
                    tsPrincipal.DropDownItems.Add(tsRestaurar);
                   

                    p2.Controls.Add(menu);

                    p1.Controls.Add(p2);

                    if (reader["Estado_Icono"].ToString() != "VACIO")
                    {
                        p1.Controls.Add(pb);
                    }
                    else
                    {
                        p1.Controls.Add(lbl);
                    }
                    
                    lbl.BringToFront();
                    p2.SendToBack();
                    panelGrupos.Controls.Add(p1);
                }
                Conexion.ConexionMaster.cerrarConexion();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace);
                Conexion.ConexionMaster.cerrarConexion();
            }
        }

    }
}
