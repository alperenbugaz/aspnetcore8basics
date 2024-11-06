using Microsoft.AspNetCore.Mvc;
using MeetingApp.Model;

namespace MeetingApp.Controllers
{
    public class MeetingController : Controller
    {

        [HttpGet]
        public IActionResult Apply()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Apply(UserInfo model)
        {   
            if(ModelState.IsValid)
            {
            Console.WriteLine("Name: " + model.Name);
            Repository.AddUser(model);
            ViewBag.UserCount = Repository.Users.Where(i=> i.WillAttend == true).Count();
            return View("Thanks", model);
            }else
            {
                return View(model);
            }




        }


        public IActionResult List()
        {   
            return View(Repository.Users);
        }

        public IActionResult Details(int id)
        {

            return View(Repository.GeyById(id));
        }

    }
}