using Neptuno2022EF.Datos.Interfaces;
using Neptuno2022EF.Entidades.Dtos.Categoria;
using Neptuno2022EF.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuno2022EF.Datos.Repositorios
{
    public class RepositorioCategorias:IRepositorioCategorias
    {
        private readonly NeptunoDbContext _context;

        public RepositorioCategorias(NeptunoDbContext context)
        {
            _context = context;
        }

        public void Agregar(Categoria categoria)
        {
            try
            {
                
                _context.Categorias.Add(categoria);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Borrar(int categoriaId)
        {
            try
            {
                var categoriaInDb = GetCategoriaPorId(categoriaId);
                if (categoriaInDb == null)
                {
                    throw new Exception("Registro dado de baja por otro usuario");
                }
                else
                {
                    _context.Entry(categoriaInDb).State = EntityState.Deleted;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Editar(Categoria categoria)
        {
            try
            {
                if (categoria.NombreCategoria != null)
                {
                    _context.Categorias.Attach(categoria);

                }
                var categoriaInDb = GetCategoriaPorId(categoria.CategoriaId);
                if (categoriaInDb == null)
                {
                    throw new Exception("Registro dado de baja por otro usuario");
                }
                _context.Entry(categoria).State = EntityState.Modified;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool EstaRelacionada(Categoria categoria)
        {
            try
            {
                return _context.Productos.Any(p => p.CategoriaId == categoria.CategoriaId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(Categoria categoria)
        {
            try
            {
                if (categoria.CategoriaId == 0)
                {
                    return _context.Categorias.Any(c => c.NombreCategoria == categoria.NombreCategoria);

                }
                return _context.Categorias.Any(c => c.NombreCategoria == categoria.NombreCategoria
                            && c.CategoriaId != categoria.CategoriaId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<CategoriaListDto> GetCategorias()
        {
            return _context.Categorias.Include(c => c.NombreCategoria)
                .Select(c => new CategoriaListDto
                {
                    CategoriaId = c.CategoriaId,
                    NombreCategoria = c.NombreCategoria,
                    Descripcion = c.Descripcion
                }).AsNoTracking()
                .ToList();
        }

        public Categoria GetCategoriaPorId(int categoriaId)
        {
            try
            {
                return _context.Categorias.SingleOrDefault(c => c.CategoriaId == categoriaId);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
