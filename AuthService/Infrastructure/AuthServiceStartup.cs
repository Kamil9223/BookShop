using AuthService.Repositories;
using AuthService.Services.Implementations;
using AuthService.Services.Interfaces;
using Core.IRepositories;
using Unity;

namespace AuthService.Infrastructure
{
    public class AuthServiceStartup
    {
        public static void RegisterServices(IUnityContainer unityContainer)
        {
            unityContainer.RegisterType<IUserRepository, UserRepository>();
            unityContainer.RegisterType<ILoggedUserRepository, LoggedUserRepository>();
            unityContainer.RegisterType<IUserService, UserService>();
            unityContainer.RegisterType<IJwtHelper, JwtHelper>();
            unityContainer.RegisterSingleton<IJwtService, JwtService>();
            unityContainer.RegisterSingleton<IEncrypter, Encrypter>();
        }
    }
}
