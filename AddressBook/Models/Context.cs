using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace AddressBook.Models
{
    public class Context : DbContext
    {
        static Context()
        {
            Database.SetInitializer<Context>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();


            base.OnModelCreating(modelBuilder);
        }


        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Name> Names { get; set; }
        public DbSet<Group> Groups { get; set; }
    }
}