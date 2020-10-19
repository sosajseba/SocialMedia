using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;

        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postRepository.GetPosts();
            var postsDto = posts.Select(x => new PostDto
            {
                PostId = x.PostId,
                Description = x.Description,
                Image = x.Image,
                UserId = x.UserId,
                Date = x.Date
            });

            return Ok(postsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _postRepository.GetPost(id);
            var postDto = new PostDto
            {
                PostId = post.PostId,
                Description = post.Description,
                Image = post.Image,
                UserId = post.UserId,
                Date = post.Date
            };
            return Ok(postDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostDto postDto)
        {
            var post = new Post
            {
                Description = postDto.Description,
                Image = postDto.Image,
                UserId = postDto.UserId,
                Date = postDto.Date
            };
            await _postRepository.InsertPost(post);
            return Ok(post);
        }
    }
}
