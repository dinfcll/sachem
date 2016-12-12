using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using sachem.Models;
using PagedList;
using static sachem.Classes_Sachem.ValidationAcces;

namespace sachem.Controllers
{
    public class ParametresController : Controller
    {
        protected int noPage = 1;
        private const int ID_COURRIEL = 1;
        private readonly SACHEMEntities db = new SACHEMEntities();

        [HttpGet]
        [ValidationAccesSuper]
        public ActionResult EditCourrier()
        {
            var courrier = db.Courriel.First();
            ViewBag.id_TypeCourriel = new SelectList(db.p_TypeCourriel, "id_TypeCourriel", "TypeCourriel");
            return View(courrier);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCourrier(Courriel courriel)
        {
            courriel.id_TypeCourriel = ID_COURRIEL;
            if (courriel.DateFin != null)
            {
                if ((courriel.DateDebut - courriel.DateFin.Value).TotalDays > 0)
                {
                    ModelState.AddModelError(string.Empty, Messages.C_005);
                }
            }

            if (ModelState.IsValid)
            {
                db.Entry(courriel).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Success"] = Messages.I_032();
            }
            return View();
        }

        [HttpGet]
        [ValidationAccesSuper]
        public ActionResult EditContact()
        {
            var contact = db.p_Contact.First();
            contact.Telephone = SachemIdentite.RemettreTel(contact.Telephone);
            return View(contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidationAccesSuper]
        public ActionResult EditContact([Bind(Include = "id_Contact,Nom,Prenom,Courriel,Telephone,Poste,Facebook,SiteWeb,Local")] p_Contact contact)
        {
            ValiderContact(contact);
            string site = contact.SiteWeb;
            string facebook = contact.Facebook;
               
            if (!site.StartsWith("https://") || site.StartsWith("http://"))
            {
                site = "https://" + site;
            }
            if (!facebook.StartsWith("https://") || facebook.StartsWith("http://"))
            {
                facebook = "https://" + facebook;
            }
            contact.Facebook = facebook;
            contact.SiteWeb = site;

            if (ModelState.IsValid)
            {
                contact.Telephone = SachemIdentite.FormatTelephone(contact.Telephone);
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();

                TempData["Success"] = string.Format(Messages.I_031());
                return View(contact);
            }
            return View(contact);
        }

        //Méthode qui envoie a la view Edit horaire la liste de toutes les horaires d'inscription ainsi que l'horaire de la session courrante
        [ValidationAccesSuper]
        public ActionResult EditHoraire(int session = 0)
        {
            var idZero = db.Session.OrderByDescending(y => y.id_Sess).FirstOrDefault();
            if (session == 0)
            {
                session = idZero.id_Sess;
            }
            ViewBag.idSess = session;
            var lhoraire = db.p_HoraireInscription.OrderByDescending(y => y.id_Sess).Where(x => x.id_Sess == session).FirstOrDefault();
            ViewBag.id_sess = Liste.ListeSession(session);
            ViewBag.idSessStable = idZero.id_Sess;         
            return View(lhoraire);
        }


        
        [HttpPost]
        [ValidationAccesSuper]
        public ActionResult EditHoraire([Bind(Include = "id_Sess, DateDebut, DateFin, HeureDebut, HeureFin")] p_HoraireInscription HI)
        {
            var id_Session = db.Session.AsNoTracking().OrderByDescending(y => y.Annee).ThenByDescending(x => x.id_Saison).FirstOrDefault();
            Session session = db.Session.Find(HI.id_Sess);
            if (id_Session.id_Sess == session.id_Sess)
            {
                if (session.Annee != HI.DateDebut.Year || session.Annee != HI.DateFin.Year)
                {
                    ModelState.AddModelError(string.Empty, Messages.C_006(session.Annee.ToString(), null));
                }

                if ((HI.DateFin - HI.DateDebut).TotalDays < 1)
                {
                    ModelState.AddModelError(string.Empty, Messages.C_005);
                }

                switch (session.p_Saison.id_Saison)
                {
                    //Si hiver : de janvier inclus jusqua mai inclus (mois fin <= 5) pas besoin de verif la date de début
                    //car on est sur que c'est la bonne année et qu'elle est avant la date de fin
                    case 1:
                        if (HI.DateFin.Month > new DateTime(1, 5, 1).Month)
                        {
                            ModelState.AddModelError(string.Empty, Messages.C_006("d'hiver, janvier (01)", " à juin (06)"));
                        }
                        break;
                    //Si ete : de juin inclus jusqua aout inclus (si mois du début >= 6 et mois fin <= 8)
                    case 2:
                        if (new DateTime(1, 6, 1).Month > HI.DateDebut.Month || HI.DateFin.Month > new DateTime(1, 8, 1).Month)
                        {
                            ModelState.AddModelError(string.Empty, Messages.C_006("d'été, juin (06)", " à août (08)"));
                        }
                        break;
                    //si automne: de aout inclus jusqua decembre inclus (si mois du début >= 8 et mois fin <= 12)
                    //pas besoin de verif la date de fin car on est sur que c'est la bonne année et qu'elle est apres la date de début
                    case 3:
                        if (new DateTime(1, 8, 1).Month > HI.DateDebut.Month)
                        {
                            ModelState.AddModelError(string.Empty, Messages.C_006("d'hiver, août (08)", " à décembre (12)"));
                        }
                        break;
                }

                if (ModelState.IsValid)
                {
                    var SessionSurHI = db.p_HoraireInscription.AsNoTracking().FirstOrDefault(x => x.id_Sess == session.id_Sess);
                    if (SessionSurHI == null)
                    {
                        db.Entry(HI).State = EntityState.Added;
                    }
                    else
                    {
                        db.Entry(HI).State = EntityState.Modified;
                    }
                    db.SaveChanges();
                }
            }
            var idZero = db.Session.OrderByDescending(y => y.id_Sess).FirstOrDefault();
            ViewBag.idSess = session.id_Sess;
            ViewBag.id_sess = Liste.ListeSession(session.id_Sess);
            ViewBag.idSessStable = idZero.id_Sess;
            return View(HI);
        }

        [HttpGet]
        [ValidationAccesSuper]
        public ActionResult EditCollege(string recherche, int? page)
        {
            var pageNumber = page ?? 1;
            ViewBag.Recherche = recherche;
            return View(Recherche(recherche).ToPagedList(pageNumber, 20));
        }

        [HttpPost]
        [ValidationAccesSuper]
        public void ModifCollege(string nomCollege, int? id)
        {
            if (db.p_College.Any(r => r.id_College == id))
            {
                ValiderCollege(nomCollege);
                if (ModelState.IsValid)
                {
                    var college = db.p_College.Find(id);
                    college.College = nomCollege;
                    db.Entry(college).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["Success"] = string.Format(Messages.I_046());
                }
                else
                {
                   TempData["Erreur"] = "Ce collège d'enseignement existe déjà";
                }
            }
        }

        [HttpPost]
        [ValidationAccesSuper]
        public void AddCollege(string nomCollege)
        {
            ValiderCollege(nomCollege);
            if (ModelState.IsValid)
            {
                var college = new p_College
                {
                    College = nomCollege
                };
                db.p_College.Add(college);
                db.SaveChanges();
                TempData["Success"] = string.Format(Messages.I_044(nomCollege));
            }
            else
            {
                TempData["Erreur"] = "Ce collège d'enseignement existe déjà";
            }
        }

        [HttpPost]
        [ValidationAccesSuper]
        public void DeleteCollege(int? id)
        {
            var college = db.p_College.Find(id);
            if (college != null)
            {
                db.p_College.Remove(college);
                db.SaveChanges();
                TempData["Success"] = string.Format(Messages.I_047(college.College));
            }
        }
        private void ValiderCollege(string college)
        {
            if (db.p_College.Any(p => p.College == college))
            {
                ModelState.AddModelError(string.Empty,"Ce collège d'enseignement existe déjà");
            }
        }
        private void ValiderContact([Bind(Include = "id_Contact,Nom,Prenom,Courriel,Telephone,Poste,Facebook,SiteWeb,Local")]p_Contact contact)
        {
            if (!db.p_Contact.Any(r => r.id_Contact == contact.id_Contact))
                ModelState.AddModelError(string.Empty, " ");
        }

        private IEnumerable<p_College> Recherche(string recherche)
        {
            if (Request.RequestType == "GET" && Session["DernRechCollege"] != null && (string)Session["DernRechCollegeUrl"] == Request.Url?.LocalPath)
            {
                var tanciennerech = Session["DernRechCollege"].ToString().Split(';');
                if (tanciennerech[0].Length != 0)
                {
                    recherche = tanciennerech[0];
                    ViewBag.Recherche = recherche;
                }
            }

            Session["DernRechCollege"] = recherche + ";" + noPage;
            Session["DernRechCollegeUrl"] = Request.Url?.LocalPath;
            var college = from c in db.p_College
                          select c;

            var collegeFormater = Formatage(college);

            if (!String.IsNullOrEmpty(recherche))
            {
                collegeFormater = collegeFormater.FindAll(c => c.College.ToLower().Contains(recherche.ToLower()));
            }

            return collegeFormater;
        }

        private List<p_College> Formatage(IQueryable<p_College> college)
        {
            List<string> motsNonSignificatifs = new List<string> { "Collège", "Cégep", "Collégial", "de", "la", "du", "le", "les", "des" };
            string construitPhraseEntreParentheses;
            var collegeFormater = new List<p_College>();
            foreach(var element in college)
            {
                construitPhraseEntreParentheses = "";
                string[] splitCollege = element.College.Split(' ');
                for(int i = 0; i < splitCollege.Length && motsNonSignificatifs.Contains(splitCollege[i]); i++)
                {
                    construitPhraseEntreParentheses += splitCollege[i] + " ";
                }                
                if (construitPhraseEntreParentheses.Length > 0)
                {
                    construitPhraseEntreParentheses = construitPhraseEntreParentheses.Remove(construitPhraseEntreParentheses.Length - 1);
                    element.College = element.College.TrimStart(construitPhraseEntreParentheses.ToCharArray());                    
                    if(element.College.StartsWith(" "))
                    {
                        element.College = element.College.Remove(0, 1);
                    }
                    element.College += " (" + construitPhraseEntreParentheses + ")";
                    element.College = char.ToUpper(element.College.First()) + element.College.Substring(1);
                }
                collegeFormater.Add(element);
            }
            return collegeFormater.OrderBy(x => x.College).ToList();
        }
    }
}
