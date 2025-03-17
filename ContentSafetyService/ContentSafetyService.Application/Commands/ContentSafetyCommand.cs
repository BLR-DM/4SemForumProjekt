using ContentSafetyService.Application.Configuration;
using ContentSafetyService.Application.Services;
using ContentSafetyService.Application.Services.ProxyInterface;
using ContentSafetyService.Domain;
using ContentSafetyService.Domain.Enums;

namespace ContentSafetyService.Application.Commands;

public class ContentSafetyCommand : IContentSafetyCommand
{
    private readonly IContentSafetyDetection _contentSafetyDetection;
    private readonly IDecisionService _decisionService;
    private readonly IRejectionThresholdProvider _rejectionThe;

    public ContentSafetyCommand(IContentSafetyDetection contentSafetyDetection,
        IDecisionService decisionService,
        IRejectionThresholdProvider rejectionThe)
    {
        _contentSafetyDetection = contentSafetyDetection;
        _decisionService = decisionService;
        _rejectionThe = rejectionThe;
    }

    async Task<Decision> IContentSafetyCommand.ModerateContentAsync(MediaType mediaType, string content, string[] blocklists)
    {
        // Detect/analyze content safety
        var detectionResult = await _contentSafetyDetection.ContentDetectionAsync(mediaType, content, blocklists);

        // Load the rejection thresholds settings
        var rejectThresholds = _rejectionThe.GetRejectionThresholds();

        // Make a decision based on the detection result and reject thresholds
        var decisionResult = _decisionService.MakeDecision(detectionResult, rejectThresholds);

        return decisionResult;
    }
}