using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Object13.DataLayer.Context
{
    public class Object13Context : DbContext
    {
        public Object13Context(DbContextOptions<Object13Context> options):base(options)
        {
        }

        #region Tables

        

        #endregion

        #region ManageCascade-OnModelCreateing
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            var cascades = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascades)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}
