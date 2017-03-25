using Ninject.Extensions.Interception.Infrastructure.Language;
using Ninject.Modules;
using Ninject.Web.Common;
using System.Web;
using Visions.Auth;
using Visions.Auth.Contracts;
using Visions.Data;
using Visions.Data.Contracts;
using Visions.Data.Interceptors;
using Visions.Helpers;
using Visions.Helpers.Contracts;
using Visions.Services;
using Visions.Services.Contracts;
using Visions.Web.Common;
using Visions.Web.Common.Contracts;

namespace Visions.Web.App_Start
{
    public class VisionsNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<HttpServerUtilityBase>().ToMethod(c => new HttpServerUtilityWrapper(HttpContext.Current.Server));

            // Visions.Data
            this.Bind<IVisionsDbContext>().To<VisionsDbContext>().InRequestScope();

            this.Bind(typeof(IEfRepository<>)).To(typeof(EfRepository<>));

            // Services
            this.Bind<IPhotoService>().To<PhotoService>();
            this.Bind<ITagService>().To<TagService>();
            this.Bind<IArticleService>().To<ArticleService>();

            // Interceptors
            var uploadServiceBinding = this.Bind(typeof(IUploadService<>)).To(typeof(UploadService<>)).InRequestScope();
            uploadServiceBinding.Intercept().With<SaveChangesInterceptor>();

            var modifyServiceBinding = this.Bind(typeof(IModifyService<>)).To(typeof(ModifyService<>)).InRequestScope();
            modifyServiceBinding.Intercept().With<SaveChangesInterceptor>();

            var deleteServiceBinding = this.Bind(typeof(IDeleteService<>)).To(typeof(DeleteService<>)).InRequestScope();
            deleteServiceBinding.Intercept().With<SaveChangesInterceptor>();

            // Visions.Auth
            this.Bind<IUserProvider>().To<UserProvider>();

            // Helpers
            this.Bind<IPhotoHelper>().To<PhotoHelper>();
            this.Bind<ITagsHelper>().To<TagsHelper>();

            // Common
            this.Bind<IPhotoUploader>().To<PhotoUploader>();
            this.Bind<IPhotoConverter>().To<PhotoConverter>();
        }
    }
}