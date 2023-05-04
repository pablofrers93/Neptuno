using System;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Neptuno2022EF.Datos
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NeptunoDbContext _context;

        public UnitOfWork(NeptunoDbContext context)
        {
            _context = context;
        }

        public void SaveChanges()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                //saveFailed = true;
                ex.Entries.ToList()
                     .ForEach(entry =>
                     {
                         //entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                         //entry.CurrentValues.SetValues(entry.GetDatabaseValues());
                         entry.Reload();

                     });

                throw new Exception("Registro modificado o borrado por otro usuario");


            }
            catch (Exception ex)
            {

                if (ex.InnerException != null && ex.InnerException.InnerException != null)
                {
                    if (ex.InnerException.InnerException.Message.Contains("REFERENCE"))
                    {
                        throw new Exception("Registro relacionado\nBaja denegada");
                    }
                    else if (ex.InnerException.InnerException.Message.Contains("IX"))
                    {
                        throw new Exception("Registro repetido\nAlta o edición denegada");

                    }
                }
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
