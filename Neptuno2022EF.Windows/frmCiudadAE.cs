using Neptuno2022EF.Entidades.Entidades;
using Neptuno2022EF.Windows.Helpers;
using System;
using System.Windows.Forms;

namespace Neptuno2022EF.Windows
{
    public partial class frmCiudadAE : Form
    {
        private Ciudad ciudad;
        public frmCiudadAE()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CombosHelper.CargarComboPaises(ref cboPaises);
            if (ciudad != null)
            {
                txtCiudad.Text = ciudad.NombreCiudad;
                cboPaises.SelectedValue = ciudad.PaisId;
            }
        }
        public Ciudad GetCiudad()
        {
            return ciudad;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                if (ciudad == null)
                {
                    ciudad = new Ciudad();
                }
                ciudad.NombreCiudad = txtCiudad.Text;
                //ciudad.Pais = (Pais)cboPaises.SelectedItem;
                ciudad.PaisId = (int)cboPaises.SelectedValue;

                
                

                DialogResult = DialogResult.OK;
            }
        }

        private bool ValidarDatos()
        {
            bool valido = true;
            errorProvider1.Clear();
            if (cboPaises.SelectedIndex == 0)
            {
                valido = false;
                errorProvider1.SetError(cboPaises, "Debe seleccionar un país");

            }
            if (string.IsNullOrEmpty(txtCiudad.Text))
            {
                valido = false;
                errorProvider1.SetError(txtCiudad, "Nombre de la Ciudad es requerido");
            }
            return valido;
        }

        public void SetCiudad(Ciudad ciudad)
        {
            this.ciudad = ciudad;
        }

        private void cboPaises_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void frmCiudadAE_Load(object sender, EventArgs e)
        {

        }
    }
}
