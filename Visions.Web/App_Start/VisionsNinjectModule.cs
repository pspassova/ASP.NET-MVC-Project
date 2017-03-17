using Ninject.Extensions.Interception.Infrastructure.Language;
using Ninject.Modules;
using Visions.Data;
using Visions.Data.Contracts;
using Visions.Data.Interceptors;
using Visions.Services;
using Visions.Services.Contracts;

namespace Visions.Web.App_Start
{
    public class VisionsNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IVisionsDbContext>().To<VisionsDbContext>().InSingletonScope();

            this.Bind(typeof(IEfRepository<>)).To(typeof(EfRepository<>));

            this.Bind<IPhotoService>().To<PhotoService>();

            var uploadServiceBinding = this.Bind(typeof(IUploadService<>)).To(typeof(UploadService<>));
            uploadServiceBinding.Intercept().With<SaveChangesInterceptor>();
        }
    }
}