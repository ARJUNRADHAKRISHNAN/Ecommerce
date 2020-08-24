namespace Ecommerce.Model
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class EcommerceDB : DbContext
    {
      
        public EcommerceDB()
            : base("name=EcommerceDB")
        {
        }


        // public virtual DbSet<MyEntity> MyEntities { get; set; }
         public virtual DbSet<User> Users { get; set; }
         public virtual DbSet<UserSession> UserSessions { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Subcategory> Subcategories { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Itemfeatures> Itemfeaturess { get; set; }
        public virtual DbSet<Gallery> Gallerys { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Newlaunch> Newlaunchs { get; set; }
        public virtual DbSet<Newlaunchproduct> Newlaunchproducts { get; set; }
    }

    
}