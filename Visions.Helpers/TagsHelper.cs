using System;
using System.Collections.Generic;
using Visions.Helpers.Contracts;
using Visions.Models.Models;
using Visions.Services.Contracts;

namespace Visions.Helpers
{
    public class TagsHelper : ITagsHelper
    {
        private readonly ITagService tagService;

        public TagsHelper(ITagService tagService)
        {
            this.tagService = tagService;
        }

        public ICollection<Tag> CreateTags(string tagsTexts)
        {
            ICollection<Tag> tags = new List<Tag>();
            IEnumerable<string> separatedTagsTexts = this.SeparateTagsTexts(tagsTexts);
            foreach (var tagText in separatedTagsTexts)
            {
                Tag tag = this.tagService.Create(tagText);
                tags.Add(tag);
            }

            return tags;
        }

        private IEnumerable<string> SeparateTagsTexts(string tagsTexts)
        {
            return tagsTexts.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
