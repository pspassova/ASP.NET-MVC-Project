using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Visions.Models.Models;

namespace Visions.Web.Models
{
    public class PhotoViewModel
    {
        public static Expression<Func<Photo, PhotoViewModel>> FromPhoto
        {
            get
            {
                return photo => new PhotoViewModel
                {
                    Id = photo.Id,
                    UserId = photo.UserId,
                    Path = photo.Path,
                    Likes = photo.Likes,
                    Tags = photo.Tags,
                    CreatedOn = photo.CreatedOn
                };
            }
        }

        public Guid Id
        {
            get; set;
        }

        public string UserId
        {
            get; set;
        }

        public string Path
        {
            get; set;
        }

        public int Likes
        {
            get; set;
        }

        public ICollection<Tag> Tags
        {
            get; set;
        }

        public DateTime? CreatedOn
        {
            get; set;
        }
    }
}