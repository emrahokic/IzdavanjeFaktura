using IzdavanjeFaktura.Controllers;
using IzdavanjeFaktura.Models;
using IzdavanjeFaktura.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Mvc5;

namespace IzdavanjeFaktura
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();


            //MojiServisi
            container.RegisterType<IFakturaService, FakturaService>();
            container.RegisterType<IPorezService, PorezService>();
            container.RegisterType<IStavkaService, StavkaService>();

            //Identity
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>();
            container.RegisterType<UserManager<ApplicationUser>>();
            container.RegisterType<DbContext, ApplicationDbContext>();
            container.RegisterType<ApplicationUserManager>();
            container.RegisterType<AccountController>(new InjectionConstructor());

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}