using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace blogmvc.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        //propiedades que añadire a la tabla Users de Identity
        public string user_url { get; set; }
        //este campo hay que manejarlo a la hora de registrar los usuarios que almacene en que fecha y hora se registraron los usuarios
        public DateTime user_registered { get; set; }
        //datos que añadimos a la tabla Users de prueba que seguramente probare para ver si recrea la DB y luego seran añadidos a la version final de la app
        public string nombre { get; set; }
        public virtual ICollection<Post> Post { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }
        public virtual ICollection<Option> Option { get; set; }
        

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")//, throwIfV1Schema: false)
        {
            //Database.SetInitializer<ApplicationDbContext>(new CreateDatabaseIfNotExists<ApplicationDbContext>());
            //Database.SetInitializer<ApplicationDbContext>(new DropCreateDatabaseIfModelChanges<ApplicationDbContext>());
            Database.SetInitializer<ApplicationDbContext>(new BlogInit());
            //Database.SetInitializer<ApplicationDbContext>(new DropCreateDatabaseAlways<ApplicationDbContext>());
            
        }

        private string[] databases = { "SQLServer", "PostgreSQL", "MySQL" };
        public string DatabaseCheck { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Option> Option { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<IdentityUser>().ToTable("Users");
            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            //definir clave externa en api fluida esta definida como clave que no se genera automaticamente ahora la necesito como viene por defecto
            //modelBuilder.Entity<Option>().Property(t => t.OptionID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
        //metodo Create() lo mete en el codigo porque quiere
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public string CheckDb()
        {
            DatabaseCheck = databases[0];
            return DatabaseCheck;
        }

    }
}