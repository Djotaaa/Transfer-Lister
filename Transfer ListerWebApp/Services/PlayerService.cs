using System.Text;
using System.Text.Json;
using Transfer_ListerWebApp.Models;
using Transfer_ListerWebApp.Models.DTO;

namespace Transfer_ListerWebApp.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly HttpClient _httpClient;
        private readonly string? _baseUrl;

        public PlayerService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ServiceUrls:TransferListerAPI"];
        }
        public async Task<ServiceResponse> CreatePlayerAsync(CreatePlayerDTO playerDTO)
        {
            var json = JsonSerializer.Serialize(playerDTO);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_baseUrl}api/TransferListerAPI", content);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();

                var errors = JsonSerializer.Deserialize<Dictionary<string, string[]>>(errorContent);

                return new ServiceResponse { IsSuccess = false, ErrorMessage = string.Join("\n", errors.SelectMany(e => e.Value)) };
            }

            return new ServiceResponse { IsSuccess = true }; 
        }

        public async Task<bool> DeletePlayerAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}api/TransferListerAPI/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            return true;
        }

        public async Task<IEnumerable<PlayerDTO>> GetPlayersAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}api/TransferListerAPI");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<List<PlayerDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true, AllowTrailingCommas = true });

            return result;
        }

        public async Task<PlayerDTO> GetSingleAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}api/TransferListerAPI/{id}");
            
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<PlayerDTO>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true, AllowTrailingCommas = true });

            return result;
        }

        public async Task<ServiceResponse> UpdatePlayerAsync(int id, UpdatePlayerDTO playerDTO)
        {
            var json = JsonSerializer.Serialize(playerDTO);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{_baseUrl}api/TransferListerAPI/{id}", content);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();

                var errors = JsonSerializer.Deserialize<Dictionary<string, string[]>>(errorContent);

                return new ServiceResponse { IsSuccess = false, ErrorMessage = string.Join("\n", errors.SelectMany(e => e.Value)) };
            }

            return new ServiceResponse { IsSuccess = true};
        }
    }
}
