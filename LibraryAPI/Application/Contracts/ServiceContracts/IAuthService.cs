using LibraryAPI.Application.Dtos;

namespace LibraryAPI.Application.Contracts.ServiceContracts
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDTO request);
        Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto request);
    }
}
