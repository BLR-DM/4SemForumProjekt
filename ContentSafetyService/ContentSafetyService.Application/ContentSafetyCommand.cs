using ContentSafetyService.Application.CommandDto;
using ContentSafetyService.Application.Services.ProxyInterface;
using ContentSafetyService.Domain;
using ContentSafetyService.Domain.Enums;

namespace ContentSafetyService.Application;

public class ContentSafetyCommand : IContentSafetyCommand
{
    private readonly IContentSafetyService _contentSafetyService;

    public ContentSafetyCommand(IContentSafetyService contentSafetyService)
    {
        _contentSafetyService = contentSafetyService;
    }
    async Task<Decision> IContentSafetyCommand.MakeDecisionAsync(PostDto post)
    {
        // MOVE TO DOMAIN LAYER
        // create the detectionResult after getting the azure service from serviceProvider ...


        var mediaType = MediaType.Text;
        var blocklists = Array.Empty<string>();
        var content = post.Content;

        // Detect content safety
        var detectionResult = await _contentSafetyService.Detect(mediaType, content.Text, blocklists);

        return null;
    }
}