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
            BtnSalir.Image = Properties.Resources.BtnSalir;
            LbSalir.Parent = BtnSalir;
            LbSalir.BackColor = Color.Transparent;
            LbSalir.Location = new Point(100, 23);

            BtnTrabajo1.Image = Properties.Resources.BtnTrabajo1_1;
            LbTrabajo1.Parent = BtnTrabajo1;
            LbTrabajo1.BackColor = Color.Transparent;
            LbTrabajo1.Location = new Point(125, 22);

            BtnTrabajo2.Image = Properties.Resources.BtnTrabajo2_1;
            LbTrabajo2.Parent = BtnTrabajo2;
            LbTrabajo2.BackColor = Color.Transparent;
            LbTrabajo2.Location = new Point(125, 22);

            BtnTrabajo3.Image = Properties.Resources.BtnTrabajo3_1;
            LbTrabajo3.Parent = BtnTrabajo3;
            LbTrabajo3.BackColor = Color.Transparent;
            LbTrabajo3.Location = new Point(125, 22);

            BtnTrabajo4.Image = Properties.Resources.BtnTrabajo;
            LbTrabajo4.Parent = BtnTrabajo4;
            LbTrabajo4.BackColor = Color.Transparent;
            LbTrabajo4.Location = new Point(125, 22);

            BtnTrabajo5.Image = Properties.Resources.BtnTrabajo;
            LbTrabajo5.Parent = BtnTrabajo5;
            LbTrabajo5.BackColor = Color.Transparent;
            LbTrabajo5.Location = new Point(125, 22);

            CajaDialogo.Image = Properties.Resources.CajaDialogo;
            LbDialogo.Parent = CajaDialogo;
            LbDialogo.BackColor = Color.Transparent;
            LbDialogo.Location = new Point(35, 60);

        }
        //
        //BOTON SALIR
        //
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
        //
        //BOTON TRABAJO 1
        //
        private void BtnTrabajo1_Click(object sender, EventArgs e)
        {
            LbTrabajo1.ForeColor = Color.Red;
            // Verificar si el formulario secundario ya existe
            if (T1 == null || T1.IsDisposed)
            {
                T1 = new DlgTrabajo1();
                T1.ShowDialog();
            }
            
            this.Hide();
        }
    }       
}   
