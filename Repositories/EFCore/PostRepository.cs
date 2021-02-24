using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Blog.API.Models;
using Blog.API.Data;

namespace Blog.API.Repositories.EFCore
{
    public class PostRepository : IPostRepository
    {   
        private readonly BlogContext _context;
        
        public PostRepository(BlogContext context) 
        {
            _context = context;
        }
        public IEnumerable<Post> Get() 
        {
            return _context.Posts.AsNoTracking().ToList();
        }
        
        public Post Find(int id) 
        {
            return _context.Posts.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
        }
        public void Create(Post post) 
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }
        public void Update(int id, Post post) 
        {
            _context.Entry(post).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public void Delete(int id) 
        {
            _context.Posts.Remove(this.Find(id));
            _context.SaveChanges();
        }
    }
}