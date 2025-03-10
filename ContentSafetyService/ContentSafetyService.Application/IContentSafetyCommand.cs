using ContentSafetyService.Application.CommandDto;
using ContentSafetyService.Domain;

namespace ContentSafetyService.Application;

public interface IContentSafetyCommand
{
    Task<Decision> MakeDecisionAsync(PostDto post);
}