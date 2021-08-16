using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Object13.DataLayer.Models.Access;
using Object13.DataLayer.Models.Account;
using Object13.DataLayer.Models.Orders;
using Object13.DataLayer.Models.Product;
using Object13.DataLayer.Models.SiteUtilites;

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

        public DbSet<Slider> TblSliders { get; set; }
        public DbSet<Product> TblProducts { get; set; }
        public DbSet<ProductGallery> TblProductGalleries { get; set; }
        public DbSet<ProductVisit> TblProductVisits { get; set; }
        public DbSet<ProductCategory> TblProductCategories { get; set; }
        public DbSet<ProductSelectedCategory> TblProductSelectedCategories { get; set; }
        public DbSet<ProductComment> TblProductComments { get; set; }
        public DbSet<Order> TblOrders { get; set; }
        public DbSet<OrderDetail> TblOrderDetails { get; set; }
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
