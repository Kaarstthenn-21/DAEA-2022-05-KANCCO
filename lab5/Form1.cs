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

namespace lab5
{
    public partial class Form1 : Form
    {
        SqlConnection conn;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            String str = "Server=DESKTOP-0JFPDPC;DataBase=School; Integrated Security=true;";
            conn = new SqlConnection(str);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            conn.Open();
            String sql = "SELECT * FROM Person";

            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);

            dvgListado.DataSource = dt;
            dvgListado.Refresh();
            conn.Close();
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            conn.Open();
            String sp = "InsertPerson";
            SqlCommand cmd = new SqlCommand(sp, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FirstName", txtNombre);
            cmd.Parameters.AddWithValue("@LastName", txtApellido);
            cmd.Parameters.AddWithValue("@HireDate", txtEnrollmentDate);
            cmd.Parameters.AddWithValue("@EnrollmentDate", txtEnrollmentDate);

            int codigo = Convert.ToInt32(cmd.ExecuteScalar());

            MessageBox.Show("Se ha registrado nueva persona con el codigo"+ codigo);
            conn.Close();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            conn.Open();
            String sp = "UpdatePerson";
            SqlCommand cmd = new SqlCommand(sp, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FirstName", txtNombre);
            cmd.Parameters.AddWithValue("@LastName", txtApellido);
            cmd.Parameters.AddWithValue("@HireDate", txtEnrollmentDate);
            cmd.Parameters.AddWithValue("@EnrollmentDate", txtEnrollmentDate);

            int resultado = cmd.ExecuteNonQuery();

            if(resultado > 0)
            {
                MessageBox.Show("Se ha modificado el registro correctamente");

            }
            conn.Close();

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            conn.Open();
            String sp = "DeletePerson";
            SqlCommand cmd = new SqlCommand(sp, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PersonID", txtCodigo);

            int resultado = cmd.ExecuteNonQuery();

            if(resultado > 0)
            {
                MessageBox.Show("Se ha eliminado el registro correctamente");
            }
            conn.Close();
        }

        private void dvgListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dvgListado_SelectionChanged(object sender, EventArgs e)
        {
           if(dvgListado.SelectedRows.Count > 0)
            {
                txtCodigo.Text = dvgListado.SelectedRows[0].Cells[0].Value.ToString();
                txtNombre.Text = dvgListado.SelectedRows[0].Cells[1].Value.ToString();
                txtApellido.Text = dvgListado.SelectedRows[0].Cells[2].Value.ToString();
                txtHireDate.Text = dvgListado.SelectedRows[0].Cells[3].Value.ToString();
                txtEnrollmentDate.Text = dvgListado.SelectedRows[0].Cells[4].Value.ToString();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            conn.Open();
            if (conn.State == ConnectionState.Open)
            {
                String FirstName = txtNombre.Text;

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "get_buscarpersonanombre";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@FirstName";
                param.SqlDbType = SqlDbType.NVarChar;
                param.Value = FirstName;

                cmd.Parameters.Add(param);

                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                dvgListado.DataSource = dt;
                dvgListado.Refresh();
            }
            else
            {
                MessageBox.Show("La conexion esta cerrada");
            }
            conn.Close();
        }
    }
}
