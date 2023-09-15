using Microsoft.AspNetCore.Mvc;
using System;
using X.PagedList;
using KisiselAdresDefteri.Web.Models;

namespace KisiselAdresDefteri.Web.App.Controllers
{
    public class KisiselController : Controller
    {
        private AppDbContext _context;

        private readonly KisiRepository _kisiselRepository;
        public KisiselController(AppDbContext context)
        {
            //DI Container
            //Dependency Injection Pattern
            _kisiselRepository = new KisiRepository();
            _context = context;
        }

        public IActionResult Index(string searchString, int page2 = 1)
        {
            //isme göre arama işlemi

            var kisilers = from p in _context.kisiler
                         select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                kisilers = kisilers.Where(p => p.Name.Contains(searchString));
            }
            var personCount = _context.kisiler.ToList();
            ViewData["status"] = personCount.Count();

            return View(kisilers.ToPagedList(page2, 7));

        }
        //veri silme
        public IActionResult Remove(int id)
        {
            var person = _context.kisiler.Find(id);
            _context.kisiler.Remove(person);
            _context.SaveChanges();
            TempData["Status"] = "Kişi Başarıyla Silindi !";
            return RedirectToAction("Index");
        }
        [HttpGet]
        //veri ekleme 
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Kisiler newKisiler)
        {
            //Person newPerson = new Person() { Name = Name, Surname = Surname, PhoneNumber = PhoneNumber, Email = Email, Address = Address };

            _context.kisiler.Add(newKisiler);
            _context.SaveChanges();

            //Ürün başarıyla eklendi bildirimi verme
            TempData["Status"] = "Kişi Başarıyla Eklendi !";

            return RedirectToAction("Index");
        }

        //veri güncelleme
        [HttpGet]
        public IActionResult Update(int id)
        {
            var person = _context.kisiler.Find(id);
            return View(person);
        }
        [HttpPost]
        public IActionResult Update(Kisiler updateKisiler, int personId)
        {
            updateKisiler.Id = personId;
            _context.kisiler.Update(updateKisiler);
            _context.SaveChanges();

            //Ürün güncellendi bildirimi verme
            TempData["Status"] = "Kişi Başarıyla Güncellendi !";

            return RedirectToAction("Index");
        }

        //kişi listeleme 
        public IActionResult PersonsList(string searchString, int page = 1)
        {
            //isme göre arama işlemi

            var person = from p in _context.kisiler
                         select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                person = person.Where(p => p.Name.Contains(searchString));
            }

            var personCount = _context.kisiler.ToList();
            ViewData["status"] = personCount.Count();

            return View(person.ToPagedList(page, 10));
        }

        // Kişi Detay getirme
        [HttpGet]
        public IActionResult PersonDetail(int id)
        {
            var person = _context.kisiler.Find(id);
            return View(person);
        }
    }
}
