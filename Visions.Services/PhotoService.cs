using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Visions.Data.Contracts;
using Visions.Models.Models;
using Visions.Services.Contracts;
using Visions.Services.Enumerations;

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

        public Photo GetById(Guid id)
        {
            return this.repository.GetById(id);
        }

        public IEnumerable<Photo> GetAll()
        {
            return this.GetAll(null);
        }

        public IEnumerable<Photo> GetAll(string userId)
        {
            if (userId == null)
            {
                return this.repository.GetAll();
            }
            else
            {
                return this.repository.GetAll(photo => photo.UserId == userId);
            }
        }

        public IEnumerable<T1> GetAll<T, T1>(string userId, Expression<Func<Photo, T>> orderByProperty, OrderBy? order, Expression<Func<Photo, T1>> selectAs)
        {
            IQueryable<Photo> result = this.repository.All;

            if (userId != null)
            {
                result = result.Where(photo => photo.UserId == userId);
            }

            if (orderByProperty != null)
            {
                if (order == OrderBy.Ascending || order == null)
                {
                    result = result.OrderBy(orderByProperty);
                }
                else if (order == OrderBy.Descending)
                {
                    result = result.OrderByDescending(orderByProperty);
                }
            }

            if (selectAs != null)
            {
                return result.Select(selectAs).ToList();
            }
            else
            {
                return result.OfType<T1>().ToList();
            }
        }

        public IEnumerable<Photo> SortByTag(string tag, string userId = "")
        {
            if (tag == null)
            {
                if (userId == string.Empty)
                {
                    return this.GetAll();
                }
                else
                {
                    return this.GetAll(userId);
                }
            }
            else
            {
                IEnumerable<Photo> photos = new List<Photo>();
                if (userId == string.Empty)
                {
                    photos = this.GetAll();
                }
                else
                {
                    photos = this.GetAll(userId);
                }

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
}
