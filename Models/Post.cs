using System.ComponentModel.DataAnnotations;

namespace MicroBlog.Models;

public class Post
{
    public int ID { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Body { get; set; } = string.Empty;

    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
}