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
    //Dialogo de mesa practica
    //HMFG 06/09/2023
    //-------------------------------------------------------------------------
    public partial class DlgTrabajo1 : Form
    {
        //-------------------------------------------------------------------------
        //Cosntructor
        //-------------------------------------------------------------------------
        public DlgTrabajo1()
        {
            InitializeComponent();
        }

        //-------------------------------------------------------------------------
        //FUNCION CERRAR PROGRAMA 
        public void CerrarVentana()
        {
            // Mostrar el formulario principal nuevamente
            if (Application.OpenForms.Count > 0)
            {
                DlgMenu Menu = Application.OpenForms[0] as DlgMenu;
                Menu.Show();
            }
        }
        //------------------------------------------------------------------------
        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            CbLista.Items.Clear();
            CbLista.Items.Add("Amarillo");
            CbLista.Items.Add("Azul");
            CbLista.Items.Add("Rojo");
            CbLista.Items.Add("Verde");
        }
        private void TbCampo1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13 && CheckB.Checked)
            {
                CbLista.Items.Add(TbCampo1.Text);
            }
        }
        //------------------------------------------------------------------------
        private void DlgTrabajo1_FormClosing(object sender, FormClosingEventArgs e)
        {
            CerrarVentana();
        }
    }
}
