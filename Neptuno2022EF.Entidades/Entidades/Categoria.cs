using Neptuno2022EF.Entidades.Dtos.Categoria;
using Neptuno2022EF.Entidades.Dtos.Ciudad;
using System;

namespace Neptuno2022EF.Entidades.Entidades
{
    public class Categoria : ICloneable
    {
        public int CategoriaId { get; set; }
        public string NombreCategoria { get; set; }
        public string Descripcion { get; set; }
        public byte[] RowVersion { get; set; }
        public object Clone()
        {
            return MemberwiseClone();
        }

        public CategoriaListDto ToCategoriaListDto()
        {
            return new CategoriaListDto()
            {
                CategoriaId = this.CategoriaId,
                NombreCategoria = this.NombreCategoria,
                Descripcion = this.Descripcion
            };
        }
    }
}
