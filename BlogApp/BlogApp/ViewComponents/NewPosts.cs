using BlogApp.Data.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.ViewComponents
{
    public class NewPosts : ViewComponent
    {
        private IPostRepository _postRepository;

        public NewPosts(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {   //isactive olan postları tarihe göre sırala ve en son 3 postu getir.
            var posts = await _postRepository.Posts.Where(p => p.IsActive).OrderByDescending(p => p.CreatedAt).Take(3).
            ToListAsync();
            return View(posts);
        }
    }
}