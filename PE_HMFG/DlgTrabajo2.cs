using OfficeOpenXml.Style;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace PE_HMFG
{
    public partial class DlgTrabajo2 : Form
    {
        //VARIABLES GLOBALES
        DlgTrabajo1 funcs = new DlgTrabajo1();

        private Bitmap bmp;
        private bool cambiarModo = false;
        private bool usarLapiz = false;
        private bool usarBorrador = false;
        private bool dibujar = false;
        private Point puntoAnterior;

        //-------------------------------------------------------------------------
        //CONSTRUCTOR Y CONFIGURACION AL INICIAR EL FORMULARIO
        //-------------------------------------------------------------------------
        public DlgTrabajo2()
        {
            InitializeComponent();

            //-----------------------------------------------------------------
            //CONFIGURACION DE BOTON SALIR - 'Crear una funcion en el MENU y ponerla publica' 
            BtnSalirT1.Image = Properties.Resources.BtnSalir;
            LbSalirT1.Parent = BtnSalirT1;
            LbSalirT1.BackColor = Color.Transparent;
            LbSalirT1.Location = new Point(100, 23);

            //-----------------------------------------------------------------
            //CONFIGURACION DE BOTON SALIR - 'Crear una funcion en el MENU y ponerla publica' 
            CbPotenciaAdd();

            //-----------------------------------------------------------------
            //CONFIGURACION DE PICTUREBOX
            bmp = new Bitmap(PbxGeneral.Width, PbxGeneral.Height);
        }
        private void CbPotenciaAdd()
        {
            for (int i = 2; i < 7; i++)
            {
                CbxPotencias.Items.Add($"Potencia {i}");
            }
            CbxPotencias.SelectedIndex = 0;
        }
        private void DlgTrabajo2_FormClosing(object sender, FormClosingEventArgs e)
        {
            funcs.CerrarVentana();
        }        
        //-------------------------------------------------------------------------
        //BOTON SALIR
        //-------------------------------------------------------------------------
        private void BtnSalirT1_Click(object sender, EventArgs e)
        {
            funcs.CerrarVentana();
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
        //FUNCIONES MANDELBROT:
        //Funcion que recibe como parametro el numero de iteraciones y el valor de
        //la potencia para calcular el fractal de mandelbrot
        //-------------------------------------------------------------------------
        private void MandelbrotCustomPowerSet(int power, int iteraciones)
        {
            int width = PbxGeneral.Width;
            int height = PbxGeneral.Height;
            Bitmap bmp = new Bitmap(width, height);

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    double c_re = (col - width / 2.0) * 4.0 / width;
                    double c_im = (row - height / 2.0) * 4.0 / height;
                    int iteration = 0;
                    double x = 0, y = 0;

                    while (iteration < iteraciones && ((x * x + y * y)) <= 4)
                    {
                        if (power == 2)
                        {
                            double x_temp = (x * x) - (y * y) + c_re;
                            y = 2 * x * y + c_im;
                            x = x_temp;
                        }
                        else if (power == 3)
                        {
                            double x_temp = x * x * x - 3 * x * y * y + c_re;
                            y = 3 * x * x * y - y * y * y + c_im;
                            x = x_temp;
                        }
                        else if (power == 4)
                        {   
                            double x_temp = x * x * x * x - 6 * x * x * y * y + y * y * y * y + c_re;
                            y = 4 * x * x * x * y - 4 * x * y * y * y + c_im;
                            x = x_temp;
                        }
                        else if (power == 5)
                        {
                            double x_temp = x * x * x * x * x - 10 * x * x * x * y * y + 5 * x * y * y * y * y + c_re;
                            y = 5 * x * x * x * x * y - 10 * x * x * y * y * y + y * y * y * y * y + c_im;
                            x = x_temp;
                        }
                        else if (power == 6)
                        {
                            double x_temp = x * x * x * x * x * x - 15 * x * x * x * x * y * y + 15 * x * x * y * y * y * y - y * y * y * y * y * y + c_re;
                            y = 6 * x * x * x * x * x * y - 20 * x * x * x * y * y * y + 6 * x * y * y * y * y * y + c_im;
                            x = x_temp;
                        }
                        iteration++;
                    }

                    if (iteration < 1000)
                        bmp.SetPixel(col, row, Color.FromArgb(iteration % 128, iteration % 50 * 5, iteration % 10));
                    else
                        bmp.SetPixel(col, row, Color.Black);
                }
                PbxGeneral.Image = bmp;
            }
        }
        private void DibujaRectangulo(int X, int Y)
        {
            int x, y, ancho, alto;
            Random rgb = new Random();
            Color color = Color.FromArgb(rgb.Next(0, 255),
                                         rgb.Next(0, 255),
                                         rgb.Next(0, 255));
            Pen pen = new Pen(color, rgb.Next(2,6));

            x = X;
            y = Y;
            ancho = rgb.Next(10, 150);
            alto = rgb.Next(10, 150);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.DrawRectangle(pen, x, y, ancho, alto);
            }
            PbxGeneral.Image = bmp;
        }
        //Funcione que dibuje un rectangulo ezopecificando el tamaño con el mouse
        private void DibujarPoligono(int X, int Y)
        {
            //Colorear colores aleatorios
            Random rgb = new Random();

            //Creamos una pluma para dibujar
            Color color = Color.FromArgb(rgb.Next(0, 255),
                                         rgb.Next(0, 255),
                                         rgb.Next(0, 255));

            Font f = new Font("Arial", 12.0f);
            Brush brush = new SolidBrush(Color.Blue);

            Pen Pluma = new Pen(color, rgb.Next(2, 6));

            //Le indicamos donde vamos a dibujar
            using(Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
                //Le indicamos las cordenadas
                PointF p1 = new PointF(100.0f + X, -100.0f + Y);
                PointF p2 = new PointF(259.869f + X, -54.52f + Y);
                PointF p3 = new PointF(424.593f + X, -101.937f + Y);
                PointF p4 = new PointF(483.179f + X, -251.583f + Y);
                PointF p5 = new PointF(329.925f + X, -197.340f + Y);
                PointF p6 = new PointF(233.929f + X, -258.241f + Y);
                PointF p7 = new PointF(129.781f + X, -204.533f + Y);
                PointF p8 = new PointF(136.551f + X, -160.046f + Y);

                PointF[] points = new PointF[] { p1, p2, p3, p4, p5, p6, p7, p8 };

                //Dibujar poligono
                g.DrawPolygon(Pluma, points);

                //Puntos con strings
                g.DrawString("P1", f, brush, p1);
                g.DrawString("P2", f, brush, p2);
                g.DrawString("P3", f, brush, p3);
                g.DrawString("P4", f, brush, p4);
                g.DrawString("P5", f, brush, p5);
                g.DrawString("P6", f, brush, p6);
                g.DrawString("P7", f, brush, p7);
                g.DrawString("P8", f, brush, p8);
            }
            PbxGeneral.Image = bmp;
        }
        public void Lapiz(Point valor1, Point valor2)
        {
            //Creamos una pluma para dibujar
            Pen Pluma;
            Color color = Color.Black;

            using (Graphics g = Graphics.FromImage(bmp))
            {
                Pluma = new Pen(color, TrackTamano.Value);
                g.DrawLine(Pluma, valor1, valor2);
            }
            PbxGeneral.Image = bmp;
        }
        public void Borrador(Point valor, int tamanoBorrador)
        {
            // Obtén las coordenadas para el cuadrado de borrado
            int mitadTamano = tamanoBorrador/2;
            Rectangle areaBorrado = new Rectangle(valor.X - mitadTamano, valor.Y - mitadTamano, tamanoBorrador, tamanoBorrador);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                // Usa el color de fondo del PictureBox para simular el borrado
                using (Brush borrador = new SolidBrush(PbxGeneral.BackColor)) // O el color que desees usar para borrar
                {
                    g.FillRectangle(borrador, areaBorrado);
                }
            }
            PbxGeneral.Image = bmp;
        }
        //-------------------------------------------------------------------------
        //BOTONES DE CLASE
        //-------------------------------------------------------------------------
        private void BtnMandelBrot_Click(object sender, EventArgs e)
        {
            MandelbrotCustomPowerSet(CbxPotencias.SelectedIndex + 2, (int)NumFactalIteracion.Value);
        }
        private void BtnDibujarPoligono_Click(object sender, EventArgs e)
        {
            DibujarPoligono((int)NumPY.Value, (int)NumPX.Value);
        }
        private void BtnLapiz_Click(object sender, EventArgs e)
        {
            usarLapiz = !usarLapiz;
            usarBorrador = false;
            PbxGeneral.Cursor = Cursors.Cross;
            LbTamano.Text = "Tamaño lapiz";
            if (usarLapiz == false) { PbxGeneral.Cursor = Cursors.Default; }
        }
        private void BtnBorrador_Click(object sender, EventArgs e)
        {
            usarBorrador = !usarBorrador;
            usarLapiz = false;
            PbxGeneral.Cursor = Cursors.Cross;
            LbTamano.Text = "Tamaño borrador";
            if (usarBorrador == false) { PbxGeneral.Cursor = Cursors.Default; }
        }
        private void BtnExportar_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "Imagen PNG|*.png";
            saveFileDialog.Title = "Guardar Imagen";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog.OpenFile();
                PbxGeneral.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
                fs.Close();
            }
        }
        //-------------------------------------------------------------------------
        //EVENTOS DE CLASE
        //-------------------------------------------------------------------------
        private void PbxGeneral_MouseClick(object sender, MouseEventArgs e)
        {
            if (cambiarModo == false && dibujar == false)
            {
                DibujaRectangulo(e.X, e.Y);
            }
        }
        private void PbxGeneral_MouseMove(object sender, MouseEventArgs e)
        {
            if (dibujar == true && usarLapiz == true)
            {
                Lapiz(puntoAnterior, e.Location);
                puntoAnterior = e.Location;
            }
            else if (dibujar == true && usarBorrador == true)
            {
                Borrador(e.Location, 2*TrackTamano.Value);
                puntoAnterior = e.Location;
            }
            else if (cambiarModo == true)
            {
                DibujaRectangulo(e.X, e.Y);
            }
        }
        private void PbxGeneral_DoubleClick(object sender, EventArgs e)
        {
            cambiarModo = !cambiarModo;
            if (cambiarModo == true)
            {
                LbCambioModo.Text = "Varias";
            }
            else if (cambiarModo == false)
            {
                LbCambioModo.Text = "Una";
            }
        }
        private void PbxGeneral_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && usarLapiz == true)
            {
                dibujar = true;
                puntoAnterior = e.Location;
            }else if (e.Button == MouseButtons.Left && usarBorrador == true)
            {
                dibujar = true;
                puntoAnterior = e.Location;
            }
        }
        private void PbxGeneral_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                dibujar = false;
            }
        }
        private void GbxBotonesFiguras_Enter(object sender, EventArgs e)
        {
            ToolTip tooltip = new ToolTip();
            tooltip.SetToolTip(GbxBotonesFiguras, "Doble click para cambiar de Modo.");
        }

        private void LbCambioModo_Click(object sender, EventArgs e)
        {

        }
    }
}
