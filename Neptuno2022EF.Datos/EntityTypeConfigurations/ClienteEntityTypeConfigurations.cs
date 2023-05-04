using NuevaAppComercial2022.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuno2022EF.Datos.EntityTypeConfigurations
{
    public class ClienteEntityTypeConfigurations:EntityTypeConfiguration<Cliente>
    {
        public ClienteEntityTypeConfigurations()
        {
            ToTable("Clientes");
        }
    }
}
