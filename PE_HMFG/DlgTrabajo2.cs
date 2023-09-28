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
    public partial class DlgTrabajo2 : Form
    {
        //Variables globales
        DlgTrabajo1 funcs = new DlgTrabajo1();

        public DlgTrabajo2()
        {
            InitializeComponent();
        }

        private void DlgTrabajo2_FormClosing(object sender, FormClosingEventArgs e)
        {
            funcs.CerrarVentana();
        }
    }
}
