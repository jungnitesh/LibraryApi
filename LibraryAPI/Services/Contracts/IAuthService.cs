
using LibraryAPI.Data.Dtos;

namespace LibraryAPI.Services.Contracts
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDTO request);
        Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto request);
    }
}
