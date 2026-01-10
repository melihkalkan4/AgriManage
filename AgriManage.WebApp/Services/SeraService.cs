using AgriManage.WebApp.DTOs; // DTO'ları görmesi için şart
using System.Net.Http.Headers;
using System.Text.Json; // JSON işlemleri için şart

namespace AgriManage.WebApp.Services
{
    // ==========================================
    // 1. ARAYÜZ (INTERFACE) TANIMI
    // ==========================================
    public interface ISeraService
    {
        Task<List<SeraDto>> GetSeralarAsync(string token);
        Task<bool> AddSeraAsync(SeraDto sera, string token);
        Task DeleteSeraAsync(int id, string token);
        Task<SeraDto?> GetSeraByIdAsync(int id, string token);
        Task<bool> UpdateSeraAsync(SeraDto sera, string token);

        // Hasat İşlemleri
        Task<bool> AddHasatAsync(HasatDto hasat, string token);
        Task<List<HasatDto>> GetHasatlarAsync(int seraId, string token);

        // Ekim İşlemleri
        Task<bool> AddEkimAsync(EkimDto ekim, string token);
        Task<List<EkimDto>> GetEkimlerAsync(int seraId, string token);
    }

    // ==========================================
    // 2. GERÇEK SINIF (IMPLEMENTATION)
    // ==========================================
    public class SeraService : ISeraService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _options;

        public SeraService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        // --- SERA CRUD İŞLEMLERİ ---

        public async Task<List<SeraDto>> GetSeralarAsync(string token)
        {
            var client = CreateClient(token);
            var response = await client.GetAsync("/api/sera");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<List<SeraDto>>(json, _options);
                return result ?? new List<SeraDto>();
            }
            return new List<SeraDto>();
        }

        public async Task<bool> AddSeraAsync(SeraDto sera, string token)
        {
            var client = CreateClient(token);
            var response = await client.PostAsJsonAsync("/api/sera", sera);
            return response.IsSuccessStatusCode;
        }

        public async Task DeleteSeraAsync(int id, string token)
        {
            var client = CreateClient(token);
            await client.DeleteAsync($"/api/sera/{id}");
        }

        public async Task<SeraDto?> GetSeraByIdAsync(int id, string token)
        {
            var client = CreateClient(token);
            var response = await client.GetAsync($"/api/sera/{id}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<SeraDto>(json, _options);
            }
            return null;
        }

        public async Task<bool> UpdateSeraAsync(SeraDto sera, string token)
        {
            var client = CreateClient(token);
            var response = await client.PutAsJsonAsync("/api/sera", sera);
            return response.IsSuccessStatusCode;
        }

        // --- HASAT İŞLEMLERİ ---

        public async Task<bool> AddHasatAsync(HasatDto hasat, string token)
        {
            var client = CreateClient(token);

            // 🔥 GÜNCELLENEN KISIM BURASI 🔥
            // Checkbox bilgisini (kokSokumu) URL üzerinden API'ye gönderiyoruz.
            // API tarafında [FromQuery] bool kokSokumu parametresi bunu karşılayacak.
            var url = $"/api/sera/hasat?kokSokumu={hasat.KokSokumu}";

            var response = await client.PostAsJsonAsync(url, hasat);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<HasatDto>> GetHasatlarAsync(int seraId, string token)
        {
            var client = CreateClient(token);
            var response = await client.GetAsync($"/api/sera/hasat/{seraId}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<List<HasatDto>>(json, _options);
                return result ?? new List<HasatDto>();
            }
            return new List<HasatDto>();
        }

        // --- EKİM (PLANTING) İŞLEMLERİ ---

        public async Task<bool> AddEkimAsync(EkimDto ekim, string token)
        {
            var client = CreateClient(token);
            var response = await client.PostAsJsonAsync("/api/sera/ekim", ekim);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<EkimDto>> GetEkimlerAsync(int seraId, string token)
        {
            var client = CreateClient(token);
            var response = await client.GetAsync($"/api/sera/ekim/{seraId}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<List<EkimDto>>(json, _options);
                return result ?? new List<EkimDto>();
            }
            return new List<EkimDto>();
        }

        // --- YARDIMCI METOT ---
        private HttpClient CreateClient(string token)
        {
            var client = _httpClientFactory.CreateClient("GreenhouseClient");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return client;
        }
    }
}