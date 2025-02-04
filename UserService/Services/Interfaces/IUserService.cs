using UserService.Database.Entities;
using UserService.Models;

namespace UserService.Services.Interfaces;

public interface IUserService
{
    Task<UserResponse> PostUser(UserRegisterDto user);
    Task<UserResponse> GetUser(string userName, string password);
    Task<UserResponse> GetUser(Guid userId);
}