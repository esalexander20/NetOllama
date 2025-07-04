using ChatService.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ChatService.Services;

public interface IOllamaChatService
{
    Task<ChatResponse> GenerateResponseAsync(ChatRequest request);
}

public class OllamaChatService : IOllamaChatService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<OllamaChatService> _logger;

    public OllamaChatService(IHttpClientFactory httpClientFactory, ILogger<OllamaChatService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<ChatResponse> GenerateResponseAsync(ChatRequest request)
    {
        var client = _httpClientFactory.CreateClient("ollama");
        var response = await client.PostAsJsonAsync("/api/generate", request);
        
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("Failed to get response from Ollama. Status code: {StatusCode}", response.StatusCode);
            throw new HttpRequestException($"Ollama API returned {response.StatusCode}");
        }

        var chatResponse = await response.Content.ReadFromJsonAsync<ChatResponse>();
        if (chatResponse == null)
        {
            throw new InvalidOperationException("Failed to deserialize Ollama response");
        }

        return chatResponse;
    }
}
