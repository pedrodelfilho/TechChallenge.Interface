using TechChallenge.Entities.Responses;
using Microsoft.Extensions.Options;
using System.Text.Json;
using TechChallenge.Entities;
using TechChallenge.Interface.Extensions;
using TechChallenge.Interface.Interfaces;

namespace TechChallenge.Interface.Services
{
    public class DDDService : IDDDService
    {
        private readonly HttpClient _httpClient;
        private readonly string _resourceDDD;

        public DDDService(HttpClient httpClient, IOptions<ApiSettings> settings)
        {
            _httpClient = httpClient;
            _resourceDDD = settings.Value.ResourceDDD;
        }
        public async Task<List<DDD>> ObterTodosDDDs()
        {
            var response = await _httpClient.GetAsync($"{_resourceDDD}obtertodos");
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonSerializer.Deserialize<BaseResponse>(jsonResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var ddds = JsonSerializer.Deserialize<List<DDD>>(apiResponse.Data, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return ddds ?? new List<DDD>();
        }
    }
}
