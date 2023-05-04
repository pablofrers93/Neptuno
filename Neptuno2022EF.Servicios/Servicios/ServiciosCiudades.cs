using Neptuno2022EF.Datos;
using Neptuno2022EF.Datos.Interfaces;
using Neptuno2022EF.Datos.Repositorios;
using Neptuno2022EF.Entidades.Dtos.Ciudad;
using Neptuno2022EF.Entidades.Entidades;
using Neptuno2022EF.Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuno2022EF.Servicios.Servicios
{
    public class ServiciosCiudades : IServiciosCiudades
    {
        private readonly IRepositorioCiudades _repitorioCiudades;
        private readonly IUnitOfWork _unitOfWork;


        public ServiciosCiudades(IRepositorioCiudades repitorioCiudades, IUnitOfWork unitOfWork)
        {
            _repitorioCiudades = repitorioCiudades;
            _unitOfWork = unitOfWork;
        }

        public void Borrar(int ciudadId)
        {
            try
            {
                _repitorioCiudades.Borrar(ciudadId);
                _unitOfWork.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool EstaRelacionada(Ciudad ciudad)
        {
            try
            {
                return _repitorioCiudades.EstaRelacionada(ciudad);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(Ciudad ciudad)
        {
            try
            {
                return _repitorioCiudades.Existe(ciudad);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<CiudadListDto> GetCiudades()
        {
            try
            {
                return _repitorioCiudades.GetCiudades();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Ciudad GetCiudadPorId(int ciudadId)
        {
            try
            {
                return _repitorioCiudades.GetCiudadPorId(ciudadId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Guardar(Ciudad ciudad)
        {
            try
            {
                if (ciudad.CiudadId==0)
                {
                    _repitorioCiudades.Agregar(ciudad);
                }
                else
                {
                    _repitorioCiudades.Editar(ciudad);
                }
                _unitOfWork.SaveChanges();
                ciudad=GetCiudadPorId(ciudad.CiudadId);

            }
            catch (Exception)
            {

                throw;
            }
        }

        
    }
}
