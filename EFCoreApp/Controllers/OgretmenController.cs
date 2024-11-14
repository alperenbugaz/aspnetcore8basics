using EFCoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreApp.Controllers;

public class OgretmenController : Controller
{   

    private readonly DataContext _context;

    public OgretmenController(DataContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {   
        var ogretmenler = await _context.Ogretmenler.ToListAsync();
        return View(ogretmenler);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {   
        
        var ogrenci = await _context.Ogretmenler.FirstOrDefaultAsync(o => o.OgretmenId == id);
        return View(ogrenci);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,Ogretmen ogretmen)
    {   
        if(id != ogretmen.OgretmenId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Ogretmenler.Update(ogretmen);
                await _context.SaveChangesAsync();

            }
            catch(DbUpdateConcurrencyException)
            {
                if (!_context.Ogretmenler.Any(e => e.OgretmenId == ogretmen.OgretmenId))
                {
                    return NotFound();
                }

                else
                {
                    throw;
                }

            }
            return RedirectToAction("Index");

        }

        //firsordefault da kullanılabilir
        return View(ogretmen);
    }
    [HttpGet]
    public IActionResult Create()
    {   

        return View();
    }

    [HttpPost]
    public async Task <IActionResult> Create(Ogretmen ogretmen)
    {   
        if (ModelState.IsValid)
        {
            _context.Ogretmenler.Add(ogretmen);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        return View();
    }
        [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var ogretmen = await _context.Ogretmenler.FindAsync(id);
        _context.Ogretmenler.Remove(ogretmen);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

}
