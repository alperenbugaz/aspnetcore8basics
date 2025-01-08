using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApp.Data.Abstract;
using StoreApp.Web.Models;

namespace StoreApp.Web.Controllers;

public class HomeController : Controller
{   
    public int pageSize = 5;
    private IStoreRepository _repository;
    
    private IMapper _mapper;

    public HomeController(IStoreRepository repository , IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }


    public IActionResult Index(string category,int page = 1)
    {


        return View(new ProductListViewModel
        {
            Products = _repository.GetProductsByCategory(category, page, pageSize)
            .Select(p => _mapper.Map<ProductViewModel>(p)),
            PageInfo = new PageInfo
            {
                ItemsPerPage = pageSize,
                CurrentPage = page,
                TotalItems = _repository.GetProductsCount(category)
            }
        });

    }
}