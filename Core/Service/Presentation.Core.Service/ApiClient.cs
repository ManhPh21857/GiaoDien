using Blazored.LocalStorage;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace Presentation.Core.Service;

public class ApiClient {
    private readonly HttpClient httpClient;

    public readonly ILocalStorageService LocalStorageService;

    public ApiClient(
        HttpClient httpClient,
        ILocalStorageService localStorageService
    ) {
        this.httpClient = httpClient;
        this.LocalStorageService = localStorageService;

        this.httpClient.DefaultRequestHeaders.Accept.Clear();
        this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        this.httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", localStorageService.GetItemAsStringAsync("token").ToString());
    }

    public string GenerateApiUrl(string url, IDictionary<string, object> data)
    {
        var result = url;
        if (data.Any())
        {
            foreach (string key in data.Keys)
            {
                result = result.Replace($":{key}", data[key].ToString());
            }
        }

        return result;
    }

    public async Task<HttpResponseMessage> GetAsync(string url)
    {
        try
        {
            var response = await this.httpClient.GetAsync(url);

            return response;
        }
        catch (HttpRequestException)
        {
            var errorModel = new
            {
                errorCode = "400",
                errorMessage = "Fail to fetch data"
            };

            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent(JsonSerializer.Serialize(errorModel))
            };

            return response;
        }
        
    }

    public async Task<HttpResponseMessage> PostAsync(string url, object request)
    {
        try
        { 
            var response = await this.httpClient.PostAsJsonAsync(url, request);

            return response;
        }
        catch (HttpRequestException)
        {
            var errorModel = new
            {
                errorCode = "400",
                errorMessage = "Fail to fetch data"
            };

            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent(JsonSerializer.Serialize(errorModel))
            };

            return response;
        }

    }

    public async Task<HttpResponseMessage> PutAsync(string url, object request)
    {
        try
        {
            var response = await this.httpClient.PutAsJsonAsync(url, request);

            return response;
        }
        catch (HttpRequestException)
        {
            var errorModel = new
            {
                errorCode = "400",
                errorMessage = "Fail to fetch data"
            };

            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent(JsonSerializer.Serialize(errorModel))
            };

            return response;
        }

    }

    public async Task<HttpResponseMessage> DeleteAsync(string url)
    {
        try
        {
            var response = await this.httpClient.DeleteAsync(url);

            return response;
        }
        catch (HttpRequestException)
        {
            var errorModel = new
            {
                errorCode = "400",
                errorMessage = "Fail to fetch data"
            };

            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent(JsonSerializer.Serialize(errorModel))
            };

            return response;
        }

    }
}