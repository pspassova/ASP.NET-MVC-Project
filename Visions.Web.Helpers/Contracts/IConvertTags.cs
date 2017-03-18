using System.Collections.Generic;
using Visions.Models.Models;

namespace Visions.Web.Helpers.Contracts
{
    public interface IConvertTags
    {
        IEnumerable<string> SeparateTagsTexts(string tagsTexts);

        ICollection<Tag> CreateTags(string tagsTexts);
    }
}
