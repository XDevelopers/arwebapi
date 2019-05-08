using AltaRail.Application.DTOs;
using AltaRail.Application.Exceptions;
using AltaRail.Application.Services;
using AltaRail.Domain;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AltaRail.Application
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, ITokenService tokenService, IMapper mapper)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<UserDto> GetUser(string id)
        {
            var user = await _userRepository.GetAsync(id);
            var userDto = _mapper.Map<User, UserDto>(user);
            return userDto;
        }

        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            var users = await _userRepository.GetAllAsync();
            var usersMapped = _mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(users);
            return usersMapped;
        }

        public async Task CreateUser(CreateUserDto userDto)
        {
            var exists = await _userRepository.ExistsAsync(e => e.Username == userDto.Username);
            if (exists)
                throw new UserExistsException();

            var user = _mapper.Map<CreateUserDto, User>(userDto);

            //hash password before inserting
            await _userRepository.AddAsync(user);
        }

        public async Task EditUser(EditUserDto editUser)
        {
            var user = await _userRepository.GetAsync(editUser.Id);
            user.Name = editUser.Name;
            user.Lastname = editUser.Lastname;

            await _userRepository.UpdateAsync(user.Id, user);
        }

        public async Task<bool> Authenticate(string username, string password)
        {
            //this password should be hashed
            return await _userRepository.Authenticate(username, password);
        }            
    }
}
