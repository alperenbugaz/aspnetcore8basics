using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EFCoreApp.Models;
using EFCoreApp.Data;
using Microsoft.EntityFrameworkCore;

namespace EFCoreApp.Controllers;

public class OgrenciController : Controller
{   
    //Injection
    private readonly DataContext _context;

    public OgrenciController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Ogrenci ogrenci)
    {   
        if (ModelState.IsValid)
        {
            _context.Ogrenciler.Add(ogrenci);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        return View(ogrenci);
    }

    public async Task<IActionResult> Index()
    {   
        var ogrenciler = await _context.Ogrenciler.ToListAsync();
        return View(ogrenciler);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {   
        
        var ogrenci = await _context.Ogrenciler.Include(o => o.KursKayitlari).ThenInclude(o=>o.Kurs).FirstOrDefaultAsync(o => o.OgrenciId == id);
        return View(ogrenci);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,Ogrenci ogrenci)
    {   
        if(id != ogrenci.OgrenciId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Ogrenciler.Update(ogrenci);
                await _context.SaveChangesAsync();

            }
            catch(DbUpdateConcurrencyException)
            {
                if (!_context.Ogrenciler.Any(e => e.OgrenciId == ogrenci.OgrenciId))
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
        return View(ogrenci);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var ogrenci = await _context.Ogrenciler.FindAsync(id);
        _context.Ogrenciler.Remove(ogrenci);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}
