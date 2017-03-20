﻿using Ninject.Extensions.Interception.Infrastructure.Language;
using Ninject.Modules;
using Visions.Data;
using Visions.Data.Contracts;
using Visions.Data.Interceptors;
using Visions.Helpers;
using Visions.Helpers.Contracts;
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

            this.Bind<IPhotoUploadHelper>().To<PhotoUploadHelper>();
            this.Bind<ITagsConvertHelper>().To<TagsConvertHelper>();

            this.Bind<IPhotoService>().To<PhotoService>();
            this.Bind<ITagService>().To<TagService>();
            this.Bind<IArticleService>().To<ArticleService>();

            var uploadServiceBinding = this.Bind(typeof(IUploadService<>)).To(typeof(UploadService<>));
            uploadServiceBinding.Intercept().With<SaveChangesInterceptor>();
        }
    }
}