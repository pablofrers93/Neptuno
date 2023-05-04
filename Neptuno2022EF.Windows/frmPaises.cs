using Neptuno2022EF.Entidades.Entidades;
using Neptuno2022EF.Servicios.Interfaces;
using Neptuno2022EF.Servicios.Servicios;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Neptuno2022EF.Windows
{
    public partial class frmPaises : Form
    {
        private List<Pais> lista;
        private readonly IServiciosPaises _servicio;
        public frmPaises(IServiciosPaises servicio)
        {
            InitializeComponent();
            _servicio =servicio;
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmPaisAE frm = new frmPaisAE() { Text = "Agregar País" };
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel) return;
            try
            {
                var pais = frm.GetPais();
                if (!_servicio.Existe(pais))
                {
                    _servicio.Guardar(pais);

                    var r = ConstruirFila();
                    SetearFila(r, pais);
                    AgregarFila(r);
                    MessageBox.Show("Registro agregado satisfactoriamente", "Mensaje",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);


                }
                else
                {
                    MessageBox.Show("Error registro duplicado!!!", "Error",
                         MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception)
            {

                MessageBox.Show("Error al intentar agregar un registro", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void frmPaises_Load(object sender, EventArgs e)
        {
            RecargarGrilla();
        }

        private void MostrarDatosEnGrilla()
        {
            dgvDatos.Rows.Clear();
            foreach (Pais pais in lista)
            {
                DataGridViewRow r = ConstruirFila();
                SetearFila(r, pais);
                AgregarFila(r);
            }
        }

        private void AgregarFila(DataGridViewRow r)
        {
            dgvDatos.Rows.Add(r);
        }

        private void SetearFila(DataGridViewRow r, Pais pais)
        {
            r.Cells[colPais.Index].Value = pais.NombrePais;

            r.Tag = pais;
        }

        private DataGridViewRow ConstruirFila()
        {
            var r = new DataGridViewRow();
            r.CreateCells(dgvDatos);
            return r;
        }

        private void tsbBorrar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            try
            {
                var r = dgvDatos.SelectedRows[0];
                Pais pais = (Pais)r.Tag;
                DialogResult dr = MessageBox.Show($"¿Confirma la baja de {pais.NombrePais}?",
                    "Confirmar",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.No)
                {
                    return;
                }
                if (!_servicio.EstaRelacionado(pais))
                {
                    _servicio.Borrar(pais.PaisId);
                    QuitarFila(r);
                    MessageBox.Show("Registro borrado satisfactoriamente!!!",
                        "Mensaje",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Registro relacionado",
                        "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message,
                    "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                RecargarGrilla();

            }

        }

        private void QuitarFila(DataGridViewRow r)
        {
            dgvDatos.Rows.Remove(r);
        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            var r = dgvDatos.SelectedRows[0];
            Pais pais = (Pais)r.Tag;
            Pais paisClone = (Pais)pais.Clone();
            try
            {
                frmPaisAE frm = new frmPaisAE() { Text = "Editar Pais" };
                frm.SetPais(pais);
                DialogResult dr = frm.ShowDialog(this);
                if (dr == DialogResult.No) { return; }
                pais = frm.GetPais();
                if (!_servicio.Existe(pais))
                {
                    _servicio.Guardar(pais);
                    SetearFila(r, pais);
                    MessageBox.Show("Registro editado satisfactoriamente!!!",
                        "Mensaje",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    SetearFila(r, paisClone);
                    MessageBox.Show("Error registro duplicado!!",
                        "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                        "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (ex.Message.Contains("modificado o borrado"))
                {
                    var paisInDb = _servicio.GetPaisPorId(pais.PaisId);
                    if (paisInDb!=null)
                    {
                        SetearFila(r, paisInDb);
                    }
                    else
                    {
                        RecargarGrilla();

                    }
                }
                else
                {
                    SetearFila(r, paisClone);

                }

            }

        }

        private void RecargarGrilla()
        {
            try
            {
                lista = _servicio.GetPaises();
                MostrarDatosEnGrilla();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void tsbCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
