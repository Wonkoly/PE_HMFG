using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PE_HMFG
{
    //-------------------------------------------------------------------------
    //CLASE MENU
    //
    //-------------------------------------------------------------------------

    public partial class DlgMenu : Form
    {
        //-------------------------------------------------------------------------
        //VARIABLES

        //VENTANAS
        DlgTrabajo1 T1;

        public DlgMenu()
        {
            InitializeComponent();
            //-----------------------------------------------------------------
            //CONFIGURACION DE LOS BOTONES AL INICIAR
            //-----------------------------------------------------------------
            BtnSalir.Image = Properties.Resources.BtnSalir;
            BtnSalir.TabStop = true;
            LbSalir.Parent = BtnSalir;
            LbSalir.BackColor = Color.Transparent;
            LbSalir.Location = new Point(100, 23);

            BtnTrabajo1.Image = Properties.Resources.BtnTrabajo1_1;
            BtnTrabajo1.TabStop = true;
            LbTrabajo1.Parent = BtnTrabajo1;
            LbTrabajo1.BackColor = Color.Transparent;
            LbTrabajo1.Location = new Point(125, 22);

            BtnTrabajo2.Image = Properties.Resources.BtnTrabajo2_1;
            BtnTrabajo2.TabStop = true;
            LbTrabajo2.Parent = BtnTrabajo2;
            LbTrabajo2.BackColor = Color.Transparent;
            LbTrabajo2.Location = new Point(125, 22);

            BtnTrabajo3.Image = Properties.Resources.BtnTrabajo3_1;
            BtnTrabajo3.TabStop = true;
            LbTrabajo3.Parent = BtnTrabajo3;
            LbTrabajo3.BackColor = Color.Transparent;
            LbTrabajo3.Location = new Point(125, 22);

            BtnTrabajo4.Image = Properties.Resources.BtnTrabajo;
            BtnTrabajo4.TabStop = true;
            LbTrabajo4.Parent = BtnTrabajo4;
            LbTrabajo4.BackColor = Color.Transparent;
            LbTrabajo4.Location = new Point(125, 22);

            BtnTrabajo5.Image = Properties.Resources.BtnTrabajo;
            BtnTrabajo5.TabStop = true;
            LbTrabajo5.Parent = BtnTrabajo5;
            LbTrabajo5.BackColor = Color.Transparent;
            LbTrabajo5.Location = new Point(125, 22);

            CajaDialogo.Image = Properties.Resources.CajaDialogo;
            LbDialogo.Parent = CajaDialogo;
            LbDialogo.BackColor = Color.Transparent;
            LbDialogo.Location = new Point(35, 50);

            this.KeyPreview = true;

        }
        //-----------------------------------------------------------------
        //BOTON SALIR
        //-----------------------------------------------------------------
        private void BtnSalir_Click(object sender, EventArgs e)
        {
            LbSalir.ForeColor = Color.Red;
            Application.Exit();
        }
        private void BtnSalir_MouseEnter(object sender, EventArgs e)
        {
            LbSalir.ForeColor = Color.Gray;
            LbDialogo.Text = "Saldras de la aplicacion";
        }
        private void BtnSalir_MouseLeave(object sender, EventArgs e)
        {
            LbSalir.ForeColor = Color.White;
            LbDialogo.Text = "...";
        }
        //-----------------------------------------------------------------
        //BOTON TRABAJO 1
        //-----------------------------------------------------------------
        private void BtnTrabajo1_Click(object sender, EventArgs e)
        {
            LbTrabajo1.ForeColor = Color.Red;

            // Verificar si el formulario secundario ya existe
            if (T1 == null || T1.IsDisposed)
            {
                T1 = new DlgTrabajo1();
                T1.Show();
                this.Hide();
            }
        }
        private void BtnTrabajo1_MouseEnter(object sender, EventArgs e)
        {
            LbTrabajo1.ForeColor = Color.Gray;
            LbDialogo.Text = "Este es el trabajo 1";
            PkImagen.Image = Properties.Resources.PkTrabajo1;
            BtnTrabajo1.Image = Properties.Resources.BtnTrabajo1_2;
        }
        private void BtnTrabajo1_MouseLeave(object sender, EventArgs e)
        {
            LbTrabajo1.ForeColor = Color.White;
            BtnTrabajo1.Image = Properties.Resources.BtnTrabajo1_1;
            LbDialogo.Text = "...";
            PkImagen.Image = null;
        }
        //
        //BOTON TRABAJO 2
        //
        private void BtnTrabajo2_Click(object sender, EventArgs e)
        {
            LbTrabajo2.ForeColor = Color.Red;
        }
        private void BtnTrabajo2_MouseEnter(object sender, EventArgs e)
        {
            LbTrabajo2.ForeColor = Color.Gray;
            LbDialogo.Text = "Este es el Trabajo 2";
            PkImagen.Image = Properties.Resources.PkTrabajo2;
            BtnTrabajo2.Image = Properties.Resources.BtnTrabajo2_2;
        }
        private void BtnTrabajo2_MouseLeave(object sender, EventArgs e)
        {
            LbTrabajo2.ForeColor = Color.White;
            BtnTrabajo2.Image = Properties.Resources.BtnTrabajo2_1;
            LbDialogo.Text = "...";
            PkImagen.Image = null;
        }
        //
        //BOTON TRABAJO 3
        //
        private void BtnTrabajo3_Click(object sender, EventArgs e)
        {
            LbTrabajo3.ForeColor = Color.Red;
        }
        private void BtnTrabajo3_MouseEnter(object sender, EventArgs e)
        {
            LbTrabajo3.ForeColor = Color.Gray;
            LbDialogo.Text = "Este es el Trabajo 3";
            PkImagen.Image = Properties.Resources.PkTrabajo3;
            BtnTrabajo3.Image = Properties.Resources.BtnTrabajo3_2;
        }
        private void BtnTrabajo3_MouseLeave(object sender, EventArgs e)
        {
            LbTrabajo3.ForeColor = Color.White;
            BtnTrabajo3.Image = Properties.Resources.BtnTrabajo3_1;
            LbDialogo.Text = "...";
            PkImagen.Image = null;
        }
        //
        //BOTON TRABAJO 4
        //
        private void BtnTrabajo4_Click(object sender, EventArgs e)
        {
            LbTrabajo4.ForeColor = Color.Red;
        }
        private void BtnTrabajo4_MouseEnter(object sender, EventArgs e)
        {
            LbTrabajo4.ForeColor= Color.Gray;
            LbDialogo.Text = "Este seria el Trabajo 4, xd.";
            PkImagen.Image = Properties.Resources.PkDesconocido;
        }
        private void BtnTrabajo4_MouseLeave(object sender, EventArgs e)
        {
            LbTrabajo4.ForeColor = Color.White;
            LbDialogo.Text= "...";
            PkImagen.Image = null;
        }
        //
        //BOTON TRABAJO 5
        //
        private void BtnTrabajo5_Click(object sender, EventArgs e)
        {
            LbTrabajo5.ForeColor = Color.Red;
            LbDialogo.Text = "Este seria el Trabajo 5, xd.";
        }

        private void BtnTrabajo5_MouseEnter(object sender, EventArgs e)
        {
            LbTrabajo5.ForeColor = Color.Gray;
            LbDialogo.Text = "Este seria el Trabajo 5, xd.";
            PkImagen.Image = Properties.Resources.PkDesconocido;
        }

        private void BtnTrabajo5_MouseLeave(object sender, EventArgs e)
        {
            LbTrabajo5.ForeColor = Color.White;
            LbDialogo.Text = "...";
            PkImagen.Image = null;
        }

        private void DlgMenu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Cambia el foco al siguiente control
                if (BtnTrabajo1.Focused)
                {
                    BtnTrabajo1.Focus();
                    BtnTrabajo1_MouseEnter(sender, e);
                }
                else if (BtnTrabajo1.Focused)
                {
                    BtnTrabajo2.Focus();
                    BtnTrabajo2_MouseEnter(sender, e);
                }
            }
        }
    }       
}   
