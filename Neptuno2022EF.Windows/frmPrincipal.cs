using Neptuno2022EF.Ioc;
using Neptuno2022EF.Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Neptuno2022EF.Windows
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnPaises_Click(object sender, EventArgs e)
        {
            frmPaises frm=new frmPaises(DI.Create<IServiciosPaises>());
            frm.ShowDialog();
        }

        private void btnCiudades_Click(object sender, EventArgs e)
        {
            frmCiudades frm=new frmCiudades(DI.Create<IServiciosCiudades>());
            frm.ShowDialog(this);
        }

        private void btnCategorias_Click(object sender, EventArgs e)
        {
            frmCategorias frm = new frmCategorias(DI.Create<IServiciosCategorias>());
            frm.ShowDialog(this);
        }
    }
}
