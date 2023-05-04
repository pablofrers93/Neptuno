using Neptuno2022EF.Entidades.Entidades;
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
    public partial class frmSeleccionarPais : Form
    {
        public frmSeleccionarPais()
        {
            InitializeComponent();
        }
        private Pais pais;
        public Pais GetPais()
        {
            return pais;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult=DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {

            }
        }

        private bool ValidarDatos()
        {
            bool valido = true;
            errorProvider1.Clear();

            if(cboPaises.SelectedIndex==0)
            {
                valido = false;
                errorProvider1.SetError(cboPaises, "Debe seleccionar un país");
            }
            return valido;
        }

        private void frmSeleccionarPais_Load(object sender, EventArgs e)
        {

        }
    }
}
