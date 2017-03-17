using Bytes2you.Validation;
using System.Collections.Generic;
using System.Linq;
using Visions.Data.Contracts;
using Visions.Models.Models;
using Visions.Services.Contracts;

namespace Visions.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IEfRepository<Photo> repository;

        public PhotoService(IEfRepository<Photo> repository)
        {
            Guard.WhenArgument(repository, "repository").IsNull().Throw();

            this.repository = repository;
        }

        public IQueryable<Photo> GetAll()
        {
            return this.repository.GetAll();
        }

        public IEnumerable<Photo> SortByTag(string tag)
        {
            if (tag == null)
            {
                tag = string.Empty;
            }

            IEnumerable<Photo> photos = this.repository.GetAll();

            ICollection<Photo> matchingPhotos = new List<Photo>();
            foreach (var photo in photos)
            {
                if (photo.Tags.Count > 0)
                {
                    foreach (var photoTag in photo.Tags)
                    {
                        if (photoTag.Text.Contains(tag))
                        {
                            matchingPhotos.Add(photo);
                        }
                    }
                }
            }

            return matchingPhotos;
        }
    }
}
