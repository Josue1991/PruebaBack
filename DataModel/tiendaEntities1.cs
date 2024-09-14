using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Remoting.Contexts;

namespace DataModel
{
    public class tiendaEntities1 : DbContext
    {
        public tiendaEntities1(DbContextOptions<tiendaEntities1> options)
            : base(options)
        {
        }

        // Define tus DbSets para las entidades aquí
        public DbSet<Evento> Evento { get; set; }
    }
}
