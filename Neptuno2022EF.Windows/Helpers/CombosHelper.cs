using Neptuno2022EF.Entidades.Entidades;
using Neptuno2022EF.Ioc;
using Neptuno2022EF.Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Neptuno2022EF.Windows.Helpers
{
    public static class CombosHelper
    {
        public static void CargarComboPaises(ref ComboBox combo)
        {
            IServiciosPaises _servicio=DI.Create<IServiciosPaises>();
            var lista = _servicio.GetPaises();
            var defaultPais = new Pais
            {
                PaisId = 0,
                NombrePais = "Seleccione País"
            };
            lista.Insert(0,defaultPais);
            combo.DataSource = lista;
            combo.ValueMember = "PaisId";
            combo.DisplayMember = "NombrePais";
            combo.SelectedIndex = 0;
        }
        public static void CargarComboCategorias(ref ComboBox combo)
        {
            IServiciosCategorias _servicio = DI.Create<IServiciosCategorias>();
            var lista = _servicio.GetCategorias();
            var defaultCategoria = new Categoria
            {
                CategoriaId = 0,
                NombreCategoria = "Seleccione Categoria"
            };
            //lista.Insert(0, defaultCategoria);
            combo.DataSource = lista;
            combo.ValueMember = "CategoriaId";
            combo.DisplayMember = "NombreCategoria";
            combo.SelectedIndex = 0;
        }
    }
}
