using SubscriptionService.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SubscriptionService.Application.Commands.Interfaces;
using SubscriptionService.Application.Commands.CommandDto;
using SubscriptionService.Domain.Entities;

namespace SubscriptionService.Application.Commands
{
    public class PostSubCommand : IPostSubCommand
    {
        private readonly IPostSubRepository _postSubRepository;

        public PostSubCommand(IPostSubRepository postSubRepository)
        {
            _postSubRepository = postSubRepository;
        }
        async Task IPostSubCommand.CreateAsync(SubDto postSubDto)
        {
            var postSub = PostSubscription.Create(postSubDto.ItemId, postSubDto.AppUserId);

            await _postSubRepository.AddAsync(postSub);
        }

        async Task IPostSubCommand.DeleteAsync(SubDto postSubDto)
        {
            var postSub = await _postSubRepository.GetAsync(postSubDto.ItemId, postSubDto.AppUserId);

            await _postSubRepository.DeleteAsync(postSub);
        }
    }
}
