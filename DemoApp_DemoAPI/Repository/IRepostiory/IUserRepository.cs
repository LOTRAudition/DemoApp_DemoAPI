using DemoApp_DemoAPI.Models;
using DemoApp_DemoAPI.Models.Dto;

namespace DemoApp_DemoAPI.Repository.IRepostiory
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string username);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<UserDTO> Register(RegisterationRequestDTO registerationRequestDTO);
    }
}
