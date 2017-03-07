using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Visions.Models.Models
{
    public class Photo
    {
        public Guid Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        [Required]
        public string Path { get; set; }

        public int Likes { get; set; }

        public ICollection<string> Tags { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}