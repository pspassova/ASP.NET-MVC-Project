using System.Collections.Generic;
using Visions.Models.Models;

namespace Visions.Helpers.Contracts
{
    public interface IConvertTags
    {
        ICollection<Tag> CreateTags(string tagsTexts);
    }
}
