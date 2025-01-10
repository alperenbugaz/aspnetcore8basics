using Microsoft.AspNetCore.Mvc;
using StoreApp.Data.Abstract;
using StoreApp.Data.Concrete;
using StoreApp.Web.Models;

namespace StoreApp.Web.Controllers;


public class OrderController : Controller
{   
    private Cart _cart;

    private readonly IOrderRepository _orderRepository;

    
    public OrderController(Cart cart,IOrderRepository orderRepository)
    {
        _cart = cart;
        _orderRepository = orderRepository;
    }

    [HttpGet]
    public IActionResult CheckOut()
    {
        return View(new OrderModel() { Cart = _cart });
    }

    [HttpPost]
    public IActionResult CheckOut(OrderModel model)
    {   
        if(_cart.Items.Count == 0)
        {
            ModelState.AddModelError("","Sorry, your cart is empty!");
        }

        if(ModelState.IsValid)
        {
            var order = new Order()
            {
                Name = model.Name,
                AdressLine = model.Address,
                City = model.City,
                Email = model.Email,
                Phone = model.Phone,
                OrderDate = DateTime.Now,
                OrderItems = _cart.Items.Select(i => new OrderItem()
                {
                    ProductId = i.Product.ProductId,
                    Quantity = i.Quantity,
                    Price = i.Product.Price
                }).ToList()

            };
            _orderRepository.CreateOrder(order);
            _cart.Clear();

            return RedirectToPage("/Completed" , new { orderId = order.Id });
        }else
        {   
            model.Cart = _cart;
            return View(model);
        }
    }

}