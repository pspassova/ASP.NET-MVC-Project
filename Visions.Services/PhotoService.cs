using Bytes2you.Validation;
using System;
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

        public Photo Create(string userId, string path, ICollection<Tag> tags)
        {
            return new Photo()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Path = path,
                Likes = 0,
                CreatedOn = DateTime.UtcNow,
                Tags = tags
            };
        }

        public IQueryable<Photo> GetAll()
        {
            return this.repository.GetAll();
        }

        public IQueryable<Photo> GetAllForUser(string userId)
        {
            return this.repository.GetAll().Where(photo => photo.UserId == userId);
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
