using ContentSafetyService.Domain;
using ContentSafetyService.Domain.Enums;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using ContentSafetyService.Infrastructure.Services;

namespace ContentSafetyService.Infrastructure.Helpers;

public class RequestBuilder : IRequestBuilder
{
    private readonly string _apiVersion;
    private readonly string _endpoint;
    private readonly string _subscriptionKey;

    public static readonly JsonSerializerOptions options =
        new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new JsonStringEnumConverter() }
        };

    public RequestBuilder(IConfiguration configuration)
    {
        _apiVersion = configuration["ContentSafety:ApiVersion"];
        _endpoint = configuration["ContentSafety:Endpoint"];
        _subscriptionKey = configuration["ContentSafety:SubscriptionKey"];
    }

    HttpRequestMessage IRequestBuilder.BuildHttpRequestMessage(MediaType mediaType, string content, string[] blocklists)
    {
        var url = BuildUrl(mediaType);
        var requestBody = BuildRequestBody(mediaType, content, blocklists);
        var payload = JsonSerializer.Serialize(requestBody, requestBody.GetType(), options);
        var msg = new HttpRequestMessage(HttpMethod.Post, url);

        msg.Content = new StringContent(payload, Encoding.UTF8, "application/json");
        msg.Headers.Add("Ocp-Apim-Subscription-Key", _subscriptionKey);

        return msg;
    }

    string BuildUrl(MediaType mediaType)
    {
        switch (mediaType)
        {
            case MediaType.Text:
                return $"{_endpoint}/contentsafety/text:analyze?api-version={_apiVersion}";
            case MediaType.Image:
                return $"{_endpoint}/contentsafety/image:analyze?api-version={_apiVersion}";
            default:
                throw new ArgumentException($"Invalid Media Type {mediaType}");
        }
    }

    DetectionRequest BuildRequestBody(MediaType mediaType, string content, string[] blocklists)
    {
        switch (mediaType)
        {
            case MediaType.Text:
                return new TextDetectionRequest(content, blocklists);
            case MediaType.Image:
                return new ImageDetectionRequest(content);
            default:
                throw new ArgumentException($"Invalid Media Type {mediaType}");
        }
    }

}