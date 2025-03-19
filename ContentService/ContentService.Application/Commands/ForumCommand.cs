using ContentService.Application.Commands.CommandDto.ForumDto;
using ContentService.Application.Commands.CommandDto.PostDto;
using ContentService.Application.Commands.Interfaces;
using ContentService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentService.Application.Commands
{
    public class ForumCommand : IForumCommand
    {
        private readonly IForumRepository _forumRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ForumCommand(IUnitOfWork unitOfWork, IForumRepository forumRepository)
        {
            _unitOfWork = unitOfWork;
            _forumRepository = forumRepository;
        }

        async Task IForumCommand.CreatePostAsync(CreatePostDto postDto, string appUserId, string role, int forumId)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                // Load
                var forum = await _forumRepository.GetForumAsync(forumId);

                // Do
                forum.AddPost(postDto.Description, appUserId);

                //Save
                await _unitOfWork.Commit();
            }
            catch (Exception)
            {
                await _unitOfWork.Rollback();
                throw;
            }
        }

        async Task IForumCommand.UpdatePostAsync(UpdatePostDto postDto, string appUserId, string role, int postId, int forumId)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                // Load
                var forum = await _forumRepository.GetForumAsync(forumId);

                // Do
                var post = forum.UpdatePost(postId, postDto.Description,
                    appUserId);
                _forumRepository.UpdatePost(post, postDto.RowVersion);

                //Save
                await _unitOfWork.Commit();
            }
            catch (Exception)
            {
                await _unitOfWork.Rollback();
                throw;
            }
        }

        async Task IForumCommand.DeletePostAsync(DeletePostDto postDto, string appUserId, string role, int postId, int forumId)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                // Load
                var forum = await _forumRepository.GetForumAsync(forumId);

                // Do
                var post = forum.DeletePost(postId, appUserId);
                _forumRepository.DeletePost(post, postDto.RowVersion);

                //Save
                await _unitOfWork.Commit();
            }
            catch (Exception)
            {
                await _unitOfWork.Rollback();
                throw;
            }
        }

        async Task IForumCommand.CreateForumAsync(CreateForumDto forumDto)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                // Do
                var forum = Forum.Create(forumDto.Name);
                await _forumRepository.AddForumAsync(forum);

                // Save
                await _unitOfWork.Commit();
            }
            catch (Exception)
            {
                await _unitOfWork.Rollback();
                throw;
            }
        }

        async Task IForumCommand.DeleteForumAsync(DeleteForumDto forumDto, int forumId)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                // Load
                var forum = await _forumRepository.GetForumAsync(forumId);

                // Do
                _forumRepository.DeleteForum(forum, forumDto.RowVersion);

                // Save
                await _unitOfWork.Commit();
            }
            catch (Exception)
            {
                await _unitOfWork.Rollback();
                throw;
            }
        }
    }
}
