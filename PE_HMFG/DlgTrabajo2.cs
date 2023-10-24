using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PE_HMFG
{  
    public partial class DlgTrabajo2 : Form
    {
        //VARIABLES GLOBALES
        DlgTrabajo1 funcs = new DlgTrabajo1();

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

            TabPracticas.Focus();
     
            //-----------------------------------------------------------------
            //CONFIGURACION DE BOTON SALIR - 'Crear una funcion en el MENU y ponerla publica' 
            ConfigComboxP(); 
            DesabilitarControles();
        }
        private void ConfigComboxP()
        {
            for (int i = 1; i < 6; i++)
            {
                CbxPotencias.Items.Add($"Potencia {i.ToString()}");
            }
        }
        private void DesabilitarControles()
        {
            GbxBotonesFiguras.Visible = false;
            GbxHerramientasPain.Visible = false;
            GbxBotonesMandel.Visible = false;
            GbxBotonesPoligonos.Visible = false;
        }
        private void DlgTrabajo2_FormClosing(object sender, FormClosingEventArgs e)
        {
            funcs.CerrarVentana();
        }  
        private void TabPracticas_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabPage paginaSeleccionada = TabPracticas.SelectedTab;
            
            switch (paginaSeleccionada.Name)
            {
                case "TabMandelBrot":
                    DesabilitarControles();
                    GbxBotonesMandel.Visible = true;
                    break;
                case "TabPaint":
                    DesabilitarControles();
                    GbxHerramientasPain.Visible = true;
                    break;
                case "TabFiguras":
                    DesabilitarControles();
                    GbxBotonesFiguras.Visible = true;
                    break;
                case "TabPoligonos":
                    DesabilitarControles(); 
                    GbxBotonesPoligonos.Visible = true;
                    break;
                case null:
                    DesabilitarControles();
                    break;
            }
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
        //VENTANA FIGURAS
        //Crea rectangulos de diferentes dimesiones y colores atravez de un funcion
        //utilizando la clase de Graphis t Drawn
        //------------------------------------------------------------------------- 
        //VARIABLES 
        bool CambioModo = false; //falso - Cuadrados con un clik verdadero - al mover

        //------------------------------------------------------------------------- 
        //FUNCIONES
        //------------------------------------------------------------------------- 
        public void Reactangulo(int X, int Y)
        {
            //Colorear colores aleatorios
            Random rgb = new Random();

            //Creamos una pluma para dibujar
            Pen Pluma;
            Color color = Color.FromArgb(rgb.Next(0, 255), 
                                         rgb.Next(0, 255), 
                                         rgb.Next(0, 255));

            //Le indicamos donde vamos a dibujar
            Graphics g = TabFiguras.CreateGraphics();
            
            //Le indicamos las cordenadas
            int x, y, ancho, alto;
            
            Pluma = new Pen(color, rgb.Next(2, 6));
            x = X;
            y = Y;   
            ancho = rgb.Next(10, 150);
            alto = rgb.Next(10, 150);

            g.DrawRectangle(Pluma, x, y, ancho, alto);

        }
        //-------------------------------------------------------------------------
        //EVENTOS
        //------------------------------------------------------------------------- 
        private void TabFiguras_MouseClick(object sender, MouseEventArgs e)
        {
            if (CambioModo == false)
            {
                Reactangulo(e.X, e.Y);
            }
        }
        private void PnlPintar_MouseMove(object sender, MouseEventArgs e)
        {
            if (CambioModo == true)
            {
                MouseEventArgs mouse;
                mouse = (MouseEventArgs)e;
                Reactangulo(mouse.X, mouse.Y);    
            } 
        }
        private void TabFiguras_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            CambioModo = !CambioModo;
            if (CambioModo == true)
            {
                LbCambioModo.Text = "Turbo";
            } else if (CambioModo == false)
            {
                LbCambioModo.Text = "Una figura";
            }
        }
        //-------------------------------------------------------------------------
        //BOTONES
        //------------------------------------------------------------------------- 
        private void BtnBorrarRectangulos_Click(object sender, EventArgs e)
        {
            Graphics g = TabFiguras.CreateGraphics();
            g.Clear(Color.White);
        }
        //-------------------------------------------------------------------------
        //VENTANA MANDELBROT
        //Crea un factal atravez de una funcion utilizacndo las ecuaciones del fisico
        //Mandelbrot 
        //------------------------------------------------------------------------- 
        //FUNCIONES
        //------------------------------------------------------------------------- 
        private void MandelBrotSet(float anchoX, float anchoY, float largoX, float largoY)
        {
            //Construccion del lienzo a pintar
            int Ancho = PbxMandalaLienzo.Width;
            int Alto = PbxMandalaLienzo.Height;
            Bitmap bitmap = new Bitmap(Ancho, Alto);

            for (int fila = 0; fila < Alto; fila++)
            {
                for (int columna = 0; columna < Ancho; columna++)
                {
                    double c_re = (columna - Ancho / anchoX) * anchoY / Ancho; //1.6 2.4
                    double c_im = (fila - Alto / largoX) * largoY / Alto;      //2.0 2.6
                    int iteracion = 0;
                    double x = 0, y = 0;

                    while (iteracion < 1000 && ((x*x)+(y*y)) <= 4)
                    {
                        double x_temporal = (x * x) - (y * y) + c_re;
                        y = 2 * x * y + c_im;
                        x = x_temporal;
                        iteracion++;
                    }

                    if (iteracion < 1000)
                    {
                        bitmap.SetPixel(columna, fila, Color.FromArgb(iteracion % 128, iteracion % 30 * 5, iteracion % 10));
                    }
                    else
                    {
                        bitmap.SetPixel(columna, fila, Color.Black);
                    }
                }
            } 
            PbxMandalaLienzo.Image = bitmap;
        }
        private void MandelBrot(int maxIterations, double power)
        {
            int Ancho = PbxMandalaLienzo.Width;
            int Alto = PbxMandalaLienzo.Height;
            Bitmap bitmap = new Bitmap(Ancho, Alto);

            for (int fila = 0; fila < Alto; fila++)
            {
                for (int columna = 0; columna < Ancho; columna++)
                {
                    double c_re = (columna - Ancho / 1.6) * 2.4 / Ancho;
                    double c_im = (fila - Alto / 2.0) * 2.6 / Alto;
                    int iteracion = 0;
                    double x = 0, y = 0;

                    while (iteracion < maxIterations && ((x * x) + (y * y)) <= 4)
                    {
                        double x_temporal = Math.Pow(x, power) - Math.Pow(y, power) + c_re;
                        y = 2 * x * y + c_im;
                        x = x_temporal;
                        iteracion++;
                    }

                    if (iteracion < maxIterations)
                    {
                        int r = (int)Math.Max(0, Math.Min(255, 255 * (double)iteracion / maxIterations));
                        int g = (int)Math.Max(0, Math.Min(255, 255 * Math.Sin(2 * Math.PI * (double)iteracion / maxIterations)));
                        int b = (int)Math.Max(0, Math.Min(255, 255 * Math.Cos(2 * Math.PI * (double)iteracion / maxIterations)));

                        bitmap.SetPixel(columna, fila, Color.FromArgb(r, g, b));
                    }
                    else
                    {
                        bitmap.SetPixel(columna, fila, Color.Black);
                    }
                }
            }
            PbxMandalaLienzo.Image = bitmap;
        }
        private void DrawMandelbrot()
        {
            int width = PbxMandalaLienzo.Width;
            int height = PbxMandalaLienzo.Height;
            Bitmap bitmap = new Bitmap(width, height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    double real = (x - width / 2.0) * 4.0 / width;
                    double imag = (y - height / 2.0) * 4.0 / height;

                    double zReal = 0;
                    double zImag = 0;
                    int iterations = 0;

                    while (zReal * zReal + zImag * zImag < 4 && iterations < 1000)
                    {
                        double temp = zReal * zReal - zImag * zImag + real;
                        zImag = 2 * zReal * zImag + imag;
                        zReal = temp;
                        iterations++;
                    }

                    Color color = Color.FromArgb(iterations % 256, iterations % 128, iterations % 64);
                    bitmap.SetPixel(x, y, color);
                }
            }

            PbxMandalaLienzo.Image = bitmap;
        }
        private void DrawMandelbrotP(int power)
        {
            int width = PbxMandalaLienzo.Width;
            int height = PbxMandalaLienzo.Height;
            Bitmap bitmap = new Bitmap(width, height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    double real = (x - width / 2.0) * 4.0 / width;
                    double imag = (y - height / 2.0) * 4.0 / height;

                    double zReal = 0;
                    double zImag = 0;
                    int iterations = 0;

                    while (zReal * zReal + zImag * zImag < 4 && iterations < 1000)
                    {
                        double temp = Math.Pow(zReal, power) - Math.Pow(zImag, power) + real;
                        zImag = 2 * zReal * zImag + imag;
                        zReal = temp;
                        iterations++;
                    }

                    Color color = Color.FromArgb(iterations % 256, iterations % 128, iterations % 64);
                    bitmap.SetPixel(x, y, color);
                }
            }

            PbxMandalaLienzo.Image = bitmap;
        }
        private void DrawCustomPowerFractal(int power)
        {
            int width = PbxMandalaLienzo.Width;
            int height = PbxMandalaLienzo.Height;
            Bitmap bitmap = new Bitmap(width, height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    double real = (x - width / 2.0) * 4.0 / width;
                    double imag = (y - height / 2.0) * 4.0 / height;

                    double zReal = 0;
                    double zImag = 0;
                    int iterations = 0;

                    while (zReal * zReal + zImag * zImag < 4 && iterations < 1000)
                    {
                        double zRealTemp = zReal;
                        zReal = Math.Pow(zReal, power) - Math.Pow(zImag, power) + real;
                        zImag = power * zRealTemp * Math.Pow(zRealTemp, power - 1) * zImag + imag;
                        iterations++;
                    }

                    Color color = Color.FromArgb(iterations % 256, iterations % 128, iterations % 64);
                    bitmap.SetPixel(x, y, color);
                }
            }

            PbxMandalaLienzo.Image = bitmap;
        }
        //------------------------------------------------------------------------- 
        //BOTONES
        //------------------------------------------------------------------------- 
        private void BtnMandelBrot_Click(object sender, EventArgs e)
        {
            float valor1 = (float)NumAncho1.Value;
            float valor2 = (float)NumAncho2.Value;
            float valor3 = (float)NumAlto1.Value;
            float valor4 = (float)NumAlto2.Value;

            MandelBrotSet(valor1, valor2, valor3, valor4);
        }
        private void BtnMandelBrotExp_Click(object sender, EventArgs e)
        {
            DrawCustomPowerFractal(CbxPotencias.SelectedIndex + 1); 
            /*
            int p = CbxPotencias.TabIndex + 1;
            MandelBrot((int)NumFactalIteracion.Value, p);
            */
        }
        private void BtnBorrar_Click(object sender, EventArgs e)
        {
            Graphics g = TabFiguras.CreateGraphics();
            g.Clear(Color.White);

            PbxMandalaLienzo.Image = null;
        }
        //-------------------------------------------------------------------------
        //VENTANA PAINT
        //Es la app de paint en una vantana que hara las funciones principales como
        //Dibujar, Borrar y seleccionar color
        //------------------------------------------------------------------------- 
        //Variables globales
        private Point puntoAnterior;
        private bool dibujando = false;

        private bool UsarLapiz = false;
        //FUNCIONES
        //------------------------------------------------------------------------- 
        public void Lapiz(Point valor1, Point valor2)
        {
            //Creamos una pluma para dibujar
            Pen Pluma;
            Color color = Color.Black;

            using(Graphics g = TabPaint.CreateGraphics())
            {
                Pluma = new Pen(color, TrackLapizTamano.Value);
                g.DrawLine(Pluma, valor1, valor2);
            }
        }
        //------------------------------------------------------------------------- 
        //EVENTOS
        //-------------------------------------------------------------------------  
        private void TabPaint_MouseMove(object sender, MouseEventArgs e)
        {
            if (dibujando)
            {
                Lapiz(puntoAnterior, e.Location);
                puntoAnterior = e.Location;
            }
        }
        private void TabPaint_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && UsarLapiz == true)
            {
                dibujando = true;
                puntoAnterior = e.Location;
            }
        }
        private void TabPaint_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                dibujando = false;
            }
        }
        //------------------------------------------------------------------------- 
        //BOTONES
        //-------------------------------------------------------------------------  
        private void BtnLapiz_Click(object sender, EventArgs e)
        {
            UsarLapiz = !UsarLapiz;
            TabPaint.Cursor = Cursors.Cross;
            if (UsarLapiz == false) {TabPaint.Cursor = Cursors.Default;}
        }
        private void BtnBorrador_Click(object sender, EventArgs e)
        {
            TabPaint.Invalidate();
        }
        private void BtnRectangulo_Click(object sender, EventArgs e)
        {

        }
        private void BtnCirculo_Click(object sender, EventArgs e)
        {

        }
        private void BtnTriangulo_Click(object sender, EventArgs e)
        {

        }
        private void BtnExportar_Click(object sender, EventArgs e)
        {

        }
        //-------------------------------------------------------------------------
        //VENTANA POLIGONOS
        //Dibujamos poligonos con la 
        //Mandelbrot 
        //------------------------------------------------------------------------- 
        //FUNCIONES
        //------------------------------------------------------------------------- 
        public void Poligonos(int X, int Y)
        {
            //Colorear colores aleatorios
            Random rgb = new Random();
            
            Font f = new Font("Arial", 12.0f);
            Brush brush = new SolidBrush(Color.Blue);

            //Creamos una pluma para dibujar
            Pen Pluma;
            Color color = Color.FromArgb(rgb.Next(0, 255), 
                                         rgb.Next(0, 255), 
                                         rgb.Next(0, 255));

            //Le indicamos donde vamos a dibujar
            Graphics g = TabPoligonos.CreateGraphics();

            //Le indicamos las cordenadas
            PointF p1 = new PointF(100.0f + X, -100.0f + Y);
            PointF p2 = new PointF(259.869f + X, -54.52f + Y);
            PointF p3 = new PointF(424.593f + X, -101.937f + Y); 
            PointF p4 = new PointF(483.179f + X, -251.583f + Y);
            PointF p5 = new PointF(329.925f + X, -197.340f + Y);
            PointF p6 = new PointF(233.929f + X, -258.241f + Y);
            PointF p7 = new PointF(129.781f + X, -204.533f + Y);
            PointF p8 = new PointF(136.551f + X, -160.046f + Y);

            PointF[] points = new PointF[] {p1,p2,p3,p4,p5,p6,p7,p8};
            
            Pluma = new Pen(color, rgb.Next(2, 6));

            g.DrawPolygon(Pluma, points);

            //Puntos con strings
            g.DrawString("P1",f,brush,p1);
            g.DrawString("P2",f,brush,p2);
            g.DrawString("P3",f,brush,p3);
            g.DrawString("P4",f,brush,p4);
            g.DrawString("P5",f,brush,p5);
            g.DrawString("P6",f,brush,p6);
            g.DrawString("P7",f,brush,p7);
            g.DrawString("P8",f,brush,p8);
        }
        private void BtnDibujarPoligono_Click(object sender, EventArgs e)
        {
            Poligonos((int)NumPY.Value, (int)NumPX.Value);
        }
    }
}
