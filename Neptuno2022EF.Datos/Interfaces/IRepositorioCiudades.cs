using Neptuno2022EF.Entidades.Dtos.Ciudad;
using Neptuno2022EF.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuno2022EF.Datos.Interfaces
{
    public interface IRepositorioCiudades
    {
        List<CiudadListDto> GetCiudades();
        void Agregar(Ciudad ciudad);
        bool Existe(Ciudad ciudad);
        void Editar(Ciudad ciudad);
        void Borrar(int ciudadId);
        bool EstaRelacionada(Ciudad ciudad);
        Ciudad GetCiudadPorId(int ciudadId);
    }
}
