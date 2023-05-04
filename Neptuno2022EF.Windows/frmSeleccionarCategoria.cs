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
    public partial class frmSeleccionarCategoria : Form
    {
        public frmSeleccionarCategoria()
        {
            InitializeComponent();
        }

        private void frmSeleccionarCategoria_Load(object sender, EventArgs e)
        {

        }
        private Categoria categoria;
        public Categoria GetCategoria()
        {
            return categoria;
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

            if (cboCategorias.SelectedIndex == 0)
            {
                valido = false;
                errorProvider1.SetError(cboCategorias, "Debe seleccionar una categoria");
            }
            return valido;
        }
    }
}
