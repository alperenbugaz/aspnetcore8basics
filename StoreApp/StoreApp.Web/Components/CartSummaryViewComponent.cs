using Microsoft.AspNetCore.Mvc;
using StoreApp.Web.Models;

namespace StoreApp.Web.Components;

public class CartSummaryViewComponent : ViewComponent
{
    private Cart _cartService;

    public CartSummaryViewComponent(Cart cartService)
    {
        _cartService = cartService;
    }

    public IViewComponentResult Invoke()
    {
        return View(_cartService);
    }
}