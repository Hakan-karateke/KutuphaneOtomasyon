using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KütüphaneOtomasyonu.Models;
using System.Linq;
using System.Threading.Tasks;
using KütüphaneOtomasyonu.Controllers;

public class BookController : Controller
{
    private readonly MyDbContext _context;

    public BookController(MyDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var books = await _context.Books.ToListAsync();
        ViewData["MemberLog"] = HomeController.Memberdef;
        return View(books);
    }

    [HttpGet] // Bu nitelik, bu metodun sadece HTTP GET istekleriyle çağrılmasını sağlar.

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    public IActionResult Borrow(int bookId)
    {
        ViewData["BookId"] = bookId;
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> BorrowConfirmed(int bookId)
    {
        var book = await _context.Books.FindAsync(bookId);

        if (book == null)
        {
            return NotFound();
        }

        if (book.Isborrowed)
        {
            // Kitap zaten ödünç alınmış, hata mesajı göster
            TempData["ErrorMessage"] = "Bu kitap zaten ödünç alınmış.";
        }
        else
        {
            // Kitabı ödünç al
            book.Isborrowed = true;
            book.MemberId = HomeController.Memberdef.Id;
            book.Day = DateTime.Now;

            _context.Update(book);
            await _context.SaveChangesAsync();
        }

        // Member controller'ındaki Details view sayfasına yönlendir
        return RedirectToAction("Details", "Member", HomeController.Memberdef);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Title,Author,Numberofpages,ImageUrl,CategoryId")] Book book)
    {
        // Default değerler atanabilir
        book.Isborrowed = false; // Varsayılan olarak kitap ödünç alınmamış olarak ayarlanır.

            _context.Add(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ReturnBook(int bookId, int memberId)
    {
        // Kitap ve üye var mı diye kontrol et
        var book = await _context.Books.FindAsync(bookId);
        var member = await _context.Members.FindAsync(memberId);

        if (book == null || member == null)
        {
            return NotFound();
        }

        // Kitabı iade et
        book.Isborrowed = false;
        book.MemberId = null;
        book.Day = null;

        _context.Update(book);
        await _context.SaveChangesAsync();

        // Üye detay sayfasına yönlendir
        return RedirectToAction("Details", "Member", new { id = memberId });
    }
}

