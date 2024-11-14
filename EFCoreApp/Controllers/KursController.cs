using EFCoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EFCoreApp.Controllers
{
    public class KursController : Controller 
    {       

        private readonly DataContext _context;
        public KursController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var kurslar = await _context.Kurslar.Include(k =>k.Ogretmen).ToListAsync();
            return View(kurslar);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {   
            ViewBag.Ogretmenler = new SelectList(await _context.Ogretmenler.ToListAsync(), "OgretmenId", "AdSoyad");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Kurs kurs)
        {   
            if (ModelState.IsValid)
            {
                _context.Kurslar.Add(kurs);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(kurs);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {  ViewBag.Ogretmenler = new SelectList(await _context.Ogretmenler.ToListAsync(), "OgretmenId", "AdSoyad");

            if (id == null)
            {
                return NotFound();
            }

            var kurs = await _context.Kurslar.Include(k => k.KursKayitlari).ThenInclude(k=>k.Ogrenci).FirstOrDefaultAsync(k => k.KursId == id);
            if (kurs == null)
            {
                return NotFound();
            }

            return View(kurs);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Kurs kurs)
        {   
            _context.Entry(kurs).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {


            var kurs = await _context.Kurslar.FindAsync(id);
            _context.Kurslar.Remove(kurs);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }



    }
}