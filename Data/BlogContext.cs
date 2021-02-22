using Microsoft.EntityFrameworkCore;
using Blog.API.Models;

namespace Blog.API.Data 
{
    public class BlogContext : DbContext 
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)  
        {
        }

        public DbSet<Post> Posts { get; set; }
    }
}