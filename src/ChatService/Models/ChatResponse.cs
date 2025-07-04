namespace ChatService.Models;

public class ChatResponse
{
    public string Model { get; set; } = string.Empty;
    public string Response { get; set; } = string.Empty;
    public long CreatedAt { get; set; }
    public bool Done { get; set; }
    public int TotalDuration { get; set; }
    public int LoadDuration { get; set; }
    public int PromptEvalCount { get; set; }
    public int PromptEvalDuration { get; set; }
    public int EvalCount { get; set; }
    public int EvalDuration { get; set; }
}
