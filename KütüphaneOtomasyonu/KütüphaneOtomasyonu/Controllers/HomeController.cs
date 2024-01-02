using KütüphaneOtomasyonu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;

namespace KütüphaneOtomasyonu.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyDbContext _context;

        public static Member Memberdef=new Member();

        public HomeController(ILogger<HomeController> logger, MyDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Kullanıcı giriş bilgilerini kontrol et (örneğin, veritabanında kontrol edebilirsiniz)

            var members = _context.Members.ToList();
            var books = _context.Books.ToList();

            Member user = null;
            foreach (var u in members)
            {
                if (u.UserName.ToLower() == username.ToLower() && u.PasswordMember == password)
                {
                    user = u;
                    // Kullanıcının ödünç aldığı kitapları BooksBorrowed özelliğine ekle
                    foreach (Book book in books)
                    {
                        if (user.Id == book.Id)
                        {
                            if (!user.BooksBorrowed.Contains(book))
                            {
                                user.BooksBorrowed.Add(book);
                            }
                        }
                    }

                    Memberdef = user;

                    break;
                }
            }
            if (user != null)
            {
                // Kullanıcı girişi başarılı ise CurrentUser'ı ayarla
                _context.CurrentUser = user;

                HttpContext.Items["CurrentUser"] = user;
                // Kullanıcı girişi başarılı ise BookController'ın Index eylemine yönlendir
                if (user.AdminRol)
                {
                    return RedirectToAction("Index", "Member");
                }

                return RedirectToAction("Index", "Book");
            }

            // Giriş başarısız ise aynı sayfaya geri dön
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
