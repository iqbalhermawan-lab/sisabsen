using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sisabsen_iqbal
{
    public partial class FUser : Form
    {
        public FUser()
        {
            InitializeComponent();
        }

        public void tampildata()
        {
            guna2DataGridView1.Rows.Clear();
            int no = 1;
            db.crud("SELECT * FROM users INNER JOIN roles ON users.id_role = roles.id_role");
            foreach (DataRow Row in db.ds.Tables[0].Rows)
            {
                string id = "" + Row["id_user"];
                string Nama = "" + Row["nama"];
                string user = "" + Row["username"];
                string pass = "" + Row["password"];
                string role = "" + Row["role"];
                guna2DataGridView1.Rows.Add(no, id, user, pass, role, Nama);
                no++;
            }
        }

        public void bersih()
        {
            txtNama.Clear();
            txtUser.Clear();
            txtPass.Clear();
            cmbRole.Text = "";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtNama.Text == "" || txtUser.Text == "" || txtPass.Text == "" || cmbRole.SelectedIndex == -1)
            {
                DialogResult DataKosong = MessageBox.Show("Masukan Data yang Lengkap!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                string Nama = txtNama.Text;
                string user = txtUser.Text;
                string pass = txtPass.Text;
                string role = cmbRole.Text;
                db.crud($"INSERT INTO users VALUES (null, '{user}', '{pass}', (SELECT id_role FROM roles WHERE role = '{role}'), '{Nama}');");
                bersih();
                tampildata();
            }
        }

        private void FUser_Load(object sender, EventArgs e)
        {
            tampildata();
            guna2DataGridView1.Columns["Column1"].Visible = false;
            guna2DataGridView1.Columns["Column4"].Visible = false;
            guna2HtmlLabel6.Visible = false;
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int Baris = e.RowIndex;
            int Kolom = e.ColumnIndex;

            if (Baris >= 0)
            {
                if (Kolom == 6)
                {
                    string id = guna2DataGridView1.Rows[Baris].Cells[1].Value.ToString();
                    db.crud($"SELECT u.*, r.role FROM users u JOIN roles r ON u.id_role = r.id_role WHERE u.id_user = '{id}'"); foreach (DataRow row in db.ds.Tables[0].Rows)
                    {
                        string idU = "" + row["id_user"];
                        string user = "" + row["username"];
                        string pass = "" + row["password"];
                        string role = "" + row["role"];
                        string nama = "" + row["nama"];

                        guna2HtmlLabel6.Text = idU;
                        txtNama.Text = nama;
                        txtUser.Text = user;
                        txtPass.Text = pass;
                        cmbRole.Text = role;
                    }
                }
            }

            if (Kolom == 7)
            {
                string idba = guna2DataGridView1.Rows[Baris].Cells[1].Value.ToString();
                DialogResult Hapus = MessageBox.Show("Data Anda Akan Dihapus!", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                if (Hapus == DialogResult.OK)
                {
                    db.crud($"DELETE FROM users WHERE id_user = '{idba}' ");
                    tampildata();
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string id = guna2HtmlLabel6.Text;
            string Nama = txtNama.Text;
            string user = txtUser.Text;
            string pass = txtPass.Text;
            string role = cmbRole.Text.Trim();

            //MessageBox.Show("ID: " + id + " | Role: " + role + " | Nama: " + Nama);

            db.crud($"UPDATE users SET username = '{user}', password = '{pass}', id_role = (SELECT id_role FROM roles WHERE role = '{role}'), nama = '{Nama}' WHERE id_user = '{id}'");
            bersih();
            tampildata();
        }

        private void cmbRole_DropDown(object sender, EventArgs e)
        {
            db.crud("SELECT * FROM roles");
            cmbRole.Items.Clear();

            foreach (DataRow row in db.ds.Tables[0].Rows)
            {
                string role = "" + row["role"];
                cmbRole.Items.Add(role);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            guna2DataGridView1.Rows.Clear();
            int no = 1;
            db.crud($"SELECT * FROM users INNER JOIN roles ON users.id_role = roles.id_role WHERE users.nama LIKE '%{txtSearch.Text}%' OR users.username LIKE '%{txtSearch.Text}%' OR roles.role LIKE '%{txtSearch.Text}%'");
            foreach (DataRow row in db.ds.Tables[0].Rows)
            {
                string id = "" + row["id_user"];
                string user = "" + row["username"];
                string pass = "" + row["password"];
                string role = "" + row["role"];
                string nama = "" + row["nama"];
                guna2DataGridView1.Rows.Add(no, id, user, pass, role, nama);
                no++;
            }
        }
    }
}
