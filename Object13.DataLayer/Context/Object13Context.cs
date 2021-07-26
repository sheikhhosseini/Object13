using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Object13.DataLayer.Models.Access;
using Object13.DataLayer.Models.Account;

namespace Object13.DataLayer.Context
{
    public class Object13Context : DbContext
    {
        public Object13Context(DbContextOptions<Object13Context> options):base(options)
        {
        }

        #region Tables
        public DbSet<User> TblUsers { get; set; }
        public DbSet<Role> TblRoles { get; set; }
        public DbSet<UserRole> TblUserRoles { get; set; }
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
