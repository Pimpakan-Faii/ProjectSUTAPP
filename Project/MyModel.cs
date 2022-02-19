using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Project
{
    public partial class MyModel : DbContext
    {
        public MyModel()
            : base("name=MyModel")
        {
        }

        public virtual DbSet<AdminUser> AdminUser { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Coupon> Coupon { get; set; }
        public virtual DbSet<Safe> Safe { get; set; }
        public virtual DbSet<Store> Store { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Store>()
                .HasMany(e => e.Comment)
                .WithOptional(e => e.Store)
                .HasForeignKey(e => e.Store_Id);
        }
    }
}
