using ChatService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configure Ollama client and services
builder.Services.AddHttpClient("ollama", client =>
{
    client.BaseAddress = new System.Uri("http://192.168.1.1:11434");
});
builder.Services.AddScoped<IOllamaChatService, OllamaChatService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Enable static file serving for wwwroot
app.UseDefaultFiles(); // Enable default files (e.g., index.html)

app.UseAuthorization();
app.MapControllers();

app.Run();
