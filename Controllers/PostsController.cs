using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Blog.API.Repositories;
using Blog.API.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace Blog.API.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {   
        private readonly IPostRepository _repository;
        
        public PostsController(IPostRepository repository) 
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Post>> Get() 
        {
            return Ok(_repository.Get());
        }

        [HttpGet("{id}", Name="Find")]
        public ActionResult<Post> Find(int id) 
        {   
            Post post = _repository.Find(id);

            if(post == null) return NotFound();

            return Ok(post);
        }

        [HttpPost]
        public ActionResult<Post> Create(Post post) 
        {
            _repository.Create(post);

            return CreatedAtRoute(nameof(Find), new {Id = post.Id}, post);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, Post post) 
        {   
            Post existingPost = _repository.Find(id);
            
            if (existingPost == null) return NotFound();

            _repository.Update(id, post);

            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult Patch(int id, JsonPatchDocument<Post> patchDocument) 
        {   
            Post post = _repository.Find(id);

            if (post == null) return NotFound();

            patchDocument.ApplyTo(post, ModelState);
            _repository.Update(id, post);
      
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id) 
        {
            Post post = _repository.Find(id);

            if (post == null) return NotFound();

            _repository.Delete(id);

            return NoContent();
        }
    }
}