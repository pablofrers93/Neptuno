using Neptuno2022EF.Entidades.Dtos.Categoria;
using Neptuno2022EF.Entidades.Dtos.Ciudad;
using Neptuno2022EF.Entidades.Entidades;
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
    public partial class frmCategorias : Form
    {
        private readonly IServiciosCategorias _servicio;
        public frmCategorias()
        {
            InitializeComponent();
        }

        private void frmCategorias_Load(object sender, EventArgs e)
        {
            RecargarGrilla();
        }

        private void RecargarGrilla()
        {
            try
            {
                lista = _servicio.GetCategorias();
                MostrarDatosEnGrilla();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public frmCategorias(IServiciosCategorias servicio)
        {
            InitializeComponent();
            _servicio = servicio;
        }

        private void tsbCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private List<CategoriaListDto> lista;

        private void MostrarDatosEnGrilla()
        {
            dgvDatos.Rows.Clear();
            foreach (CategoriaListDto categoria in lista)
            {
                DataGridViewRow r = ConstruirFila();
                SetearFila(r, categoria);
                AgregarFila(r);
            }
        }

        private void AgregarFila(DataGridViewRow r)
        {
            dgvDatos.Rows.Add(r);
        }

        private void SetearFila(DataGridViewRow r, CategoriaListDto categoria)
        {
            r.Cells[colNombre.Index].Value = categoria.NombreCategoria;
            r.Cells[colDescripcion.Index].Value = categoria.Descripcion;

            r.Tag = categoria;
        }

        private DataGridViewRow ConstruirFila()
        {
            var r = new DataGridViewRow();
            r.CreateCells(dgvDatos);
            return r;
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmCategoriaAE frm = new frmCategoriaAE() { Text = "Agregar Categoria" };
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel) return;
            try
            {

                var categoria = frm.GetCategoria();
                if (!_servicio.Existe(categoria))
                {
                    _servicio.Guardar(categoria);
                    var r = ConstruirFila();
                    SetearFila(r, categoria.ToCategoriaListDto());
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
                CategoriaListDto categoriaDto = (CategoriaListDto)r.Tag;
                DialogResult dr = MessageBox.Show($"¿Confirma la baja de {categoriaDto.NombreCategoria}?",
                    "Confirmar",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.No)
                {
                    return;
                }
                var categoria = _servicio.GetCategoriaPorId(categoriaDto.CategoriaId);
                if (categoria == null)
                {
                    MessageBox.Show("Registro dado de baja por otro usuario", "Mensaje",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    RecargarGrilla();
                    return;
                }
                if (!_servicio.EstaRelacionada(categoria))
                {
                    _servicio.Borrar(categoria.CategoriaId);
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
            CategoriaListDto categoriaDto = (CategoriaListDto)r.Tag;
            var categoria = _servicio.GetCategoriaPorId(categoriaDto.CategoriaId);
            if (categoria == null)
            {
                MessageBox.Show("Registro dado de baja por otro usuario");
                RecargarGrilla();
                return;

            }
            var categoriaCopia = (Categoria)categoria.Clone();
            try
            {

                frmCategoriaAE frm = new frmCategoriaAE() { Text = "Editar Categoria" };
                frm.SetCategoria(categoria);
                DialogResult dr = frm.ShowDialog(this);
                if (dr == DialogResult.Cancel) { return; }

                categoria = frm.GetCategoria();
                if (!_servicio.Existe(categoria))
                {
                    _servicio.Guardar(categoria);
                    SetearFila(r, categoria.ToCategoriaListDto());
                    MessageBox.Show("Registro editado satisfactoriamente!!",
                        "Mensaje",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);


                }
                else
                {
                    MessageBox.Show("Registro Duplicado!!", "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    SetearFila(r, categoriaCopia.ToCategoriaListDto());
                }



            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message,
                        "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetearFila(r, categoriaCopia.ToCategoriaListDto());
            }
        }

        private void tsbFiltrar_Click(object sender, EventArgs e)
        {
            frmSeleccionarCategoria frm = new frmSeleccionarCategoria() { Text = "Seleccionar..." };
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel) { return; }
            try
            {
                var categoriaSeleccionada = frm.GetCategoria();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
