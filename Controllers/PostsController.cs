using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Blog.API.Data;
using Blog.API.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace Blog.API.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {   
        private readonly BlogContext _context; 
        public PostsController(BlogContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Post>> Get() 
        {
            return Ok(_context.Posts.AsNoTracking().ToList());
        }

        [HttpGet("{id}", Name="Find")]
        public ActionResult<Post> Find(int id) 
        {
            return Ok(_context.Posts.AsNoTracking().Where(x => x.Id == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult<Post> Create(Post post) 
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
            return CreatedAtRoute(nameof(Find), new {Id = post.Id}, post);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, Post post) 
        {   
            _context.Entry(post).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult Patch(int id, JsonPatchDocument<Post> patchDocument) 
        {   
            Post post = _context.Posts.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
            patchDocument.ApplyTo(post, ModelState);
            _context.Entry(post).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id) 
        {
            Post post = _context.Posts.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
            _context.Posts.Remove(post);
            _context.SaveChanges();
            return NoContent();
        }
    }
}