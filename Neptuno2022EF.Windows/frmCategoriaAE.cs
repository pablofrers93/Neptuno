using Neptuno2022EF.Entidades.Entidades;
using Neptuno2022EF.Windows.Helpers;
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
    public partial class frmCategoriaAE : Form
    {
        private Categoria categoria;
        public frmCategoriaAE()
        {
            InitializeComponent();
        }

        private void frmCategoriaAE_Load(object sender, EventArgs e)
        {

        }
        public void SetCategoria(Categoria categoria)
        {
            this.categoria = categoria;
        }
        public Categoria GetCategoria()
        {
            return categoria;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CombosHelper.CargarComboCategorias(ref cboCategorias);
            if (categoria != null)
            {
                DescripcionTextBox.Text = categoria.Descripcion;
                CategoriaTextBox.Text = categoria.NombreCategoria;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                if (categoria == null)
                {
                    categoria = new Categoria();
                }
                categoria.NombreCategoria = CategoriaTextBox.Text;
                categoria.Descripcion = DescripcionTextBox.Text;
                DialogResult = DialogResult.OK;
            }
        }
        private bool ValidarDatos()
        {
            bool valido = true;
            errorProvider1.Clear();
            if (string.IsNullOrEmpty(CategoriaTextBox.Text))
            {
                valido = false;
                errorProvider1.SetError(CategoriaTextBox, "Nombre de la categoria es requerida");
            }
            if (string.IsNullOrEmpty(DescripcionTextBox.Text))
            {
                valido = false;
                errorProvider1.SetError(DescripcionTextBox, "La descripcion de la categoria es requerida");
            }
            return valido;
        }
    }
}
