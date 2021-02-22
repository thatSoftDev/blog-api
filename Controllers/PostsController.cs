using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Blog.API.Data;
using Blog.API.Models;

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
    }
}