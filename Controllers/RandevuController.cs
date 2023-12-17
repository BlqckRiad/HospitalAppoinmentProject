using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using HospitalApp.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace HospitalApp.Controllers
{
    [Authorize]
    public class RandevuController : Controller
    {
        private readonly HospitalContext _context;

        public RandevuController(HospitalContext context)
        {
            _context = context;
        }

        // RandevuAl sayfasını gösteren action method
        public IActionResult RandevuAl()
        {
            var doktorlar = _context.Doktorlar
                .Include(d => d.Poliklinik)
                .Select(d => new SelectListItem
                {
                    Value = d.DoktorId.ToString(),
                    Text = $"{d.DoktorAd} - {(d.Poliklinik != null ? d.Poliklinik.PolName : "Belirtilmemiş")}"
                })
                .ToList();

            ViewBag.Doktorlar = doktorlar;

            return View();
        }

        // RandevuAl formu post edildiğinde çalışacak action method
        [HttpPost]
        public IActionResult RandevuAl(Randevu randevu)
        {
            if (ModelState.IsValid)
            {
                // Randevu kaydetme işlemleri
                _context.Randevular.Add(randevu);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home"); // Örneğin, anasayfaya yönlendirme
            }

            // ModelState geçerli değilse, sayfayı tekrar göster
            return View(randevu);
        }

        public IActionResult RandevuKontrol()
        {
            return View();
        }

        // TC kimliğine ait randevuları gösteren action method
        [HttpPost]
        public IActionResult RandevuKontrol(string hastaTC)
        {
            // TC kimliğine ait randevuları sorgula
            var randevular = _context.Randevular
                .Where(r => r.HastaTC == hastaTC)
                .ToList();

            if (randevular.Any())
            {
                return View("RandevuGoster", randevular);
            }
            else
            {
                ViewBag.Mesaj = "Girilen TC kimliğine ait randevu bulunmamaktadır.";
                return View();
            }
        }
    }
}

