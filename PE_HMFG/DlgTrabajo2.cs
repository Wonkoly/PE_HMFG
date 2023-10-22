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

            DesabilitarControles();

        }
        private void DesabilitarControles()
        {
            GbxHerramientasPain.Visible = false;
            GbxBotonesMandel.Visible = false;
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
            Reactangulo(e.X, e.Y);
        }
        private void PnlPintar_MouseMove(object sender, MouseEventArgs e)
        {
            MouseEventArgs mouse;
            mouse = (MouseEventArgs)e;
            Reactangulo(mouse.X, mouse.Y);    
        }
        //-------------------------------------------------------------------------
        //BOTONES
        //------------------------------------------------------------------------- 

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
    }
}
