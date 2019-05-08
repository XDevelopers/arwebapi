using AltaRail.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AltaRail.Application.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetUsers();
        Task<UserDto> GetUser(string id);
        Task EditUser(EditUserDto user);
        Task CreateUser(CreateUserDto user);
        Task<bool> Authenticate(string username, string password);

    }
}
