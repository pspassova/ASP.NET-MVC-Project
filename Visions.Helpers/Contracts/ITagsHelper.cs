using System.Collections.Generic;
using Visions.Models.Models;

namespace Visions.Helpers.Contracts
{
    public interface ITagsHelper
    {
        ICollection<Tag> CreateTags(string tagsTexts);
    }
}
