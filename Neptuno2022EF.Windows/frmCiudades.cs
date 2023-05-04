using Neptuno2022EF.Entidades.Dtos.Ciudad;
using Neptuno2022EF.Entidades.Entidades;
using Neptuno2022EF.Servicios.Interfaces;
using Neptuno2022EF.Servicios.Servicios;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Neptuno2022EF.Windows
{
    public partial class frmCiudades : Form
    {
        private readonly IServiciosCiudades _servicio;

        public frmCiudades(IServiciosCiudades servicio)
        {
            InitializeComponent();
            _servicio = servicio;
        }


        private void tsbCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private List<CiudadListDto> lista;

        private void MostrarDatosEnGrilla()
        {
            dgvDatos.Rows.Clear();
            foreach (CiudadListDto ciudad in lista)
            {
                DataGridViewRow r = ConstruirFila();
                SetearFila(r, ciudad);
                AgregarFila(r);
            }
        }

        private void AgregarFila(DataGridViewRow r)
        {
            dgvDatos.Rows.Add(r);
        }

        private void SetearFila(DataGridViewRow r, CiudadListDto ciudad)
        {
            r.Cells[colCiudad.Index].Value = ciudad.NombreCiudad;
            r.Cells[colPais.Index].Value = ciudad.NombrePais;

            r.Tag = ciudad;
        }

        private DataGridViewRow ConstruirFila()
        {
            var r = new DataGridViewRow();
            r.CreateCells(dgvDatos);
            return r;
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmCiudadAE frm = new frmCiudadAE() { Text = "Agregar Ciudad" };
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel) return;
            try
            {

                var ciudad = frm.GetCiudad();
                if (!_servicio.Existe(ciudad))
                {
                    _servicio.Guardar(ciudad);
                    var r = ConstruirFila();
                    SetearFila(r, ciudad.ToCiudadListDto());
                    AgregarFila(r);
                    MessageBox.Show("Registro agregado satisfactoriamente", "Mensaje",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {

                    MessageBox.Show("Registro Duplicado!!!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            catch (Exception)
            {

                MessageBox.Show("Error al intentar agregar un registro", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                CiudadListDto ciudadDto = (CiudadListDto)r.Tag;
                DialogResult dr = MessageBox.Show($"¿Confirma la baja de {ciudadDto.NombreCiudad}?",
                    "Confirmar",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.No)
                {
                    return;
                }
                var ciudad = _servicio.GetCiudadPorId(ciudadDto.CiudadId);
                if (ciudad == null)
                {
                    MessageBox.Show("Registro dado de baja por otro usuario", "Mensaje",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    RecargarGrilla();
                    return;
                }
                if (!_servicio.EstaRelacionada(ciudad))
                {
                    _servicio.Borrar(ciudad.CiudadId);
                    QuitarFila(r);
                    MessageBox.Show("Registro borrado satisfactoriamente!!!",
                        "Mensaje",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Registro relacionado...Baja denegada", "Mensaje",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje",
                         MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

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
            CiudadListDto ciudadDto = (CiudadListDto)r.Tag;
            var ciudad = _servicio.GetCiudadPorId(ciudadDto.CiudadId);
            if (ciudad==null)
            {
                MessageBox.Show("Registro dado de baja por otro usuario");
                RecargarGrilla();
                return;

            }
            var ciudadCopia = (Ciudad)ciudad.Clone();
            try
            {

                frmCiudadAE frm = new frmCiudadAE() { Text = "Editar Ciudad" };
                frm.SetCiudad(ciudad);
                DialogResult dr = frm.ShowDialog(this);
                if (dr == DialogResult.Cancel) { return; }

                ciudad = frm.GetCiudad();
                if (!_servicio.Existe(ciudad))
                {
                    _servicio.Guardar(ciudad);
                    SetearFila(r, ciudad.ToCiudadListDto());
                    MessageBox.Show("Registro editado satisfactoriamente!!",
                        "Mensaje",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);


                }
                else
                {
                    MessageBox.Show("Registro Duplicado!!", "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    SetearFila(r, ciudadCopia.ToCiudadListDto());
                }



            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message,
                        "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetearFila(r, ciudadCopia.ToCiudadListDto());


            }
        }

        private void frmCiudades_Load(object sender, EventArgs e)
        {
            RecargarGrilla();

        }

        private void RecargarGrilla()
        {
            try
            {
                lista = _servicio.GetCiudades();
                MostrarDatosEnGrilla();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void tsbFiltrar_Click(object sender, EventArgs e)
        {
            frmSeleccionarPais frm = new frmSeleccionarPais() { Text = "Seleccionar..." };
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel) { return; }
            try
            {
                var paisSeleccionado = frm.GetPais();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
