using System.Collections.Generic;

namespace ChatService.Models;

public class ChatRequest
{
    public string Model { get; set; } = "mistral:latest";
    public string Prompt { get; set; } = string.Empty;
    public bool Stream { get; set; } = false;
    public IDictionary<string, object>? Options { get; set; }
}
