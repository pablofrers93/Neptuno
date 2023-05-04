using Neptuno2022EF.Datos.Interfaces;
using Neptuno2022EF.Entidades.Dtos.Ciudad;
using Neptuno2022EF.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuno2022EF.Datos.Repositorios
{
    public class RepositorioCiudades : IRepositorioCiudades
    {
        private readonly NeptunoDbContext _context;

        public RepositorioCiudades(NeptunoDbContext context)
        {
            _context = context;
        }

        public void Agregar(Ciudad ciudad)
        {
            try
            {
                if (ciudad.Pais!=null)
                {
                    _context.Entry(ciudad.Pais).State = EntityState.Unchanged;
                }
                _context.Ciudades.Add(ciudad);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Borrar(int ciudadId)
        {
            try
            {
                var ciudadInDb = GetCiudadPorId(ciudadId);
                if (ciudadInDb==null)
                {
                    throw new Exception("Registro dado de baja por otro usuario");
                }
                else
                {
                    _context.Entry(ciudadInDb).State = EntityState.Deleted;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Editar(Ciudad ciudad)
        {
            try
            {
                if (ciudad.Pais!=null)
                {
                    _context.Paises.Attach(ciudad.Pais);

                }
                var ciudadInDb = GetCiudadPorId(ciudad.CiudadId);
                if (ciudadInDb== null)
                {
                    throw new Exception("Registro dado de baja por otro usuario");
                }
                _context.Entry(ciudad).State = EntityState.Modified;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool EstaRelacionada(Ciudad ciudad)
        {
            try
            {
                return _context.Clientes.Any(c=>c.CiudadId==ciudad.CiudadId) 
                    || _context.Proveedores.Any(p=>p.CiudadId == ciudad.CiudadId);
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
                if(ciudad.CiudadId== 0)
                {
                    return _context.Ciudades.Any(c => c.NombreCiudad == ciudad.NombreCiudad
                        && c.PaisId == ciudad.PaisId);

                }
                return _context.Ciudades.Any(c => c.NombreCiudad == ciudad.NombreCiudad
                            && c.PaisId == ciudad.PaisId 
                            && c.CiudadId!=ciudad.CiudadId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<CiudadListDto> GetCiudades()
        {
            return _context.Ciudades.Include(c=>c.Pais)
                .Select(c=>new CiudadListDto
                {
                    CiudadId=c.CiudadId,
                    NombreCiudad=c.NombreCiudad,
                    NombrePais=c.Pais.NombrePais
                }).AsNoTracking()
                .ToList();
        }

        public Ciudad GetCiudadPorId(int ciudadId)
        {
            try
            {
                return _context.Ciudades.Include(c=>c.Pais)
                    .AsNoTracking()
                    .SingleOrDefault(c=>c.CiudadId==ciudadId);  
                
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
