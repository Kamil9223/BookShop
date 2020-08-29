using AuthService.Repositories;
using AuthService.Services.Implementations;
using AuthService.Services.Interfaces;
using Core.IRepositories;
using Unity;

namespace AuthService.Infrastructure
{
    public class AuthServiceStartup
    {
        private readonly IUnityContainer unityContainer;

        public AuthServiceStartup(IUnityContainer unityContainer)
        {
            this.unityContainer = unityContainer;
        }

        public void RegisterServices()
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
