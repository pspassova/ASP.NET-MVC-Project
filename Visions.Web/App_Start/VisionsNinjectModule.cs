using Ninject.Extensions.Interception.Infrastructure.Language;
using Ninject.Modules;
using Ninject.Web.Common;
using Visions.Data;
using Visions.Data.Contracts;
using Visions.Data.Interceptors;
using Visions.Helpers;
using Visions.Helpers.Contracts;
using Visions.Services;
using Visions.Services.Contracts;
using Visions.Web.Helpers;
using Visions.Web.Helpers.Contracts;

namespace Visions.Web.App_Start
{
    public class VisionsNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IVisionsDbContext>().To<VisionsDbContext>().InRequestScope();

            this.Bind(typeof(IEfRepository<>)).To(typeof(EfRepository<>));

            this.Bind<IPhotoHelper>().To<PhotoHelper>();
            this.Bind<ITagsHelper>().To<TagsHelper>();

            this.Bind<IPhotoUploader>().To<PhotoUploader>();

            this.Bind<IPhotoService>().To<PhotoService>();
            this.Bind<ITagService>().To<TagService>();
            this.Bind<IArticleService>().To<ArticleService>();

            var uploadServiceBinding = this.Bind(typeof(IUploadService<>)).To(typeof(UploadService<>));
            uploadServiceBinding.Intercept().With<SaveChangesInterceptor>();

            var modifyServiceBinding = this.Bind(typeof(IModifyService<>)).To(typeof(ModifyService<>));
            modifyServiceBinding.Intercept().With<SaveChangesInterceptor>();

            var deleteServiceBinding = this.Bind(typeof(IDeleteService<>)).To(typeof(DeleteService<>));
            deleteServiceBinding.Intercept().With<SaveChangesInterceptor>();
        }
    }
}