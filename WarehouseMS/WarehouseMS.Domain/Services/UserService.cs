using AutoMapper;
using WarehouseMS.Domain.Dtos.UserDtos;
using WarehouseMS.Domain.Interfaces;
using WarehouseMS.Infrastructure.Context.Entities;
using WarehouseMS.Infrastructure.Interfaces;

namespace WarehouseMS.Domain.Services;

public class UserService : IUserService
{
    private IUserRepository _userRepository;
    private IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetUserDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<GetUserDto>>(users);
    }

    public async Task<GetUserDto?> GetUserByIdAsync(int userId)
    {
        var user = await _userRepository.GetAllAsync();
        return _mapper.Map<GetUserDto>(user);
    }

    public async Task<GetUserDto?> GetUserByEmailAsync(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);

        if (user is null)
            throw new ArgumentNullException(nameof(user));

        return _mapper.Map<GetUserDto>(user);
    }

    public async Task<GetUserDto> CreateUserAsync(CreateUserDto user)
    {
        var userEntity = _mapper.Map<User>(user);
        var result = await _userRepository.InsertAsync(userEntity);

        return _mapper.Map<GetUserDto>(result);
    }

    public Task<bool> UpdateUserAsync(CreateUserDto user)
    {
        var userEntity = _mapper.Map<User>(user);
        var result = _userRepository.UpdateAsync(userEntity);

        return result;
    }

    public async Task<bool> DeleteUserAsync(int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user is null)
            return false;

        var result = await _userRepository.DeleteAsync(user);
        return result;
    }
}