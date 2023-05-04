﻿using Neptuno2022EF.Entidades.Dtos.Ciudad;
using Neptuno2022EF.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuno2022EF.Servicios.Interfaces
{
    public interface IServiciosCiudades
    {
        List<CiudadListDto> GetCiudades();
        bool Existe(Ciudad ciudad);
        void Guardar(Ciudad ciudad);
        bool EstaRelacionada(Ciudad ciudad);
        Ciudad GetCiudadPorId(int ciudadId);
        void Borrar(int ciudadId);
    }
}
