using VoteService.Application.CommandDto.PostVoteDto;
using VoteService.Application.Interfaces;
using VoteService.Domain.Entities;
using VoteService.Domain.Interfaces;

namespace VoteService.Application;

public class PostVoteCommand : IPostVoteCommand
{
    private readonly IPostVoteRepository _repository;

    public PostVoteCommand(IPostVoteRepository repository)
    {
        _repository = repository;
    }
    async Task IPostVoteCommand.CreateVoteAsync(CreatePostVoteDto dto)
    {
		try
        {
            var postVote = PostVote.Create(dto.UserId, dto.PostId, dto.VoteType);
            await _repository.AddPostVoteAsync(postVote);
        }
		catch (Exception)
        {
            throw new ArgumentException();
        }
    }
}