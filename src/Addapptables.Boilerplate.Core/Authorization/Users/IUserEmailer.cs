using System.Threading.Tasks;

namespace Addapptables.Boilerplate.Authorization.Users
{
    public interface IUserEmailer
    {
        Task SendPasswordResetLinkAsync(User user, string link = null);
    }
}
