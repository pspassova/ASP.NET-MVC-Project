using System.Collections.Generic;
using Visions.Models.Models;

namespace Visions.Helpers.Contracts
{
    public interface ITagsConvertHelper
    {
        ICollection<Tag> CreateTags(string tagsTexts);
    }
}
