// UserInfoViewComponent.cs

using Microsoft.AspNetCore.Mvc;
using KütüphaneOtomasyonu.Models;
using KütüphaneOtomasyonu.Controllers;

public class UserInfoViewComponent : ViewComponent
{
    private readonly MyDbContext _context;

    public UserInfoViewComponent(MyDbContext context)
    {
        _context = context;
    }

    public IViewComponentResult Invoke()
    {
        Member currentUser = HomeController.Memberdef;
        return View(currentUser);
    }

}
