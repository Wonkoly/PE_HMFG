using System;
using System.Drawing;
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
            DesabilitarControles();

        }
        //-------------------------------------------------------------------------
        //FUNCION PARA SALIR DEL PROGRAMA
        //-------------------------------------------------------------------------
        public void CerrarVentana()
        {
            // Mostrar el formulario principal nuevamente
            if (Application.OpenForms.Count > 0)//Checamos si exixten formularios abiertos
            {
                //Agarramos una referencia del primer formulario y lo asignamos a un objeto similar.
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
            DgvTablaWhile.Rows.Clear();
            int i = 0;
            while (i < 4)
            {
                DgvTablaWhile.Rows.Add();
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

            int fila = 0, columna = 0, totalD1 = 0, totalD2 = 0, totalPares = 0, totalImpares = 0;

            //Crear un generador de números aleatorios "Random"
            Random random = new Random();

            if (NumColumnas.Value == NumFilas.Value)
            {
                if (NumColumnas.Value == 0 && NumFilas.Value == 0)
                {
                    NumColumnas.Value = 10;
                    NumFilas.Value = 10;
                }

                //Crear tabla "Matriz"
                for (int i = 1; i <= NumColumnas.Value; i++)
                {
                    DgvTablaDinamica.Columns.Add("Col" + i.ToString(), "x" + i.ToString());
                }
                DgvTablaDinamica.Rows.Add((int)NumFilas.Value);

                //Llenar Filas y generar números aleatorios
                while (fila < NumColumnas.Value)
                {
                    columna = 0;
                    while (columna < NumColumnas.Value)
                    {
                        int NumeroAleatorio = random.Next(0, 9);
                        DgvTablaDinamica.Rows[fila].Cells[columna].Value = NumeroAleatorio.ToString();
                        //int numA = (int)DgvTablaDinamica.Rows[fila].Cells[columna].Value;
                        int numA = int.Parse(DgvTablaDinamica.Rows[fila].Cells[columna].Value.ToString());
                        if (numA % 2 == 0)
                        {
                            DgvTablaDinamica.Rows[fila].Cells[columna].Style.BackColor = Color.LightYellow;
                            totalPares += numA;
                        }
                        else
                        {
                            DgvTablaDinamica.Rows[fila].Cells[columna].Style.BackColor = Color.LightGreen;
                            totalImpares += numA;
                        }

                        // Sumar la diagonal principal (de arriba izquierda a abajo derecha)
                        if (fila == columna)
                        {
                            DgvTablaDinamica.Rows[fila].Cells[columna].Style.BackColor = Color.Aqua;
                            totalD1 += NumeroAleatorio;
                        }

                        // Sumar la diagonal inversa (de arriba derecha a abajo izquierda)
                        if (fila + columna == NumColumnas.Value - 1)
                        {
                            DgvTablaDinamica.Rows[fila].Cells[columna].Style.BackColor = Color.Red;
                            totalD2 += NumeroAleatorio;
                        }
                        columna++;
                    }
                    fila++;
                }
                LbSumaTotal1.Text = "Total Amarillo: " + totalPares;
                LbSumaTotal1.ForeColor = Color.LightYellow;
                LbSumaTotal2.Text = "Total Verde: " + totalImpares;
                LbSumaTotal2.ForeColor = Color.LightGreen;
                LbSumaTotal3.Text = "Total Azul: " + totalD1;
                LbSumaTotal3.ForeColor = Color.Aqua;
                LbSumaTotal4.Text = "Total Rojo: " + totalD2;
                LbSumaTotal4.ForeColor = Color.Red;
            }
            else
            {
                MessageBox.Show("Los números no son equivalentes", "Error");
            }
        }
        private void LimpiarTablas()
        {
            DgvTablaDinamica.Rows.Clear();
            DgvTablaPrincipal.Rows.Clear();
            DgvTablaWhile.Rows.Clear();
        }
        //-------------------------------------------------------------------------
        //CHECK BOXS TABLAS - Mostrar tablas
        //-------------------------------------------------------------------------
        private void DesabilitarControles()
        {
            //Tablas
            DgvTablaPrincipal.Visible = false;
            DgvTablaWhile.Visible = false;
            DgvTablaDinamica.Visible = false;

            //Botones de Opciones
            GbxOpcionesTablaDinamica.Visible = false;
            LbColumnas.Visible = false;
            NumColumnas.Visible = false;
            LbFilas.Visible = false;
            NumFilas.Visible = false;
            BtnLlenarTablaExtremo.Visible = false;

            //GrupBox Suma Total
            GbxSumaTotal.Visible = false;
        }
        //-------------------------------------------------------------------------
        //CHECK BOXS TABLAS - Mostrar tablas
        //-------------------------------------------------------------------------
        private void CheckTablaPrincipal_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckTablaPrincipal.Checked)
            {
                CheckTablaDinamica.Checked = false;
                CheckTablaWhile.Checked = false;
                DgvTablaPrincipal.Visible = true;

                GbxOpcionesTablaDinamica.Visible = true;
                BtnLlenarTablaExtremo.Visible = true;
            }
            else
            {
                DesabilitarControles();
            }
        }
        private void CheckTablaWhile_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckTablaWhile.Checked)
            {
                CheckTablaDinamica.Checked = false;
                CheckTablaPrincipal.Checked = false;
                DgvTablaWhile.Visible = true;

            }
            else
            {
                DesabilitarControles();
            }
        }
        private void CheckTablaDinamica_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckTablaDinamica.Checked)
            {
                CheckTablaPrincipal.Checked = false;
                CheckTablaWhile.Checked = false;
                DgvTablaDinamica.Visible = true;

                GbxOpcionesTablaDinamica.Visible = true;
                LbColumnas.Visible = true;
                NumColumnas.Visible = true;
                LbFilas.Visible = true;
                NumFilas.Visible = true;

                GbxSumaTotal.Visible = true;
            }
            else
            {
                DesabilitarControles();
            }
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
            if (CheckTablaDinamica.Checked)
            {
                LbSumaTotal2.Text = "...";
                LbSumaTotal1.Text = "...";
                LbSumaTotal3.Text = "...";
                LbSumaTotal4.Text = "...";
            }
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
