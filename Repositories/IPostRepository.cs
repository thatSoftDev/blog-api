using System.Collections.Generic;
using Blog.API.Models;

namespace Blog.API.Repositories
{
    public interface IPostRepository {
        IEnumerable<Post> Get();
        Post Find(int id);
        void Create(Post post);
        void Update(int id, Post post);
        void Delete(int id);
    }
}