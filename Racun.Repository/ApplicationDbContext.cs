using IzdavanjeFaktura.Models.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzdavanjeFaktura.Repository
{
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
                context.Stavka.Add(new Models.Models.Stavka()
                {
                    StavkaID = 1,
                    Cijena = 999,
                    Naziv = "Intel I7 Procesor",
                    Opis = "Procesor nove generacije"
                });
                context.Porez.Add(new Models.Models.Porez()
                {
                    PorezID = 1,
                    Naziv = "BiH PDV 17%",
                    Iznos = 17
                });
                context.Porez.Add(new Models.Models.Porez()
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
