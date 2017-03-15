using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Visions.Models.Models
{
    public class Article
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [Required]
        [MinLength(2)]
        public string Title { get; set; }

        [Required]
        [MinLength(2)]
        public string Content { get; set; }

        public ICollection<string> Tags { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}