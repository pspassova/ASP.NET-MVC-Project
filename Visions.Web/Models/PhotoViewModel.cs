using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
                    CreatedOn = photo.CreatedOn,
                    IsDeleted = photo.IsDeleted
                };
            }
        }

        public Guid Id
        {
            get; set;
        }

        [DisplayName("User ID")]
        public string UserId
        {
            get; set;
        }

        [Required]
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

        [DisplayName("Created on")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? CreatedOn
        {
            get; set;
        }

        public bool IsDeleted
        {
            get; set;
        }
    }
}