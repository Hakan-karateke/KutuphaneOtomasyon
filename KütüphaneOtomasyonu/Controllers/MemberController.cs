using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KütüphaneOtomasyonu.Models;
using System.Linq;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;
public class MemberController : Controller
{
    private readonly MyDbContext _context;

    public MemberController(MyDbContext context)
    {
        _context = context;
    }

    // GET: Member
    public async Task<IActionResult> Index()
    {
        var members = await _context.Members.ToListAsync();
        return View(members);
    }
    // GET: Member/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var member = await _context.Members
            .FirstOrDefaultAsync(m => m.Id == id);
        var books=await _context.Books.ToListAsync();

        foreach (Book book in books)
        {
            if (member.Id == book.Id)
            {
                if (!member.BooksBorrowed.Contains(book))
                {
                    member.BooksBorrowed.Add(book);
                } //üyenin aldığı kitabları bul
            }
        }
        if (member == null)
        {
            return NotFound();
        }

        return View(member);
    }

    // GET: Member/Create
    public IActionResult Create()
    {
        return View();
    }
    // POST: Member/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Surname,phoneNumber,PasswordMember,UserName")] Member member)
    {
        member.phoneNumber = Convert.ToInt64(Request.Form["phoneNumber"]);
        member.AdminRol = false;
        if (ModelState.IsValid)
        {
            _context.Add(member);
            await _context.SaveChangesAsync();
            return RedirectToAction("Login","Home");
        }
        return View(member);
    }

    // GET: Member/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var member = await _context.Members.FindAsync(id);

        if (member == null)
        {
            return NotFound();
        }

        return View(member);
    }

    // POST: Member/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,PhoneNumber,PasswordMember,UserName,AdminRol")] Member member)
    {
        if (id != member.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(member);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberExists(member.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(member);
    }

    // GET: Member/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var member = await _context.Members
            .FirstOrDefaultAsync(m => m.Id == id);

        if (member == null)
        {
            return NotFound();
        }

        return View(member);
    }

    // POST: Member/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var member = await _context.Members.FindAsync(id);
        _context.Members.Remove(member);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MemberExists(int id)
    {
        return _context.Members.Any(e => e.Id == id);
    }
}
