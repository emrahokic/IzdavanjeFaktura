using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IzdavanjeFaktura.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
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
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer<ApplicationDbContext>(new ProfileInitializer<ApplicationDbContext>());
        }

        public DbSet<Faktura> Faktura { get; set; }
        public DbSet<FakturaStavka> FakturaStavka { get; set; }
        public DbSet<Stavka> Stavka { get; set; }
        public DbSet<Porez> Porez { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        private class ProfileInitializer<T> : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
        {
            protected override void Seed(ApplicationDbContext context)
            {
                context.Stavka.Add(new Models.Stavka()
                {
                    StavkaID = 1,
                    Cijena = 999,
                    Naziv = "Intel I7 Procesor",
                    Opis = "Procesor nove generacije"
                });
                context.Porez.Add(new Models.Porez()
                {
                    PorezID = 1,
                    Naziv = "BiH PDV 17%",
                    Iznos = 17
                });
                context.Porez.Add(new Models.Porez()
                {
                    PorezID = 2,

                    Naziv = "HR PDV 25%",
                    Iznos = 25
                });
                base.Seed(context);
            }
        }
    }
}