using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Visions.Models.Models
{
    public class Tag
    {
        private ICollection<Photo> photos;

        public Tag()
        {
            this.photos = new HashSet<Photo>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id
        {
            get; set;
        }

        [Required]
        [MinLength(2)]
        public string Text
        {
            get; set;
        }

        public virtual ICollection<Photo> Photos
        {
            get
            {
                return this.photos;
            }
            set
            {
                this.photos = value;
            }
        }
    }
}
