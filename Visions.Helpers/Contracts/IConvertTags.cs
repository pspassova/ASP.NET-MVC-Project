using System.Collections.Generic;
using Visions.Models.Models;

namespace Visions.Helpers.Contracts
{
    public interface IConvertTags
    {
        IEnumerable<string> SeparateTagsTexts(string tagsTexts);

        ICollection<Tag> CreateTags(string tagsTexts);
    }
}
