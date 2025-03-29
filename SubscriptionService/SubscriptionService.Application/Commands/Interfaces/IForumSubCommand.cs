using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SubscriptionService.Application.Commands.CommandDto;

namespace SubscriptionService.Application.Commands.Interfaces
{
    public interface IForumSubCommand
    {
        Task CreateAsync(SubDto forumSubDto);
        Task DeleteAsync(SubDto forumSubDto);
    }
}
