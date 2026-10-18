using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MicroBlog.Models;
using MicroBlog.Repositories;

namespace MicroBlog.Pages;

public class CreateModel : PageModel
{
    private readonly IBlogRepository _blogRepository;

    public CreateModel(IBlogRepository blogRepository)
    {
        _blogRepository = blogRepository;
    }

    [BindProperty]
    public Post Post { get; set; } = new();

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Post.CreatedUtc = DateTime.UtcNow;

        _blogRepository.Add(Post);
        _blogRepository.Save();

        return RedirectToPage("/Index");
    }
}