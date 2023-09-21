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

            //-----------------------------------------------------------------
            //CONFIGURACION DE BOTON SALIR
            BtnSalirT1.Image = Properties.Resources.BtnSalir;
            LbSalirT1.Parent = BtnSalirT1;
            LbSalirT1.BackColor = Color.Transparent;
            LbSalirT1.Location = new Point(100, 23);

            //-----------------------------------------------------------------
            //CONFIGURACION TABLAS - Ocultar
            DgvTablaPrincipal.Visible = false;
            DgvTablaWhile.Visible = false;
            DgvTablaDinamica.Visible = false;
            
            //-----------------------------------------------------------------
            //CONFIGURACION TABLA DINAMICA - Ocultar configuracion
            GbxOpcionesTablaDinamica.Visible = false;


        }

        //-------------------------------------------------------------------------
        //FUNCIONES PARA LAS TABLAS
        //-------------------------------------------------------------------------
        private void LlenarTablaPrincipal()
        {
            DgvTablaPrincipal.Rows.Clear();

            string[] Persona1;
            string[] Persona2;
            string[] Persona3;

            Persona1 = new string[3];
            Persona2 = new string[3];
            Persona3 = new string[3];

            Persona1[0] = "Gael";
            Persona1[1] = "Hernandez";
            Persona1[2] = "Magaña";

            Persona2[0] = "Luis";
            Persona2[1] = "Gomez";
            Persona2[2] = "Magaña";

            Persona3[0] = "Gael";
            Persona3[1] = "Hernandez";
            Persona3[2] = "Magaña";

            DgvTablaPrincipal.Rows.Add(Persona1);
            DgvTablaPrincipal.Rows.Add(Persona2);
            DgvTablaPrincipal.Rows.Add(Persona3);

        }

        private void LlenarTablaWhile()
        {
            int i = 0;
            DgvTablaWhile.Rows.Add(4);

            while (i < 4)
            {
                int j = 0;

                while (j < 4)
                {

                    DgvTablaWhile.Rows[i].Cells[j].Value = i.ToString() + "," + j.ToString();
                    j++;
                }

                i++;
            }

        }
        private void LlenarTablaDinamica()
        {
            DgvTablaDinamica.Rows.Clear();
            DgvTablaDinamica.Columns.Clear();

            if (NumColumnas.Value == 0)
            {
                return;
            }

            for (int i = 1; i <= NumColumnas.Value; i++)
            {
                DgvTablaDinamica.Columns.Add("Col" + i.ToString(), "X" + i.ToString());
            }
            DgvTablaDinamica.Rows.Add((int)NumFilas.Value);

        }
        private void LimpiarTablas()
        {
            DgvTablaDinamica.Rows.Clear();
            DgvTablaPrincipal.Rows.Clear();
            DgvTablaWhile.Rows.Clear();
        }
        //-------------------------------------------------------------------------
        //FUNCION PARA SALIR DEL PROGRAMA
        //-------------------------------------------------------------------------
        public void CerrarVentana()
        {
            // Mostrar el formulario principal nuevamente
            if (Application.OpenForms.Count > 0)
            {
                DlgMenu Menu = Application.OpenForms[0] as DlgMenu;
                Menu.Show();
            }
        }
        private void DlgTrabajo1_FormClosing(object sender, FormClosingEventArgs e)
        {
            CerrarVentana();
        }

        //-------------------------------------------------------------------------
        //BOTON SALIR 
        //-------------------------------------------------------------------------
        private void BtnSalirT1_Click(object sender, EventArgs e)
        {
            CerrarVentana();
            this.Close();
        }
        private void BtnSalirT1_MouseEnter(object sender, EventArgs e)
        {
            LbSalirT1.ForeColor = Color.Gray;
        }
        private void BtnSalirT1_MouseLeave(object sender, EventArgs e)
        {
            LbSalirT1.ForeColor = Color.White;
        }
        //-------------------------------------------------------------------------
        //CHECK BOXS TABLAS - Mostrar tablas
        //-------------------------------------------------------------------------
        private void CheckTablaPrincipal_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckTablaPrincipal.Checked)
            {
                CheckTablaWhile.Checked = false;
                CheckTablaDinamica.Checked = false;
            }

            //Mostrar tablas
            DgvTablaDinamica.Visible = false;
            DgvTablaPrincipal.Visible = true;
            DgvTablaWhile.Visible = false;

            //Limpiar tablas
            LimpiarTablas();

            //Habilitar Botones de Opciones
            GbxOpcionesTablaDinamica.Visible = true;
            LbColumnas.Visible = false;
            NumColumnas.Visible = false;
            LbFilas.Visible = false;
            NumFilas.Visible = false;
            BtnLlenarTablaExtremo.Visible = true;

        }
        private void CheckTablaWhile_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckTablaWhile.Checked)
            {
                CheckTablaPrincipal.Checked = false;
                CheckTablaDinamica.Checked = false;
            }

            //Mostrar tablas
            DgvTablaDinamica.Visible = false;
            DgvTablaPrincipal.Visible = false;
            DgvTablaWhile.Visible = true;

            //Borrar tablas
            LimpiarTablas();

            //Habilitar botones
            GbxOpcionesTablaDinamica.Visible = false;

        }
        private void CheckTablaDinamica_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckTablaDinamica.Checked)
            {
                CheckTablaWhile.Checked = false;
                CheckTablaPrincipal.Checked = false;
            }

            //Mostrar tablas
            DgvTablaDinamica.Visible = true;
            DgvTablaPrincipal.Visible = false;
            DgvTablaWhile.Visible = false;

            //Borrar tabla
            LimpiarTablas();

            //habilitar botones
            GbxOpcionesTablaDinamica.Visible = true;
            
            NumColumnas.Visible = true;
            NumFilas.Visible = true;
            BtnLlenarTablaExtremo.Visible = false;
        }

        //-------------------------------------------------------------------------
        //BOTON LLENAR TABLAS
        //-------------------------------------------------------------------------
        private void BtnLlanarTabla_Click(object sender, EventArgs e)
        {
            if (CheckTablaPrincipal.Checked)
            {
                LlenarTablaPrincipal();
            } 
            if (CheckTablaWhile.Checked)
            {
                LlenarTablaWhile();
            } 
            if (CheckTablaDinamica.Checked)
            {
                LlenarTablaDinamica();
            }
            else
            {

            }
        }

        //-------------------------------------------------------------------------
        //BOTON LIMPIAR TABLAS
        //-------------------------------------------------------------------------
        private void BtnnLimpiarTabla_Click(object sender, EventArgs e)
        {
            LimpiarTablas();
        }

        //-------------------------------------------------------------------------
        //BOTON LLENAR EXTREMO
        //-------------------------------------------------------------------------
        private void BtnLlenarTablaExtremo_Click(object sender, EventArgs e)
        {
            DgvTablaWhile.Rows.Clear();

            //LLENAR CON FOR
            for (int i = 0; i < 50; i++)
            {
                string[] Nombre = new string[3];

                Nombre[0] = "A" + i.ToString();
                Nombre[1] = "B" + i.ToString();
                Nombre[2] = "C" + i.ToString();

                DgvTablaPrincipal.Rows.Add(Nombre);
                DgvTablaPrincipal.FirstDisplayedScrollingRowIndex = DgvTablaPrincipal.Rows.Count - 1;
                DgvTablaPrincipal.Refresh();
            }

        }
    }
}
