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
    public partial class FDashboard : Form
    {
        public FDashboard()
        {
            InitializeComponent();
        }

        private void AturHakAkses()
        {
            //btnDataM.Visible = false;
            //btnAbsenH.Visible = false;

            switch (UserSession.IdRole)
            {
                case 1: //admin
                    btnDataM.Visible = true;
                    btnAbsenH.Visible = true;
                    break;

                case 2: // Walikelas
                    btnAbsenH.Visible = true;
                    break;

                case 3: // Guru
                    btnAbsenH.Visible = true;
                    break;
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            FDashboard dashboard = new FDashboard();
            dashboard.Visible = true;
            this.Hide();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            FUser user = new FUser()
            {
                TopLevel = false,
                TopMost = true
            };
            KF.untukForm(user, pnlContent);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (pnlDP.Visible == true)
            {
                pnlDP.Visible = false;
            }
            else
            {
                pnlDP.Visible = true;
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            FRole role = new FRole()
            {
                TopLevel = false,
                TopMost = true
            };
            KF.untukForm(role, pnlContent);
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            FSiswa siswa = new FSiswa()
            {
                TopLevel = false,
                TopMost = true
            };
            KF.untukForm(siswa, pnlContent);
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            FKelas kelas = new FKelas()
            {
                TopLevel = false,
                TopMost = true
            };
            KF.untukForm(kelas, pnlContent);
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            FGuru guru = new FGuru()
            {
                TopLevel = false,
                TopMost = true
            };
            KF.untukForm(guru, pnlContent);
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            FAbsenH absenH = new FAbsenH()
            {
                TopLevel = false,
                TopMost = true
            };
            KF.untukForm(absenH, pnlContent);
        }

        private void FDashboard_Load(object sender, EventArgs e)
        {
            AturHakAkses();
            //Application.Exit();
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            DialogResult konfirmasi = MessageBox.Show("Apakah Anda yakin ingin keluar?", "Konfirmasi Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (konfirmasi == DialogResult.Yes)
            {
                UserSession.Logout();

                FLogin formLogin = new FLogin();
                formLogin.Show();
                this.Close();
            }
        }

        private void FAbsenK_Click(object sender, EventArgs e)
        {
            FAbsenK absenK = new FAbsenK()
            {
                TopLevel = false,
                TopMost = true
            };
            KF.untukForm(absenK, pnlContent);
        }
    }
}
