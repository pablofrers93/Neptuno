using Neptuno2022EF.Datos.Interfaces;
using Neptuno2022EF.Datos;
using Neptuno2022EF.Entidades.Dtos.Categoria;
using Neptuno2022EF.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neptuno2022EF.Servicios.Interfaces;

namespace Neptuno2022EF.Servicios.Servicios
{
    public class ServiciosCategorias:IServiciosCategorias
    {
        private readonly IRepositorioCategorias _repitorioCategorias;
        private readonly IUnitOfWork _unitOfWork;


        public ServiciosCategorias(IRepositorioCategorias repitorioCategorias, IUnitOfWork unitOfWork)
        {
            _repitorioCategorias = repitorioCategorias;
            _unitOfWork = unitOfWork;
        }

        public void Borrar(int categoriaId)
        {
            try
            {
                _repitorioCategorias.Borrar(categoriaId);
                _unitOfWork.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool EstaRelacionada(Categoria categoria)
        {
            try
            {
                return _repitorioCategorias.EstaRelacionada(categoria);
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
                return _repitorioCategorias.Existe(categoria);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<CategoriaListDto> GetCategorias()
        {
            try
            {
                return _repitorioCategorias.GetCategorias();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Categoria GetCategoriaPorId(int categoriaId)
        {
            try
            {
                return _repitorioCategorias.GetCategoriaPorId(categoriaId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Guardar(Categoria categoria)
        {
            try
            {
                if (categoria.CategoriaId == 0)
                {
                    _repitorioCategorias.Agregar(categoria);
                }
                else
                {
                    _repitorioCategorias.Editar(categoria);
                }
                _unitOfWork.SaveChanges();
                categoria = GetCategoriaPorId(categoria.CategoriaId);

            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
