using System.Threading.Tasks;
using $safeprojectname$.Models;
using Optional;

namespace $safeprojectname$.Services
{
    public interface IUsersService
    {
        Task<Option<JwtModel, Error>> Login(LoginUserModel model);

        Task<Option<UserModel, Error>> Register(RegisterUserModel model);
    }
}
