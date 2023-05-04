using Neptuno2022EF.Entidades.Dtos.Categoria;
using Neptuno2022EF.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuno2022EF.Datos.Interfaces
{
    public interface IRepositorioCategorias
    {
        List<CategoriaListDto> GetCategorias();
        void Agregar(Categoria categoria);
        bool Existe(Categoria categoria);
        void Editar(Categoria categoria);
        void Borrar(int categoriaId);
        bool EstaRelacionada(Categoria categoria);
        Categoria GetCategoriaPorId(int categoriaId);
    }
}
