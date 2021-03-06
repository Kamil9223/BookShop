﻿using AuthService.DTO;
using System.Threading.Tasks;

namespace AuthService.Services.Interfaces
{
    public interface IUserService
    {
        Task Register(string login, string password, string email, AddressInformations addressDto = null);
        Task<AuthenticationResult> Login(string login, string password);
        Task<AuthenticationResult> RefreshConnection(string jwtToken, string refreshToken);
        Task Logout();
        Task<UserInformations> Get(string login);
    }
}
