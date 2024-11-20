using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using Microsoft.AspNetCore.Mvc;
using BlogApp.Models;
using Microsoft.EntityFrameworkCore;
using BlogApp.Entity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace BlogApp.Controllers
{
    public class PostsController : Controller
    {
        private IPostRepository _postRepository;
        private ICommentRepository _commentRepository;
        private ITagRepository _tagRepository;

        public PostsController(IPostRepository postRepository, ICommentRepository commentRepository, ITagRepository tagRepository)

        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
            _tagRepository = tagRepository;
        }


        public async Task<IActionResult> Index(string tag)
        {
            var posts = _postRepository.Posts.Where(p => p.IsActive);

            if (!string.IsNullOrEmpty(tag))
            {
                posts = posts.Where(p => p.Tags.Any(t => t.Url == tag));
            }
            return View(new PostsViewModel { Posts = await posts.ToListAsync() });
        }

        public async Task<IActionResult> Details(string url)
        {
            var post = await _postRepository.Posts.Include(x=>x.User).Include(x => x.Tags).Include(x => x.Comments).ThenInclude(x => x.User).FirstOrDefaultAsync(p => p.Url == url);
            if (post == null)
                return NotFound();
            return View(post);
        }

        public IActionResult AddComment(int postId, string CommentText, string Url)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var UserName = User.FindFirstValue(ClaimTypes.Name);
            var entity = new Comment
            {
                PostId = postId,
                CommentText = CommentText,
                CreatedAt = DateTime.Now,
                UserId = int.Parse(userId ?? "0"),
            };
            _commentRepository.CreateComment(entity);

            return Redirect("/Posts/Details/" + Url);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string PostImage = null;
                if (model.PostImage != null)
                {
                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.PostImage.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.PostImage.CopyToAsync(fileStream);
                    }
                    PostImage = uniqueFileName;
                }
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                {
                    ModelState.AddModelError("", "User is not authenticated.");
                    return View(model);
                }

                _postRepository.CreatePost(new Post
                {
                    Title = model.Title,
                    Description = model.Description,
                    Content = model.Content,
                    PostImage = PostImage,
                    Url = model.Url,
                    CreatedAt = DateTime.Now,
                    UserId = 1,
                    IsActive = false
                });
                return RedirectToAction("Index");
            }
            return View(model);

        }

        [Authorize]
        public async Task<IActionResult> List()
        {
            var UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
            var role = User.FindFirstValue(ClaimTypes.Role);


            var posts = _postRepository.Posts;

            if (string.IsNullOrEmpty(role) || role != "Admin")
            {
                posts = posts.Where(p => p.UserId == UserId);
            }
            return View(await posts.ToListAsync());
        }

        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var post = _postRepository.Posts.Include(x=>x.Tags).FirstOrDefault(p => p.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            ViewBag.Tags = _tagRepository.Tags.ToList();

            var model = new PostCreateViewModel
            {
                PostId = post.PostId,
                Title = post.Title,
                Description = post.Description,
                Content = post.Content,
                ExistingPostImage = post.PostImage,
                Url = post.Url,
                IsActive = post.IsActive,
                Tags = post.Tags
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(PostCreateViewModel model,int[] TagIds)
        {
            if (ModelState.IsValid)
            {
                var post = _postRepository.Posts.FirstOrDefault(p => p.PostId == model.PostId);
                if (post == null)
                {
                    return NotFound();
                }

                post.Title = model.Title;
                post.Description = model.Description;
                post.Content = model.Content;
                post.Url = model.Url;
                post.IsActive = model.IsActive;
                post.Tags = model.Tags;


                if (model.PostImage != null)
                {
                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.PostImage.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.PostImage.CopyToAsync(fileStream);
                    }
                    post.PostImage = uniqueFileName;
                }
                else
                {

                    post.PostImage = model.ExistingPostImage;
                }

                if (User.FindFirstValue(ClaimTypes.Role) == "Admin")
                {
                    post.IsActive = model.IsActive;
                }
                _postRepository.EditPost(post, TagIds);
                return RedirectToAction("List");
            }
            model.ExistingPostImage = model.ExistingPostImage;

            return View(model);

        }



    }
}