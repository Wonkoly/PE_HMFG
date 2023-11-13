using OfficeOpenXml;
using OfficeOpenXml.ConditionalFormatting;
using PE_HMFG.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PE_HMFG
{
    public partial class DlgTrabajo3 : Form
    {
        //VARIABLES GLOBALES
        DlgTrabajo1 func = new DlgTrabajo1();

        private Bitmap bmp;
        private Color colorLinea;
        private Color colorTexto;

        //-------------------------------------------------------------------------
        //CONSTRUCTOR
        //-------------------------------------------------------------------------
        public DlgTrabajo3()
        {
            //Inicializamos los componentes
            InitializeComponent();
            DgvCuadroConstruccion.Visible = false;

            //-----------------------------------------------------------------
            //CONFIGURACION DE BOTON SALIR - 'Crear una funcion en el MENU y ponerla publica' 
            BtnSalirT1.Image = Properties.Resources.BtnSalir;
            LbSalirT1.Parent = BtnSalirT1;
            LbSalirT1.BackColor = Color.Transparent;
            LbSalirT1.Location = new Point(100, 23);

            //Configuracion inicial de los colores por defecto
            colorLinea = colorDialog.Color;
            colorTexto = colorDialog.Color;
            BtnColorLinea.BackColor = colorLinea;
            BtnColorTexto.BackColor = colorLinea;

            bmp = new Bitmap(PbxPlano.Width, PbxPlano.Height);
        }
        //-------------------------------------------------------------------------
        //MOSTRAR TABLAS Y PLANO
        //-------------------------------------------------------------------------
        private void CheckMostrarTabla_CheckedChanged(object sender, EventArgs e)
        {
            DgvCuadroConstruccion.Visible = !DgvCuadroConstruccion.Visible;
        }
        private void DlgTrabajo3_MaximumSizeChanged(object sender, EventArgs e)
        {
            bmp = new Bitmap(PbxPlano.Width, PbxPlano.Height);
        }
        private void DlgTrabajo3_FormClosing(object sender, FormClosingEventArgs e)
        {
            func.CerrarVentana();
        }
        //-------------------------------------------------------------------------
        //FUNCIONES GENERALES
        //-------------------------------------------------------------------------
        //FUNCION IMPORTAR EXCEL:
        //Abre un ventana para seleccionar solamente archivos excel, carga el archivo
        //y lo lee pasando su informacion a una tabla.
        //-------------------------------------------------------------------------
        private void ImportExcel()
        {
            //OpenFileDialog sirve para abrir una ventana para cargar archivos
            OpenFileDialog openFileDialog = new OpenFileDialog();

            //Se aplica un filtro para solo poder cargar archivos de excel
            openFileDialog.Filter = "Archivos de Excel|*.xlsx";

            //Validamos que se haya cargado un archivo
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                //Obtine la direccion del archivo con el metodo FileName
                string rutaArchivo = openFileDialog.FileName;

                //Se pasa la ruta del archivo excel para abrirlo desde 
                using (ExcelPackage package = new ExcelPackage(new System.IO.FileInfo(rutaArchivo)))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; // Obtén la primera hoja del archivo Excel.

                    //Se calcula el numero de columnas y filas con valores no nulos
                    int numFilas = worksheet.Dimension.Rows;
                    int numColumnas = worksheet.Dimension.Columns;

                    // Limpia el DataGridView
                    DgvCuadroConstruccion.Rows.Clear();
                    DgvCuadroConstruccion.Columns.Clear();

                    //Agrega las columnas al DataGridView
                    for (int columna = 1; columna <= numColumnas; columna++)
                    {
                        DgvCuadroConstruccion.Columns.Add(worksheet.Cells[4, columna].Text, worksheet.Cells[4, columna].Text);
                    }

                    // Agrega las filas al DataGridView
                    for (int fila = 5; fila <= numFilas; fila++)
                    {
                        DataGridViewRow dataGridViewRow = new DataGridViewRow();
                        dataGridViewRow.CreateCells(DgvCuadroConstruccion);

                        for (int columna = 1; columna <= numColumnas; columna++)
                        {
                            dataGridViewRow.Cells[columna - 1].Value = worksheet.Cells[fila, columna].Text;
                        }

                        DgvCuadroConstruccion.Rows.Add(dataGridViewRow);
                    }

                    NomPersona.Text = "Persona: " + worksheet.Cells[3, 1].Text;
                    NomProyecto.Text = "Proyecto: " + worksheet.Cells[1, 1].Text;
                    NomUbicacion.Text = "Ubicacion: " + worksheet.Cells[2, 1].Text;

                }
            }
        }
        //-------------------------------------------------------------------------
        //FUNCION LIMPIAR PLANO:
        //Limpia el area de dibujo para poder trazar otra figura.
        //-------------------------------------------------------------------------
        private void LimpiarPbx()
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
            }
            PbxPlano.Image = bmp;
        }
        //-------------------------------------------------------------------------
        //FUNCION OBTENER CORDENADAS:
        //Obtine las cordenadas y las devuelve en forma de un array de puntos.
        //-------------------------------------------------------------------------
        private PointF[] ObtenerCordenadas(int X, int Y)
        {
            //Variables a utilizar
            PointF[] cordenadas;
            PointF punto;
            int filas = DgvCuadroConstruccion.RowCount - 1;
            object valorX, valorY;

            if (filas <= 0)
            {
                return null;
            }

            cordenadas = new PointF[filas];

            for (int i = 0; i < filas; i++)
            {
                valorX = DgvCuadroConstruccion.Rows[i].Cells[2].Value;
                valorY = DgvCuadroConstruccion.Rows[i].Cells[3].Value;
                try
                {
                    punto = new PointF(Convert.ToSingle(valorX) + X, //La formula que se aplica es:
                                      -Convert.ToSingle(valorY) + Y);//(x + X, -y + Y) rota la figura 

                    cordenadas[i] = punto;
                }
                catch { MessageBox.Show($"Los valores de X o Y en la fila {i + 1} son invalidos.", "Error"); }
            }

            return cordenadas;
        }
        private PointF[] CordenadasZoom(PointF[] cordenadas)
        { 
            float escala = 1.0f + (float)TamanoDibujo.Value / 100.0f;
            PointF[] cordenadasEscaladas = new PointF[cordenadas.Length];
            for (int i = 0; i < cordenadas.Length; i++)
            {
                cordenadasEscaladas[i] = new PointF(cordenadas[i].X * escala, cordenadas[i].Y * escala);
            }

            return cordenadasEscaladas;
        }
        private bool ChecarCordenadas(PointF[] cordenadas)
        {
            if (cordenadas == null || cordenadas.Length < 3)
            {
                MessageBox.Show("Debe haber almenos 3 Coredenadas.", "Error");
                return false;
            }
            return true;
        }
        //-------------------------------------------------------------------------
        //FUNCION DUBUJAR CORDENADAS:
        //Usa las cordenadas obtenidas de la funcion para trazar una figura.
        //-------------------------------------------------------------------------
        private void DibujarCordenadas(PointF[] cordenadas, Color color)
        {
            Pen pen = new Pen(color, TamanoLapiz.Value);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.DrawPolygon(pen, CordenadasZoom(cordenadas));
            }
            PbxPlano.Image = bmp;
        }
        //-------------------------------------------------------------------------
        //FUNCION DIBUJAR ETIQUETAS:
        //Dibuja las etiquetas utilizando los puntos de la figura y extrayendo la
        //etiqueta de la tabla.
        //-------------------------------------------------------------------------
        private void DibujarEtiquetas(PointF[] cordenadas, Color color)
        {
            object etiqueta;
            Font font = new Font("Arial", 12.0f);
            Brush brush = new SolidBrush(color);

            cordenadas = CordenadasZoom(cordenadas);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                for (int i = 0; i < cordenadas.Length; i++)
                {
                    etiqueta = DgvCuadroConstruccion.Rows[1].Cells[1].Value;
                    g.DrawString(etiqueta.ToString(), font, brush, cordenadas[i]);
                }
            }
            PbxPlano.Image = bmp;
        }
        //-------------------------------------------------------------------------
        //FUNCION PARA DIBUJAR LOS DATOS DE PROYECTO
        //Dibuja los datos del proyecto como etiquitas
        //-------------------------------------------------------------------------
        private void DibujarDatos()
        {
            Font font = new System.Drawing.Font("Arial", 12.0f);
            Brush brush = new SolidBrush(Color.Black);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.DrawString(NomProyecto.Text, font, brush, new PointF(10, 700));
                g.DrawString(NomUbicacion.Text, font, brush, new PointF(10, 725));
                g.DrawString(NomPersona.Text, font, brush, new PointF(10, 750));
            }
        }
        //-------------------------------------------------------------------------
        private void ActualizarDibujo()
        {
            int x = (int)NumX.Value;
            int y = (int)NumY.Value;

            PointF[] cordenadas = ObtenerCordenadas(-100 + x, 260 + y);
            if (!ChecarCordenadas(cordenadas)) return;

            LimpiarPbx();
            DibujarCordenadas(cordenadas, colorLinea);
            DibujarEtiquetas(cordenadas, colorTexto);
            DibujarDatos();
        }
        //-------------------------------------------------------------------------
        //BOTONES TABLAS
        //-------------------------------------------------------------------------
        private void BtnImportarExcel_Click(object sender, EventArgs e)
        {
            ImportExcel();
        }
        private void BtnBorrar_Click(object sender, EventArgs e)
        {
            DgvCuadroConstruccion.Rows.Clear();
            NomProyecto.Text = string.Empty;
            NomPersona.Text = string.Empty;
            NomUbicacion.Text = string.Empty;
        }
        private void BtnDibujarCordenadas_Click(object sender, EventArgs e)
        {
            ActualizarDibujo();
        }
        private void BtnExportImagen_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "Imagen PNG|*.png";
            saveFileDialog.Title = "Guardar Imagen";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog.OpenFile();
                PbxPlano.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
                fs.Close();
            }
        }
        private void BtnBorrarDibujo_Click(object sender, EventArgs e)
        {
            LimpiarPbx();
        }
        private void BtnColorTexto_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                colorTexto = colorDialog.Color;
                ActualizarDibujo();
            }
            BtnColorTexto.BackColor = colorDialog.Color;
        }
        private void BtnColorLinea_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                colorLinea = colorDialog.Color;
                ActualizarDibujo();
            }
            BtnColorLinea.BackColor = colorDialog.Color;
        }
        //-------------------------------------------------------------------------
        //BOTON SALIR CONFIGURACION
        //-------------------------------------------------------------------------
        private void BtnSalirT1_Click(object sender, EventArgs e)
        {
            func.CerrarVentana();
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

        private void checkBrujula_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBrujula.Checked == true)
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    Image img = new Bitmap(Properties.Resources.brujula, 128, 128);
                    g.DrawImage(img, new Point(1200, 15));
                }
                PbxPlano.Image = bmp;
            }
            else
            {
                ActualizarDibujo();
            }
        }
    }
}