using Microsoft.AspNetCore.Mvc;
using StoreApp.Data.Abstract;
using StoreApp.Web.Models;

namespace StoreApp.Web.Components
{
    public class CategoriesListViewComponent : ViewComponent
    {
        private readonly IStoreRepository _storeRepository;

        public CategoriesListViewComponent(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public  IViewComponentResult Invoke()
        {   

            ViewBag.SelectedCategory = RouteData?.Values["category"];
            var categories = _storeRepository
                            .Categories
                            .Select(c => new CategoryViewModel
                            {
                                CategoryId = c.CategoryId,
                                CategoryName = c.CategoryName,
                                CategoryUrl = c.CategoryUrl
                            })
                            .ToList();
            return View(categories);
        }
    }
}