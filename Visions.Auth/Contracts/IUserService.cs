using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Visions.Models.Models;

namespace Visions.Auth.Contracts
{
    public interface IUserService
    {
        Task<IdentityResult> CreateAsync(User user);

        Task<IdentityResult> CreateAsync(User user, string password);
    }
}
