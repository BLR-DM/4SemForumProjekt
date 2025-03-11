using ContentSafetyService.Domain;
using ContentSafetyService.Domain.Enums;
using System.Text.Json;

namespace ContentSafetyService.Infrastructure.ServiceProxyImpl;

public class AzureContentSafetyProxy : IAzureContentSafetyProxy
{
    private readonly HttpClient _client;

    public AzureContentSafetyProxy(HttpClient client)
    {
        _client = client;
    }

    async Task<DetectionResult> IAzureContentSafetyProxy.DetectAsync(HttpRequestMessage msg, MediaType mediaType)
    {
        var response = await _client.SendAsync(msg);
        var responseText = await response.Content.ReadAsStringAsync();

        // test
        Console.WriteLine((int)response.StatusCode);
        foreach (var header in response.Headers)
        {
            Console.WriteLine($"{header.Key}: {string.Join(", ", header.Value)}");
        }
        Console.WriteLine(responseText);

        if (!response.IsSuccessStatusCode)
        {
            DetectionErrorResponse? error =
                JsonSerializer.Deserialize<DetectionErrorResponse>(responseText);
            if (error == null || error.error == null || error.error.code == null || error.error.message == null)
            {
                throw new DetectionException(response.StatusCode.ToString(),
                    $"Error is null. Response text is {responseText}");
            }
            throw new DetectionException(error.error.code, error.error.message);
        }

        var result = DeserializeDetectionResult(responseText, mediaType);
        if (result == null)
        {
            throw new DetectionException(response.StatusCode.ToString(),
                $"HttpResponse is null. Response text is {responseText}");
        }

        return result;
    }

    DetectionResult? DeserializeDetectionResult(string json, MediaType mediaType)
    {
        switch (mediaType)
        {
            case MediaType.Text:
                return JsonSerializer.Deserialize<TextDetectionResult>(json);
            case MediaType.Image:
                return JsonSerializer.Deserialize<ImageDetectionResult>(json);
            default:
                throw new ArgumentException($"Invalid Media Type {mediaType}");
        }
    }
}