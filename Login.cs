using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace EDnevnik
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (txt_Name.Text == "" || txt_Pass.Text == "")
            {
                MessageBox.Show("Email ili lozinka nisu uneti!");
                return;
            }
            else
            {
                try
                {
                    SqlConnection veza = Konekcija.Connect();
                    SqlCommand komanda = new SqlCommand("SELECT * FROM osoba WHERE email = @username", veza);
                    komanda.Parameters.AddWithValue("@username", txt_Name.Text);
                    SqlDataAdapter adapter = new SqlDataAdapter(komanda);
                    DataTable tabela = new DataTable();
                    adapter.Fill(tabela);

                    int brojac = tabela.Rows.Count;
                    if (brojac == 1)
                    {
                        if (String.Compare(tabela.Rows[0]["pass"].ToString(), txt_Pass.Text) == 0)
                        {
                            MessageBox.Show("Uspesno ste se ulogovali!");
                            Program.user_ime = tabela.Rows[0]["ime"].ToString();
                            Program.user_prezime = tabela.Rows[0]["prezime"].ToString();
                            Program.user_uloga = (int) tabela.Rows[0]["uloga"];
                            this.Hide();
                            Glavna frm_Glavna = new Glavna();
                            frm_Glavna.Show();
                        }
                        else
                        {
                            MessageBox.Show("Uneli ste pogresnu lozinku!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nepostojeci e-mail!");
                    }
                }
                catch (Exception greska)
                {
                    MessageBox.Show(greska.Message);
                }
            }
        }
    }
}
