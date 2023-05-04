using Neptuno2022EF.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuno2022EF.Datos.EntityTypeConfigurations
{
    public class PaisEntityTypeConfigurations:EntityTypeConfiguration<Pais>
    {
        public PaisEntityTypeConfigurations()
        {
            ToTable("Paises");
            Property(p=>p.RowVersion).IsRowVersion().IsConcurrencyToken();
        }
    }
}
