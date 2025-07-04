# ChatService - Ollama Integration

A .NET microservice that provides a chat interface using Ollama's language models. This service is part of the SlnProject microservices architecture.

## Features

- REST API endpoints for chat communication
- Integration with Ollama's language models
- Simple web interface for chat interactions
- Network-accessible endpoints for cross-device communication

## Prerequisites

- .NET 9.0 SDK
- Ollama running on your local network
- A web browser for accessing the chat interface

## Project Structure

```
ChatService/
├── Controllers/         # API controllers
│   └── ChatController.cs
├── Models/             # Data models
│   ├── ChatRequest.cs
│   └── ChatResponse.cs
├── Services/           # Business logic
│   └── OllamaChatService.cs
├── wwwroot/           # Static web files
│   ├── css/
│   ├── js/
│   └── index.html
└── Program.cs         # Application entry point
```

## Configuration

### Ollama Endpoint

The Ollama endpoint is configured in `Program.cs`. Update the BaseAddress to match your Ollama server's IP address:

```csharp
builder.Services.AddHttpClient("ollama", client =>
{
    client.BaseAddress = new Uri("http://192.168.1.1:11434");
});
```

### Network Access

The service is configured to listen on all network interfaces (0.0.0.0) on port 5018. This configuration is in `Properties/launchSettings.json`:

```json
{
  "applicationUrl": "http://0.0.0.0:5018"
}
```

## API Endpoints

### Chat Endpoint

- **URL**: `/api/chat`
- **Method**: `POST`
- **Request Body**:
  ```json
  {
    "model": "mistral:latest",
    "prompt": "Your message here",
    "stream": false
  }
  ```
- **Response**: Returns the model's response in JSON format

### Health Check

- **URL**: `/api/chat/health`
- **Method**: `GET`
- **Response**: Returns the service's health status

## Web Interface

The chat interface is accessible through a web browser at `http://<your-ip>:5018`. The interface provides:
- A clean, modern chat UI
- Real-time message sending and receiving
- Support for the Mistral language model

## Running the Service

1. Make sure Ollama is running on your network
2. Navigate to the ChatService directory
3. Run the service:
   ```bash
   dotnet run
   ```
4. Access the web interface at `http://<your-ip>:5018`
5. For API testing, use the provided `ChatService.http` file

## Testing

You can test the API endpoints using:
- The provided `ChatService.http` file
- The web interface at `http://<your-ip>:5018`
- Any HTTP client (Postman, curl, etc.)

## Notes

- The service uses the `mistral:latest` model by default
- All communication with Ollama is done via HTTP
- The web interface is responsive and works on both desktop and mobile browsers
