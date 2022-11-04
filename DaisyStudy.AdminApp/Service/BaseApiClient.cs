using DaisyStudy.Utilities.Constants;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace DaisyStudy.AdminApp.Service
{
    public class BaseApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        protected BaseApiClient(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
        }

        protected async Task<TResponse> GetAsync<TResponse>(string url)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.AppSettings.Token);

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.GetAsync(url);
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                TResponse myDeserializedObjList = (TResponse)JsonConvert.DeserializeObject(body, typeof(TResponse));
                return myDeserializedObjList;
            }
            return JsonConvert.DeserializeObject<TResponse>(body);
        }

        protected async Task<TResponse> PostAsync<TResponse>(string url, object? request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient();

            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            var response = await client.PostAsync(url, httpContent);
            var body = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                TResponse myDeserializedObjList = (TResponse)JsonConvert.DeserializeObject(body, typeof(TResponse));
                return myDeserializedObjList;
            }

            return JsonConvert.DeserializeObject<TResponse>(body);
        }

        protected async Task<TResponse> PutAsync<TResponse>(string url, object? request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var sessions = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.AppSettings.Token);

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            var response = await client.PutAsync(url, httpContent);
            var body = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                TResponse myDeserializedObjList = (TResponse)JsonConvert.DeserializeObject(body, typeof(TResponse));
                return myDeserializedObjList;
            }

            return JsonConvert.DeserializeObject<TResponse>(body);
        }

        protected async Task<TResponse> DeleteAsync<TResponse>(string url)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.AppSettings.Token);

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            var response = await client.DeleteAsync(url);
            var body = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                TResponse myDeserializedObjList = (TResponse)JsonConvert.DeserializeObject(body, typeof(TResponse));
                return myDeserializedObjList;
            }

            return JsonConvert.DeserializeObject<TResponse>(body);
        }
    }
}