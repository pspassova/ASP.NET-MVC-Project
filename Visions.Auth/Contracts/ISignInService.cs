using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using Visions.Models.Models;

namespace Visions.Auth.Contracts
{
    public interface ISignInService
    {
        Task<SignInStatus> PasswordSignInAsync(string email, string password, bool isPersistent, bool shouldLockout);

        Task SignInAsync(User user, bool isPersistent, bool rememberBrowser);
    }
}
