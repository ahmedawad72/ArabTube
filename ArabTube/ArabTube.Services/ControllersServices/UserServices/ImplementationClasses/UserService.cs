﻿using ArabTube.Entities.DtoModels.UserDTOs;
using ArabTube.Entities.GenericModels;
using ArabTube.Entities.Models;
using ArabTube.Entities.UserModels;
using ArabTube.Services.ControllersServices.UserServices.ImplementationClasses;
using ArabTube.Services.ControllersServices.UserServices.Interfaces;
using ArabTube.Services.DataServices.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using static Azure.Core.HttpHeader;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ArabTube.Services.ControllersServices.UserServices.ImplementationClasses
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GetChannelsNameResult> GetChannelsNameAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return new GetChannelsNameResult { Message = "Query Cannot Be Empty" };
            }

            var names = await _unitOfWork.AppUser.SearchChannelsAsync(query);

            if (!names.Any())
            {
                return new GetChannelsNameResult { Message = "No name Found Matching The Search Query" };
            }

            return new GetChannelsNameResult { IsSuccesed = true, names = names };

        }

        public async Task<GetUsersResult> GetUsersAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return new GetUsersResult { Message = "Query Cannot Be Empty" };
            }

            var users = await _unitOfWork.AppUser.SearchUsersAsync(query);
            
            if (!users.Any())
            {
                return new GetUsersResult { Message = "No users Found Matching The Search Query" };
            }

            IEnumerable<UserViewDto> userViewDtoList = _mapper.Map<IEnumerable<UserViewDto>>(users);
            return new GetUsersResult { IsSuccesed = true , users = userViewDtoList};
        }
    
        public async Task<ProcessResult> SetProfilePicAsync(SetProfilePicDto model , AppUser user)
        {
            using var stream = new MemoryStream();
            await model.Pic.CopyToAsync(stream);
            user.ProfilePic = stream.ToArray();
            await _unitOfWork.Complete();
            return new ProcessResult
            {
                IsSuccesed = true
            };
        }
    
        public async Task<GetChannelResult> GetChannelsAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user == null)
            {
                return new GetChannelResult { Message = "Invalid userId" };
            }
            var channel = new GetChannelDto
            {
                ChannelTitle = $"{user.FirstName} {user.LastName}",
                ProfilePic = (user.ProfilePic == null) ? new byte[1] : user.ProfilePic,
                UserId = user.Id,
                Username = user.UserName
            };
            return new GetChannelResult
            {
                IsSuccesed = true,
                user = channel
            };
        }
    }
}
