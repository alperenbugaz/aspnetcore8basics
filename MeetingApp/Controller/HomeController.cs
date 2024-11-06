using Microsoft.AspNetCore.Mvc;
using MeetingApp.Model;

namespace MeetingApp.Controllers
{
    public class HomeController : Controller
    {   

        public IActionResult Index()
        {   int saat = DateTime.Now.Hour;
            ViewBag.selamla = saat > 12 ? "İyi Günler" : "Günaydın"; // ViewBag ile veri gönderme
            ViewBag.UserName = "Alperen";
            
            ViewData["selamla"] = saat > 12 ? "İyi Günler" : "Günaydın"; // ViewData ile veri gönderme
            ViewData["UserName"] = "Alperen";

            ViewBag.UserCount = Repository.Users.Where(i => i.WillAttend == true).Count();

            var meetinginfo = new MeetingInfo()
            {
                Id = 1,
                Location = "İstanbul",
                Date = DateTime.Now,


                NumberOfParticipants = 10
            };


            return View(meetinginfo);
        }
        
    }
}