using Ninject.Extensions.Factory;
using Ninject.Modules;
using Ninject.Web.Common;
using Visions.Data;
using Visions.Data.Contracts;
using Visions.Data.Factories;
using Visions.Services;
using Visions.Services.Contracts;

namespace Visions.Web.App_Start
{
    public class VisionsNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IVisionsDbContext>().To<VisionsDbContext>();

            this.Bind(typeof(IEfRepository<>)).To(typeof(EfRepository<>)).InRequestScope();
            this.Bind<IUnitOfWork>().To<UnitOfWork>();
            
            this.Bind(typeof(IStateful<>)).To(typeof(Stateful<>)).InRequestScope();
            this.Bind<IStatefulFactory>().ToFactory().InSingletonScope();
            
            this.Bind<IPhotoService>().To<PhotoService>();
        }
    }
}