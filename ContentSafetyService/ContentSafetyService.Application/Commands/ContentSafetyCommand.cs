using ContentSafetyService.Application.Services;
using ContentSafetyService.Application.Services.ProxyInterface;
using ContentSafetyService.Domain;
using ContentSafetyService.Domain.Enums;

namespace ContentSafetyService.Application.Commands;

public class ContentSafetyCommand : IContentSafetyCommand
{
    private readonly IContentSafetyDetection _contentSafetyDetection;
    private readonly IDecisionService _decisionService;

    public ContentSafetyCommand(IContentSafetyDetection contentSafetyDetection, IDecisionService decisionService)
    {
        _contentSafetyDetection = contentSafetyDetection;
        _decisionService = decisionService;
    }

    async Task<Decision> IContentSafetyCommand.ModerateContentAsync(MediaType mediaType, string content, string[] blocklists)
    {
        // Detect content safety
        var detectionResult = await _contentSafetyDetection.ContentDetectionAsync(mediaType, content, blocklists);

        // Set the reject thresholds for each category
        Dictionary<Category, int> rejectThresholds = new Dictionary<Category, int> {
            { Category.Hate, 4 }, { Category.SelfHarm, 4 }, { Category.Sexual, 4 }, { Category.Violence, 4 }
        };

        // Make a decision based on the detection result and reject thresholds
        var decisionResult = _decisionService.MakeDecision(detectionResult, rejectThresholds);

        return decisionResult;
    }
}