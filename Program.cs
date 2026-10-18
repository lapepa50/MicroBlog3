using MicroBlog.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddSingleton<JsonBlogRepository>();
builder.Services.AddSingleton<InMemoryBlogRepository>();

// Keep JSON as the default repository for pages that use IBlogRepository.
builder.Services.AddSingleton<IBlogRepository>(services =>
    services.GetRequiredService<JsonBlogRepository>());

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();