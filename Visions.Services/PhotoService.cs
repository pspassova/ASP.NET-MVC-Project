using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using Visions.Data.Contracts;
using Visions.Models.Models;
using Visions.Services.Contracts;
using Visions.Services.Enumerations;

namespace Visions.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IEfDbSetWrapper<Photo> repository;

        public PhotoService(IEfDbSetWrapper<Photo> repository)
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

        public IQueryable<Photo> GetAll()
        {
            return this.repository.All;
        }

        public IQueryable<Photo> GetAllByUserId(string userId)
        {
            if (userId == "")
            {
                return this.repository.All;
            }
            else
            {
                return this.repository.All.Where(x => x.UserId == userId);
            }
        }

        public IQueryable<Photo> GetAllOrderedByCreatedOn(OrderBy? order, string userId = "")
        {
            if (userId == string.Empty)
            {
                if (order == null || order == OrderBy.Ascending)
                {
                    return this.repository.All;
                }
                else
                {
                    return this.repository.All.OrderByDescending(x => x.CreatedOn);
                }
            }
            else
            {
                if (order == null || order == OrderBy.Ascending)
                {
                    return this.repository.All.Where(x => x.UserId == userId);
                }
                else
                {
                    return this.repository.All.Where(x => x.UserId == userId).OrderByDescending(x => x.CreatedOn);
                }
            }
        }

        public IQueryable<Photo> SortByTag(string tag, string userId = "")
        {
            if (tag == null)
            {
                if (userId == string.Empty)
                {
                    return this.GetAll();
                }
                else
                {
                    return this.GetAllByUserId(userId);
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
                    photos = this.GetAllByUserId(userId);
                }

                IList<Photo> matchingPhotos = new List<Photo>();
                foreach (var photo in photos)
                {
                    if (photo.Tags.Count > 0)
                    {
                        foreach (var photoTag in photo.Tags)
                        {
                            if (photoTag.Text.Contains(tag))
                            {
                                matchingPhotos.Add(photo);
                                break;
                            }
                        }
                    }
                }

                return matchingPhotos.AsQueryable();
            }
        }
    }
}
