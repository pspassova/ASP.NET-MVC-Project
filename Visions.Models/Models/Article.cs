using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Visions.Models.Models
{
    public class Article
    {
        private ICollection<Tag> tags;

        public Article()
        {
            this.tags = new HashSet<Tag>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id
        {
            get; set;
        }

        [Required]
        [MinLength(2)]
        public string Title
        {
            get; set;
        }

        [Required]
        [MinLength(2)]
        public string Content
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

        public bool IsDeleted
        {
            get; set;
        }

        public DateTime? CreatedOn
        {
            get; set;
        }

        public ICollection<Tag> Tags
        {
            get; set;
        }
    }
}