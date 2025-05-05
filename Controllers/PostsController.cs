using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using ReactiveLikeApiDemo.Services;
using System.Collections.Generic;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ReactiveLikeApiDemo.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class PostsController : ControllerBase
    {
     

        [HttpPost("{id}/like")]
        public IActionResult LikePost(int id)
        {
            // TryGetValue(...): Checks if the post exists.

            bool postExists = DataStore.Posts.TryGetValue(id, out var post);
            if (!postExists)
            {
                return NotFound($"Post with ID {id} not found.");
            }
            //post.Likes++: Increments the number of likes.
            post.Likes++;

            //PostSubject.OnNext(...): Triggers the broadcast to notify clients.
            DataStore.PostSubject.OnNext(post);

            //return Ok(post): Returns the updated post as a JSON response.
            return Ok(post);
        }

        [HttpGet]
        public IActionResult GetAllPosts()
        {
            //Returns a list of all posts currently stored in memory.
           return Ok(DataStore.Posts.Values);
        }
    }
}