﻿using WarehouseMS.Domain.Dtos.UserDtos;

namespace WarehouseMS.Domain.Interfaces;

public interface IUserService
{
    Task<IEnumerable<GetUserDto>> GetAllUsersAsync();
    Task<GetUserDto?> GetUserByIdAsync(int userId);
    Task<GetUserDto?> GetUserByEmailAsync(string email);
    Task<GetUserDto> CreateUserAsync(CreateUserDto user);
    Task<GetUserDto> CreateManagerAsync(CreateUserDto user);
    Task<bool> UpdateUserAsync(CreateUserDto user);
    Task<bool> DeleteUserAsync(int userId);
}