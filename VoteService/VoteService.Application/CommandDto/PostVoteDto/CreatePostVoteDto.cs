namespace VoteService.Application.CommandDto.PostVoteDto;

public record CreatePostVoteDto(string UserId, string PostId, bool VoteType);