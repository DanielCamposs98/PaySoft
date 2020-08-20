using PaySoft.Conexion;
using System;
using System.Windows.Forms;
using System.Xml;
using System.Security.Cryptography;
using System.Data.SqlClient;
namespace PaySoft
{
    public partial class Conexion_Manual : Form
    {
        private AES aes = new AES();
        private String dbcnString;
        private int idTabla;

        public Conexion_Manual()
        {
            InitializeComponent();
        }
        
        public void SaveToXML(Object dbcnString)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("ConnectionString.xml");
            XmlElement root = doc.DocumentElement;
            root.Attributes[0].Value = Convert.ToString(dbcnString);
            XmlTextWriter writer = new XmlTextWriter("ConnectionString.xml", null);
            writer.Formatting = Formatting.Indented;
            doc.Save(writer);
            writer.Close();
        }

        public void ReadFromXML()
        {
            try {
                XmlDocument doc = new XmlDocument();
                doc.Load("ConnectionString.xml");
                XmlElement root = doc.DocumentElement;
                dbcnString = root.Attributes[0].Value;
                txtCnString.Text = (aes.Decrypt(dbcnString, Desencryptacion.appPwdUnique, int.Parse("256")));
            }catch(CryptographicException ex)
            {
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ReadFromXML();
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            comprobarConexion();
        }

        private void comprobarConexion()
        {
            SqlConnection conn = new SqlConnection();
            try
            {
               
                conn.ConnectionString = txtCnString.Text;
                SqlCommand connStr = new SqlCommand("SELECT * FROM SALON",conn);
                conn.Open();
                idTabla = Convert.ToInt32(connStr.ExecuteScalar());                
                SaveToXML(aes.Encrypt(txtCnString.Text, Desencryptacion.appPwdUnique, int.Parse("256")));
                MessageBox.Show("Conexión realizada correctamente", "Conexión", MessageBoxButtons.OK, MessageBoxIcon.Information);
                conn.Close();
                Application.Exit();
               

            }
            catch(Exception e)
            {
                MessageBox.Show("Sin Conexión");
                MessageBox.Show(e.Message);
                conn.Close();
                Application.Exit();
            }
        }
    }
}
