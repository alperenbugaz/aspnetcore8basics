using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FormsApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FormsApp.Controllers;

public class HomeController : Controller
{


    public IActionResult Index(string searchString ,string category)
    {   
        var products = Repository.Products;
        if (!string.IsNullOrEmpty(searchString))
        {   ViewBag.SearchString = searchString;
            products = products.Where(p => p.Name.ToLower().Contains(searchString)).ToList();
        }

        if (!string.IsNullOrEmpty(category) && category != "0")
        {
            products = products.Where(p => p.CategoryId == int.Parse(category)).ToList();
        }

        //ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name",category);

        var model = new ProductViewModel
        {
            Products = products,
            Categories = Repository.Categories,
            SelectedCategory = category
        };
        return View(model);
    }

    [HttpGet]
    public IActionResult Create()
    {   ViewBag.Categories =  new SelectList(Repository.Categories, "CategoryId", "Name");

        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(Product model ,IFormFile imageFile)
    {   

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" }; //izin verilen uzantılar 

        
        if (imageFile != null)
        {   
            var extension = Path.GetExtension(imageFile.FileName); //uzantıyı alıyoruz
             if (!allowedExtensions.Contains(extension)) //uzantı kontrolü
             {
                ModelState.AddModelError("", "Please choose a valid image file");
             }
        

        if (ModelState.IsValid)
        {
            if (imageFile !=null)
            {
            var randomfileName = string.Format($"{Guid.NewGuid().ToString()}{extension}"); //dosyanın adını değiştiriyoruz
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomfileName); //dosyanın yolu
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }
            model.ProductId = Repository.Products.Count + 1;
            model.imageFile = randomfileName;
            Repository.AddProduct(model);
            return RedirectToAction("Index");
            }    
        }
        }
            ViewBag.Categories =  new SelectList(Repository.Categories, "CategoryId", "Name");
            return View(model);
        
    }

    [HttpGet]
    public IActionResult Edit(int? id)
    {   
        if (id == null)
        {
            return NotFound();
        }

        var entity = Repository.Products.FirstOrDefault(p => p.ProductId == id);
        if (entity == null)
        {
            return NotFound();
        }
        ViewBag.Categories =  new SelectList(Repository.Categories, "CategoryId", "Name");
        return View(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id,Product model, IFormFile? imageFile)
    {
        if (id != model.ProductId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {           

            if (imageFile != null)
            {               
                var extension = Path.GetExtension(imageFile.FileName); //uzantıyı alıyoruz
                var randomfileName = string.Format($"{Guid.NewGuid().ToString()}{extension}"); //dosyanın adını değiştiriyoruz
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomfileName); //dosyanın yolu
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
                model.imageFile = randomfileName;

            }
            Repository.EditProduct(model);
            return RedirectToAction("Index");
        }
        ViewBag.Categories =  new SelectList(Repository.Categories, "CategoryId", "Name");
        return View(model);

    }

    public IActionResult Delete (int? id)
    {
        var entity = Repository.Products.FirstOrDefault(p => p.ProductId == id);
        if (entity != null)
        {
            Repository.DeleteProduct(entity);
        }
        return RedirectToAction("Index");
    }
}
