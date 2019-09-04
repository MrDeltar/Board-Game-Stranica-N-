using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Board_Game_Stranica_N_.Ispis_PDF;
using Board_Game_Stranica_N_.Models;
using System.Threading.Tasks;
using PagedList;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Board_Game_Stranica_N_.Controllers
{
    [Authorize] //po defaultu sve je dostupno samo registriranim korisnicima
    public class DogadajController : Controller
    {
 
        // model podataka
        private DogadajiDbContext baza = new DogadajiDbContext();
        private ApplicationDbContext bazaKor = new ApplicationDbContext();

        //popis dogadaja
        [AllowAnonymous]// dozvoljeno pregled i ne-registriranim posjetiteljima
        //[HttpGet]
        public ViewResult PopisDogadaja(string sortiranje, string filter,string pretrazivanje, int? stranica)    //kód za 'PopisDogadaja' pomocu msdocs; https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/sorting-filtering-and-paging-with-the-entity-framework-in-an-asp-net-mvc-application
        {
            ViewBag.CurrentSort = sortiranje;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortiranje) ? "nazivDesc" : "";
            ViewBag.DateSortParm = sortiranje == "datumAsc" ? "datumDesc" : "datumAsc";

            if (pretrazivanje != null)
            {
                stranica = 1;
            }
            else
            {
                pretrazivanje = filter;
            }
            ViewBag.CurrentFilter = pretrazivanje;

            var dogadaji = from d in baza.Dogadaji select d;
            if (!String.IsNullOrEmpty(pretrazivanje))
            {
                dogadaji = dogadaji.Where(d => d.Naziv.Contains(pretrazivanje));
            }
            switch (sortiranje)
            {
                case "nazivDesc":
                    dogadaji = dogadaji.OrderByDescending(d => d.Naziv);
                    break;
                case "datumAsc":
                    dogadaji = dogadaji.OrderBy(d => d.DatumOdrzavanja);
                    break;
                case "datumDesc":
                    dogadaji = dogadaji.OrderByDescending(d => d.DatumOdrzavanja);
                    break;
                default:
                    dogadaji = dogadaji.OrderBy(d => d.Naziv);
                    break;
            }
            int kolStr = 100;
            int brojStr = (stranica ?? 1); //  null-coalescing operator ?? --> ako nije null, vraca 'stranica', a ako je null vraca '1'
            return View(dogadaji.ToPagedList(brojStr, kolStr));
        }

        // napravi dogadaj - get
        public ActionResult NapraviDogadaj()
        {
            return View();
        }

        // napravi dogadaj - post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NapraviDogadaj( Dogadaj dogadaj)
        {
            if (dogadaj.DatumOdrzavanja < DateTime.Now)
                ModelState.AddModelError("DatumOdrzavanja", "Datum održavanja ne može biti u prošlosti");
            if (ModelState.IsValid)
            {
                string userName = User.Identity.GetUserName();
                dogadaj.Veza = userName;
                baza.Dogadaji.Add(dogadaj);
                baza.SaveChanges();
                return RedirectToAction("PopisDogadaja");
            }

            return View(dogadaj);
        }

        // detaljni podaci dogadaja
        [AllowAnonymous]// dozvoljeno pregled i ne-registriranim posjetiteljima
        public ActionResult Detaljno(int? id)
        {
            ViewBag.Title = "Podaci o društevnoj igri";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // lambda izraz
            Dogadaj d = baza.Dogadaji.Find(id);
            if (d == null)
            {
                return HttpNotFound();
            }
            return View(d);
        }


        // ispis
        [AllowAnonymous]// dozvoljeno pregled i ne-registriranim posjetiteljima
        public FileStreamResult Ispis(int? id)
        {
            // lambda izraz
            Dogadaj d = baza.Dogadaji.Find(id);

            IspisDogadaja i = new IspisDogadaja(d);
            return new FileStreamResult(new MemoryStream(i.Podaci), "application/pdf");
        }


        // brisanje - get metoda
        public ActionResult BrisiDogadaj(int? id)
        {
            //lambda izraz
            Dogadaj d;
            d = baza.Dogadaji.Find(id);
            if (d == null)
            {
                return HttpNotFound();
            }
            else
            {
                //provjera je li korisnik organizator te igre /admin
                string veza = d.Veza;
                string userName = User.Identity.GetUserName();
                bool admin = HttpContext.User.IsInRole("Admin");
                if (userName == veza || admin==true)
                {
                    ViewBag.Title = "Brisanje društvene igre";
                    return View(d);
                }
                else
                {
                    return Obavijest(id);
                }
            }
        }

        // brisi - post metoda
        [HttpPost, ActionName("BrisiDogadaj")]
        //[ValidateAntiForgeryToken]
        public ActionResult BrisiPotvrda(int id)
        {
            Dogadaj d = baza.Dogadaji.Find(id);
            baza.Dogadaji.Remove(d);
            baza.SaveChanges();
            return RedirectToAction("PopisDogadaja");
        }

        //view za obavijest korisnika da ne moze brisati/azurirati jer on nije organizator
        public ActionResult Obavijest(int? id)
        {
            Dogadaj d = baza.Dogadaji.Find(id);
            return View("Obavijest", d);
        }

        //public ActionResult PosjetiOrganizatora(string UserName)
        //{
        //    //string user = UserName;
        //    ApplicationUser user;
        //    user = bazaKor.Users.Find(UserName);
        //    if (user == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    else
        //    {
        //        //provjera je li korisnik organizator te igre
        //        string userName = User.Identity.GetUserName();
        //        if (userName == UserName)
        //        {
        //            return View("~/Views/Manage/Index.cshtml");
        //        }
        //        else
        //        {
        //            return Posjeti(UserName);
        //        }
        //    }
        //}

        ////posjeti profil preko dogadaja
        //public ActionResult Posjeti(string UserName)
        //{
        //    ApplicationUser applicationUser = new ApplicationUser();
        //    var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        //    var user = manager.FindByEmail(UserName);
        //    var model = new ApplicationUser
        //    {
        //        Ime = user.Ime,
        //        Prezime = user.Prezime,
        //        Email = user.Email,
        //        DatumRodenja = user.DatumRodenja,
        //        Opis = user.Opis
        //    };
        //    return View(model);
        //}

        //azuriranje - get
        [HttpGet]
        public async Task<ActionResult> AzurirajDogadaj(int? id)
        {
            //lambda izraz
            Dogadaj d;
            d = baza.Dogadaji.Find(id);
            if (d == null)
            {
                return HttpNotFound();
            }
            else
            {
                //provjera je li korisnik organizator te igre
                string veza = d.Veza;
                string userName = User.Identity.GetUserName();
                if (userName == veza)
                {
                    ViewBag.Title = "Ažuriranje društvene igre";
                    return View(d);
                    //var igra = baza.Dogadaji.Where(m => m.Veza == userName);
                    //return View(igra);
                }
                else
                {
                    return Obavijest(id);
                }
            }

        }

        //azuriranje - post
        [HttpPost]
        public async Task<ActionResult> AzurirajDogadaj(Dogadaj dogadaj)
        {
            string userId = User.Identity.GetUserName();
            string veza = dogadaj.Veza;
            var igra = baza.Dogadaji.Where(m => m.Id == dogadaj.Id && m.Veza == userId);
            if (igra != null)
            {
                //validacija
                // provjera datuma odrzavanja
                if (dogadaj.DatumOdrzavanja < DateTime.Now)
                    ModelState.AddModelError("DatumOdrzavanja", "Datum odrzavanja ne može biti u prošlosti");
                // provjera ispravnosti modela
                if (ModelState.IsValid)
                {
                    // azuriranje u bazi
                    dogadaj.Veza = userId;
                    baza.Entry(dogadaj).State = EntityState.Modified;
                    baza.SaveChanges();
                    return RedirectToAction("PopisDogadaja", "Dogadaj");
                }
                else
                {
                    ViewBag.Title = "Ponovno ažuriranje društvene igre";
                    return View(dogadaj);
                }
            }
            return View();
        }

    }
}

