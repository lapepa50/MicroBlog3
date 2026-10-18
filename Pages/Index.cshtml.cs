using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MicroBlog.Models;
using MicroBlog.Repositories;

namespace MicroBlog.Pages;

public class IndexModel : PageModel
{
    private readonly JsonBlogRepository _jsonRepository;
    private readonly InMemoryBlogRepository _inMemoryRepository;

    public IEnumerable<Post> Posts { get; private set; } = [];

    [BindProperty(SupportsGet = true)]
    public string RepositoryType { get; set; } = "json";

    public IndexModel(
        JsonBlogRepository jsonRepository,
        InMemoryBlogRepository inMemoryRepository)
    {
        _jsonRepository = jsonRepository;
        _inMemoryRepository = inMemoryRepository;
    }

    public void OnGet()
    {
        RepositoryType = RepositoryType.ToLowerInvariant();

        Posts = RepositoryType switch
        {
            "memory" => _inMemoryRepository.GetAll(),
            _ => _jsonRepository.GetAll()
        };
    }

    public string RepositoryName => RepositoryType == "memory"
        ? "In-memory repository"
        : "JSON repository";
}