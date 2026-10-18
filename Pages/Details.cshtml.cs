using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.Linq;

namespace MicroBlog.Pages
{
    public class DetailsModel : PageModel
    {
        // Make the Post property nullable to match nullable-enabled projects
        public Models.Post? Post { get; set; }

        public void OnGet(int id)
        {
            string path = System.IO.Directory.GetCurrentDirectory() + @"\data\posts.json";

            string json = System.IO.File.ReadAllText(path);
            List<Models.Post> posts = System.Text.Json.JsonSerializer.Deserialize<List<Models.Post>>(json) ?? new List<Models.Post>();

            Post = posts.FirstOrDefault(post => post.ID == id);
        }
    }
}