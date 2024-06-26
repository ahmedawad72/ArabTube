﻿using ArabTube.Entities.DtoModels.CommentDTOs;
using ArabTube.Entities.DtoModels.PlaylistDTOs;
using ArabTube.Entities.Models;
using ArabTube.Services.ControllersServices;
using ArabTube.Services.ControllersServices.CommentServices.Interfaces;
using ArabTube.Services.DataServices.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ArabTube.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ICommentService _commentService;

        public CommentsController(UserManager<AppUser> userManager, ICommentService commentService)
        {
            this._userManager = userManager;
            this._commentService = commentService;
        }

        [HttpGet("Comments")]
        public async Task<IActionResult> GetVideoComments(string id)
        {
            var result = await _commentService.GetVideoCommentsAsync(id);
            
            if (!result.IsSuccesed)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Comments);
        }

        [HttpGet("Comment")]
        public async Task<IActionResult> GetComment(string id)
        {
            var result = await _commentService.GetCommentAsync(id);

            if (!result.IsSuccesed)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Comment);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("FlagedComment")]
        public async Task<IActionResult> GetFlagedComment()
        {
            var result = await _commentService.GetFlagedCommentsAsync();

            if (!result.IsSuccesed)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.getCommentDtos);
        }


        [Authorize]
        [HttpGet("UserLike")]
        public async Task<IActionResult> IsUserLikeComment(string commentId)
        {
            var userName = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userName != null)
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (user != null)
                {
                    var result = await _commentService.IsUserLikeCommentAsync(commentId, user.Id);
                    if (!result.IsSuccesed)
                    {
                        return BadRequest(result.Message);
                    }
                    return Ok(result.Message);
                }
            }
            return Unauthorized();
        }

        [Authorize]
        [HttpGet("UserDisLike")]
        public async Task<IActionResult> IsUserDisLikeComment(string commentId)
        {
            var userName = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userName != null)
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (user != null)
                {
                    var result = await _commentService.IsUserDislikeCommentAsync(commentId, user.Id);
                    if (!result.IsSuccesed)
                    {
                        return BadRequest(result.Message);
                    }
                    return Ok(result.Message);
                }
            }
            return Unauthorized();
        }

        [Authorize]
        [HttpGet("UserFlag")]
        public async Task<IActionResult> IsUserFlagComment(string commentId)
        {
            var userName = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userName != null)
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (user != null)
                {
                    var result = await _commentService.IsUserFlagCommentAsync(commentId, user.Id);
                    if (!result.IsSuccesed)
                    {
                        return BadRequest(result.Message);
                    }
                    return Ok(result.Message);
                }
            }
            return Unauthorized();
        }

        [Authorize]
        [HttpPost("Add")]
        public async Task<IActionResult> AddComment (AddCommentDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userName = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userName != null)
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (user != null)
                {
                    if (user.Isbaneed)
                    {
                        return BadRequest("You Are banned");
                    }

                    var result = await _commentService.AddCommentAsync(model, user.Id , $"{user.FirstName} {user.LastName}");
                    
                    if (!result.IsSuccesed)
                        return BadRequest(result.Message);

                    return Ok("Comment Added Succesfully");
                }
            }
            return Unauthorized();
        }

        [Authorize]
        [HttpPost("Like")]
        public async Task<IActionResult> LikeComment(string id)
        {
            var userName = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userName != null)
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (user != null)
                {
                    if (user.Isbaneed)
                    {
                        return BadRequest("You Are banned");
                    }
                    var result = await _commentService.LikeCommentAsync(id , user.Id, $"{user.FirstName} {user.LastName}");
                    if (!result.IsSuccesed)
                        return BadRequest(result.Message);

                    return Ok(result.Message);
                }
            }
            return Unauthorized();   
        }

        [Authorize]
        [HttpPost("Dislike")]
        public async Task<IActionResult> DislikeComment(string id)
        {
            var userName = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userName != null)
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (user != null)
                {
                    if (user.Isbaneed)
                    {
                        return BadRequest("You Are banned");
                    }
                    var result = await _commentService.DislikeCommentAsync(id , user.Id, $"{user.FirstName} {user.LastName}");
                    if (!result.IsSuccesed)
                        return BadRequest(result.Message);
                    return Ok(result.Message);
                }
            }
            return Unauthorized();
        }

        [Authorize]
        [HttpPost("Flag")]
        public async Task<IActionResult> FlagComment(string id)
        {
            var userName = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userName != null)
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (user != null)
                {
                    if (user.Isbaneed)
                    {
                        return BadRequest("You Are banned");
                    }
                    var result = await _commentService.FlagCommentAsync(id, user.Id);
                    if (!result.IsSuccesed)
                        return BadRequest(result.Message);
                    return Ok(result.Message);
                }
            }
            return Unauthorized();
        }

        [Authorize]
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateComment(UpdateCommentDto model)
        {
            var userName = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userName != null)
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (user != null)
                {
                    if (user.Isbaneed)
                    {
                        return BadRequest("You Are banned");
                    }
                    var result = await _commentService.UpdateCommentAsync(model , user.Id);
                    if (!result.IsSuccesed)
                        return BadRequest(result.Message);
                    return Ok("Comment Updated Successfully");
                }
            }
            return Unauthorized();        
        }

        [Authorize]
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(string id)
        {
            var userName = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userName != null)
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (user != null)
                {
                    if (user.Isbaneed)
                    {
                        return BadRequest("You Are banned");
                    }
                    var result = await _commentService.DeleteCommentAsync(id , user);
                    if (!result.IsSuccesed)
                        return BadRequest(result.IsSuccesed);
                    return Ok("Comment Deleted Successfully");
                }
            }
            return Unauthorized();    
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("FlagComment")]
        public async Task<IActionResult> DeleteFlagComment(string id)
        {
            var userName = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userName != null)
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (user != null)
                {
                    var result = await _commentService.DeleteFlagCommentAsync(id, user);
                    if (!result.IsSuccesed)
                        return BadRequest(result.IsSuccesed);
                    return Ok("Comment Deleted Successfully");
                }
            }
            return Unauthorized();
        }
    }
}
