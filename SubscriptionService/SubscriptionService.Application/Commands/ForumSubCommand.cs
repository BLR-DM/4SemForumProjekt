using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SubscriptionService.Application.Commands.CommandDto;
using SubscriptionService.Application.Commands.Interfaces;
using SubscriptionService.Application.Repositories;
using SubscriptionService.Domain.Entities;

namespace SubscriptionService.Application.Commands
{
    public class ForumSubCommand : IForumSubCommand
    {
        private readonly IForumSubRepository _forumSubRepository;

        public ForumSubCommand(IForumSubRepository forumSubRepository)
        {
            _forumSubRepository = forumSubRepository;
        }
        async Task IForumSubCommand.CreateAsync(SubDto forumSubDto)
        {
            var forumSub = ForumSubscription.Create(forumSubDto.ItemId, forumSubDto.AppUserId);

            await _forumSubRepository.AddAsync(forumSub);

        }

        async Task IForumSubCommand.DeleteAsync(SubDto forumSubDto)
        {
            var forumSub = await _forumSubRepository.GetAsync(forumSubDto.ItemId, forumSubDto.AppUserId);
           
            await _forumSubRepository.DeleteAsync(forumSub);
            
        }
    }
}
