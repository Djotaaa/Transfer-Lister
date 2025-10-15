using Transfer_ListerWebApp.Models;
using Transfer_ListerWebApp.Models.DTO;

namespace Transfer_ListerWebApp.Services
{
    public interface IPlayerService
    {
        Task<IEnumerable<PlayerDTO>> GetPlayersAsync();
        Task<PlayerDTO> GetSingleAsync(int id);
        Task<ServiceResponse> CreatePlayerAsync(CreatePlayerDTO playerDTO);
        Task<ServiceResponse> UpdatePlayerAsync(int id, UpdatePlayerDTO playerDTO);
        Task<bool> DeletePlayerAsync(int id);
    }
}
