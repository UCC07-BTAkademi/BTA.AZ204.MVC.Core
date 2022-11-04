using Microsoft.AspNetCore.Mvc;
using MVC02_Views.Models;
using System.Diagnostics;

namespace MVC02_Views.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Deneme()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Personel()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Personel(Personel personel)
        {
            if (ModelState.IsValid)
            {
                string personelBilgi="";

                personelBilgi="Personelin bilgileri (Ad, Soyad, Yaş) : " + personel.Ad + " " + personel.Soyad + " - " + personel.Yas.ToString(); 
                
                ViewBag.Mesaj=personelBilgi; 
                
                

            }


            return View(personel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}