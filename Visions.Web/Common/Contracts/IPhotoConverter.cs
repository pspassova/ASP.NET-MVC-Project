using Visions.Models.Models;
using Visions.Web.Models;

namespace Visions.Web.Common.Contracts
{
    public interface IPhotoConverter
    {
        PhotoViewModel ConvertToViewModel(Photo photo);
    }
}
