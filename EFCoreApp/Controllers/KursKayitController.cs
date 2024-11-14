using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EFCoreApp.Models;
using EFCoreApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EFCoreApp.Controllers;

public class KursKayitController : Controller
{   
    private readonly DataContext _context;

    public KursKayitController(DataContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {   
        var kursKayitlari = await _context.KursKayitlari.Include(k => k.Ogrenci).Include(k => k.Kurs).ToListAsync();
        return View(kursKayitlari);
    }

    public async Task<IActionResult> Create()
    {   
        ViewBag.Ogrenciler =  new SelectList( await _context.Ogrenciler.ToListAsync(), "OgrenciId", "AdSoyad");
        ViewBag.Kurslar = new SelectList( await _context.Kurslar.ToListAsync(), "KursId", "Baslik");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(KursKayit kursKayit)
    {   
        kursKayit.KayitTarihi = DateTime.Now;
        _context.KursKayitlari.Add(kursKayit);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
          
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {   
        var kursKayit = await _context.KursKayitlari.Include(k => k.Ogrenci).Include(k => k.Kurs).FirstOrDefaultAsync(k => k.KursKayitId == id);
        return View(kursKayit);
    }

    
    
}
