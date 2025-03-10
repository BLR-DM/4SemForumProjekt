using ContentSafetyService.Domain;
using ContentSafetyService.Domain.Enums;
using ContentSafetyService.Domain.ValueObjects;

namespace ContentSafetyService.Application.Services.ProxyInterface;

public interface IContentSafetyService
{
    Task<DetectionResult> Detect(MediaType mediaType, string content, string[] blocklists);
}