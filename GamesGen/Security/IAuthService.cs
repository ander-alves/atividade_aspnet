using GamesGen.Model;

namespace GamesGen.Security
{
    public interface IAuthService
    {
        Task<UserLogin> Autenticar(UserLogin userLogin);
    }
}
