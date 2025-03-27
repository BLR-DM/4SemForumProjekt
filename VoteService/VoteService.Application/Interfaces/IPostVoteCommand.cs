using VoteService.Application.CommandDto.PostVoteDto;

namespace VoteService.Application.Interfaces;

public interface IPostVoteCommand
{
    Task CreateVoteAsync(CreatePostVoteDto dto);
}