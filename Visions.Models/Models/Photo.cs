using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Visions.Models.Models
{
    public class Photo
    {
        private ICollection<Tag> tags;

        public Photo()
        {
            this.tags = new HashSet<Tag>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id
        {
            get; set;
        }

        [Required]
        public string UserId
        {
            get; set;
        }

        public virtual User User
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

        public bool IsDeleted
        {
            get; set;
        }

        public DateTime? CreatedOn
        {
            get; set;
        }

        public virtual ICollection<Tag> Tags
        {
            get
            {
                return this.tags;
            }
            set
            {
                this.tags = value;
            }
        }
    }
}