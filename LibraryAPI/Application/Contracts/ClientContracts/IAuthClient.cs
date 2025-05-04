using LibraryAPI.Application.Dtos;

namespace LibraryAPI.Application.Contracts.ClientContracts
{
    public interface IAuthClient
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDTO request);
        Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto request);
    }
}
