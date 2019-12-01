using AdventureWorks.DAL;
using AdventureWorks.DAL.GenericRepository;
using AdventureWorks.DAL.UnitOfWork;
using AdventureWorks.Services;
using Autofac;
using Autofac.Integration.WebApi;
using System.Data.Entity;
using System.Reflection;
using System.Web.Http;

namespace AdventureWorks.WebAPI.AutoFacConfiguration
{
    public class AutofacWebapiConfig
    {

        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }


        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            //Register your Web API controllers.  
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<AdventureWorks2017Entities>()
                   .As<DbContext>()
                   .InstancePerRequest();


            _ = builder.RegisterGeneric(typeof(BaseRepository<>))
                .As(typeof(IRepository<>))
                .InstancePerRequest();

            builder.RegisterType<DbFactory>()
                .As<IDbFactory>()
                .InstancePerRequest();

            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerRequest();

            builder.RegisterType<ProductService>()
                .As<IProductService>()
                .InstancePerRequest();


            builder.RegisterType<PurchasingService>()
                .As<IPurchasingService>()
                .InstancePerRequest();

            Container = builder.Build();

            return Container;
        }

    }
}