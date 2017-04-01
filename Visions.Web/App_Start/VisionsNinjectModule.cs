using Ninject.Modules;
using Ninject.Web.Common;
using System.Web;
using Visions.Auth;
using Visions.Auth.Contracts;
using Visions.Data;
using Visions.Data.Contracts;
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
            // Server
            this.Bind<HttpServerUtilityBase>().ToMethod(x => new HttpServerUtilityWrapper(HttpContext.Current.Server)).InRequestScope();
            this.Bind<HttpContextBase>().ToMethod(x => new HttpContextWrapper(HttpContext.Current)).InRequestScope();

            // Data
            this.Bind<IEfDbContext>().To<EfDbContext>().InRequestScope();
            this.Bind<IEfDbContextSaveChanges>().To<EfDbContextSaveChanges>();

            this.Bind(typeof(IEfDbSetWrapper<>)).To(typeof(EfDbSetWrapper<>));

            // Services
            this.Bind<IPhotoService>().To<PhotoService>();
            this.Bind<ITagService>().To<TagService>();
            this.Bind<IArticleService>().To<ArticleService>();

            // Interceptors
            this.Bind(typeof(IUploadService<>)).To(typeof(UploadService<>));
            this.Bind(typeof(IModifyService<>)).To(typeof(ModifyService<>));
            this.Bind(typeof(IDeleteService<>)).To(typeof(DeleteService<>));
            
            // Auth
            this.Bind<IUserProvider>().To<UserProvider>();

            // Helpers
            this.Bind<IPhotoHelper>().To<PhotoHelper>();
            this.Bind<ITagsHelper>().To<TagsHelper>();

            // Common
            this.Bind<IPhotoUploader>().To<PhotoUploader>();
            this.Bind<IPhotoConverter>().To<PhotoConverter>();
            this.Bind(typeof(IPagingProvider<>)).To(typeof(PagingProvider<>));
        }
    }
}