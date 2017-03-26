using Bytes2you.Validation;
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

        private const int MinTagTextLength = 2;

        public TagsHelper(ITagService tagService)
        {
            Guard.WhenArgument(tagService, "tagService").IsNull().Throw();

            this.tagService = tagService;
        }

        public ICollection<Tag> CreateTags(string tagsTexts)
        {
            IEnumerable<string> separatedTagsTexts = new List<string>();
            if (tagsTexts != null)
            {
                separatedTagsTexts = this.SeparateTagsTexts(tagsTexts);
            }

            ICollection<Tag> tags = new List<Tag>();

            foreach (var tagText in separatedTagsTexts)
            {
                if (tagText.Length >= MinTagTextLength)
                {
                    Tag tag = this.tagService.Create(tagText);
                    tags.Add(tag);
                }
            }

            return tags;
        }

        private IEnumerable<string> SeparateTagsTexts(string tagsTexts)
        {
            return tagsTexts.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
