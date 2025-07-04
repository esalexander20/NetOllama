using ChatService.Models;
using ChatService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChatService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly IOllamaChatService _chatService;
    private readonly ILogger<ChatController> _logger;

    public ChatController(IOllamaChatService chatService, ILogger<ChatController> logger)
    {
        _chatService = chatService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult<ChatResponse>> Chat([FromBody] ChatRequest request)
    {
        try
        {
            var response = await _chatService.GenerateResponseAsync(request);
            return Ok(response);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Failed to get response from Ollama");
            return StatusCode(503, "Failed to communicate with Ollama service");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while processing chat request");
            return StatusCode(500, "An error occurred while processing your request");
        }
    }

    [HttpGet("health")]
    public IActionResult HealthCheck()
    {
        try
        {
            return Ok(new { status = "healthy", message = "Chat service is running" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Health check failed");
            return StatusCode(500, new { status = "unhealthy", message = ex.Message });
        }
    }
}
