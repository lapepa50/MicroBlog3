using MicroBlog.Models;

namespace MicroBlog.Repositories;

public class InMemoryBlogRepository : IBlogRepository
{
    private readonly List<Post> _posts = [];

    public IEnumerable<Post> GetAll()
    {
        return _posts.OrderByDescending(post => post.CreatedUtc).ToList();
    }

    public Post GetById(int id)
    {
        return _posts.FirstOrDefault(post => post.ID == id)
            ?? throw new KeyNotFoundException($"Post with ID {id} was not found.");
    }

    public void Add(Post post)
    {
        if (post.ID == 0)
        {
            post.ID = _posts.Count == 0
                ? 1
                : _posts.Max(existingPost => existingPost.ID) + 1;
        }

        _posts.Add(post);
    }

    public void Save()
    {
        // No persistence required for the in-memory implementation.
    }
}