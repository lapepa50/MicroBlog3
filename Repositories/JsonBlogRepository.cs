using System.Text.Json;
using MicroBlog.Models;

namespace MicroBlog.Repositories;

public class JsonBlogRepository : IBlogRepository
{
    private readonly string _filePath;
    private readonly List<Post> _posts;
    private readonly object _lock = new();

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true
    };

    public JsonBlogRepository(IHostEnvironment environment)
    {
        var dataDirectory = Path.Combine(environment.ContentRootPath, "data");

        Directory.CreateDirectory(dataDirectory);

        _filePath = Path.Combine(dataDirectory, "posts.json");
        _posts = Load();
    }

    public IEnumerable<Post> GetAll()
    {
        lock (_lock)
        {
            return _posts
                .OrderByDescending(post => post.CreatedUtc)
                .ToList();
        }
    }

    public Post GetById(int id)
    {
        lock (_lock)
        {
            return _posts.FirstOrDefault(post => post.ID == id)
                ?? throw new KeyNotFoundException($"Post with ID {id} was not found.");
        }
    }

    public void Add(Post post)
    {
        lock (_lock)
        {
            if (post.ID == 0)
            {
                post.ID = _posts.Count == 0
                    ? 1
                    : _posts.Max(existingPost => existingPost.ID) + 1;
            }

            _posts.Add(post);
        }
    }

    public void Save()
    {
        lock (_lock)
        {
            var json = JsonSerializer.Serialize(_posts, JsonOptions);
            File.WriteAllText(_filePath, json);
        }
    }

    private List<Post> Load()
    {
        if (!File.Exists(_filePath))
        {
            return [];
        }

        var json = File.ReadAllText(_filePath);

        if (string.IsNullOrWhiteSpace(json))
        {
            return [];
        }

        return JsonSerializer.Deserialize<List<Post>>(json) ?? [];
    }
}