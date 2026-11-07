using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
//Melhorando Codigo
namespace Aula_1
{
    public partial class Form1 : Form
    {
        private MySqlConnection Conexao;
        private string data_source = "Datasource=localhost;Username=root;Password=;Database=db_agenda";
                
        public Form1()
        {
            InitializeComponent();
            lst_contatos.View = View.Details;
            lst_contatos.LabelEdit = true;
            lst_contatos.AllowColumnReorder = true;
            lst_contatos.FullRowSelect = true;
            lst_contatos.GridLines = true;

            lst_contatos.Columns.Add("ID", 30, HorizontalAlignment.Center);
            lst_contatos.Columns.Add("Nome", 150, HorizontalAlignment.Left);
            lst_contatos.Columns.Add("Email", 150, HorizontalAlignment.Left);
            lst_contatos.Columns.Add("Telefone", 150, HorizontalAlignment.Left);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Conexao = new MySqlConnection(data_source);
                Conexao.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Conexao;

                cmd.CommandText = "INSERT INTO contatos (nome, email, telefone) " +
                                  "VALUES (@nome, @email, @telefone)";

                cmd.Prepare();
                if (txtNome.Text != "" && txtTelefone.Text != "")
                {
                    cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@telefone", txtTelefone.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Contacto Inserido Com Sucesso!",
                                    "Info",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);



                    //Executar Insert
                }
                else {
                    MessageBox.Show("Os campos nome e telefone não podem estar em branco!","Info - JE Digital School",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }

            catch (MySqlException ex)
            {
                MessageBox.Show("Erro!"+ex.Number+" ocorreu: " + ex.Message,
                               "Erro",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu: " + ex.Message,
                                "Erro",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            
            finally {
                Conexao.Close();
            }
            

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try {

                string q = "'%" + txt_buscar.Text + "%'";

                //Criar conexão
                Conexao = new MySqlConnection(data_source);

                string sql = "SELECT * "+
                             "FROM contatos "+
                             " WHERE nome LIKE" +q+ "OR email LIKE"+q;
                Conexao.Open();
                MySqlCommand comando = new MySqlCommand(sql, Conexao);
                

                MySqlDataReader reader = comando.ExecuteReader();

                lst_contatos.Items.Clear();

                //Obtendo Informações
                while(reader.Read()){
                    string[] row = {
                                   reader.GetString(0),
                                   reader.GetString(1),
                                   reader.GetString(2),
                                   reader.GetString(3),
                                   };
                    var linha_listview = new ListViewItem(row);
                    lst_contatos.Items.Add(linha_listview);
                }

                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Conexao.Close();
            }
            
        }
    }
}
