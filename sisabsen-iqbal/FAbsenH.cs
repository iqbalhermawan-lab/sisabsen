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
    public partial class FAbsenH : Form
    {
        public FAbsenH()
        {
            InitializeComponent();
        }

        public void loadkelas()
        {
            db.crud("SELECT id_kelas, nama_kelas FROM kelas");

            // kode agar datanya tidak hilang saat db.crud dipanggil lagi nanti
            DataTable dtKelas = db.ds.Tables[0].Copy();

            cmbKelas.DataSource = dtKelas;
            cmbKelas.DisplayMember = "nama_kelas";
            cmbKelas.ValueMember = "id_kelas";
            cmbKelas.SelectedIndex = -1;
        }

        private void FAbsenH_Load(object sender, EventArgs e)
        {
            loadkelas();
        }

        public void loadsiswa(string idKelas)
        {
            guna2DataGridView1.Rows.Clear();
            int no = 1;

            db.crud($"SELECT id_siswa, nis, nama FROM siswa WHERE id_kelas = '{idKelas}'");

            foreach (DataRow row in db.ds.Tables[0].Rows)
            {
                string id = "" + row["id_siswa"];
                string nis = "" + row["nis"];
                string nama = "" + row["nama"];

                guna2DataGridView1.Rows.Add(no, id, nis, nama, "Hadir");
                no++;
            }
        }

        private void cmbKelas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbKelas.SelectedIndex != -1 && cmbKelas.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                loadsiswa(cmbKelas.SelectedValue.ToString());
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.Rows.Count == 0 || cmbKelas.SelectedIndex == -1)
            {
                MessageBox.Show("Pilih kelas dan pastikan data siswa tersedia!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string tgl = dtpTanggal.Value.ToString("yyyy-MM-dd");
            string idKelas = cmbKelas.SelectedValue.ToString();

            // TODO: Ganti ini dengan Session ID Guru yang login nantinya
            string idGuru = "1";

            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                // Mencegah baris kosong (new row) ikut tersimpan
                if (row.Cells[1].Value != null)
                {
                    string idSiswa = row.Cells[1].Value.ToString();
                    string ket = row.Cells[4].Value.ToString();

                    db.crud($"INSERT INTO absensi (tanggal, id_kelas, id_siswa, keterangan, id_guru) VALUES ('{tgl}', '{idKelas}', '{idSiswa}', '{ket}', '{idGuru}')");
                }
            }

            MessageBox.Show("Data absensi berhasil disimpan secara masal!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

            guna2DataGridView1.Rows.Clear();
            cmbKelas.SelectedIndex = -1;
        }
    }
}
