using Neptuno2022EF.Entidades.Dtos.Categoria;
using Neptuno2022EF.Entidades.Dtos.Ciudad;
using Neptuno2022EF.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuno2022EF.Servicios.Interfaces
{
    public interface IServiciosCategorias
    {
        List<CategoriaListDto> GetCategorias();
        bool Existe(Categoria categoria);
        void Guardar(Categoria categoria);
        bool EstaRelacionada(Categoria categoria);
        Categoria GetCategoriaPorId(int categoriaId);
        void Borrar(int categoriaId);
    }
}
