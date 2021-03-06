﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using sachem.Models;
using PagedList;
using sachem.Classes_Sachem;

namespace sachem.Controllers
{
    public class ParametresController : Controller
    {
        protected int NoPage = 1;
        private const int IdCourriel = 1;
        private readonly SACHEMEntities _db = new SACHEMEntities();

        [HttpGet]
        [ValidationAcces.ValidationAccesSuper]
        public ActionResult EditCourrier()
        {
            var courrier = _db.Courriel.First();
            ViewBag.id_TypeCourriel = new SelectList(_db.p_TypeCourriel, "id_TypeCourriel", "TypeCourriel");

            return View(courrier);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCourrier(Courriel courriel)
        {
            courriel.id_TypeCourriel = IdCourriel;
            if (courriel.DateFin != null)
            {
                if ((courriel.DateDebut - courriel.DateFin.Value).TotalDays > 0)
                {
                    ModelState.AddModelError(string.Empty, Messages.LongueurDeQuatreCaracteres);
                }
            }

            if (ModelState.IsValid)
            {
                _db.Entry(courriel).State = EntityState.Modified;
                _db.SaveChanges();
                TempData["Success"] = Messages.CourrielMisAJour();
            }
            return View();
        }

        [HttpGet]
        [ValidationAcces.ValidationAccesSuper]
        public ActionResult EditContact()
        {
            var contact = _db.p_Contact.First();
            contact.Telephone = SachemIdentite.RemettreTel(contact.Telephone);
            return View(contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidationAcces.ValidationAccesSuper]
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
                _db.Entry(contact).State = EntityState.Modified;
                _db.SaveChanges();

                TempData["Success"] = string.Format(Messages.NousContaterMisAJour());
                return View(contact);
            }
            return View(contact);
        }

        [ValidationAcces.ValidationAccesSuper]
        public ActionResult EditHoraire(int session = 0)
        {
            var idZero = _db.Session.OrderByDescending(y => y.id_Sess).FirstOrDefault();
            if (session == 0)
            {
                if (idZero != null) session = idZero.id_Sess;
            }
            ViewBag.idSess = session;
            var lhoraire = _db.p_HoraireInscription.OrderByDescending(y => y.id_Sess).FirstOrDefault(x => x.id_Sess == session);
            ViewBag.id_sess = Liste.ListeSession(session);
            if (idZero != null) ViewBag.idSessStable = idZero.id_Sess;

            return View(lhoraire);
        }


        
        [HttpPost]
        [ValidationAcces.ValidationAccesSuper]
        public ActionResult EditHoraire([Bind(Include = "id_Sess, DateDebut, DateFin, HeureDebut, HeureFin")] p_HoraireInscription horaireInscription)
        {
            var idSession = _db.Session.AsNoTracking().OrderByDescending(y => y.Annee).ThenByDescending(x => x.id_Saison).FirstOrDefault();
            var session = _db.Session.Find(horaireInscription.id_Sess);

            if (idSession != null && idSession.id_Sess == session.id_Sess)
            {
                if (session.Annee != horaireInscription.DateDebut.Year || session.Annee != horaireInscription.DateFin.Year)
                {
                    ModelState.AddModelError(string.Empty, Messages.DatesDansLaSession(session.Annee.ToString(), null));
                }

                if ((horaireInscription.DateFin - horaireInscription.DateDebut).TotalDays < 1)
                {
                    ModelState.AddModelError(string.Empty, Messages.ValidationDate());
                }

                switch (session.p_Saison.id_Saison)
                {
                    //Si hiver : de janvier inclus jusqua mai inclus (mois fin <= 5) pas besoin de verif la date de début
                    //car on est sur que c'est la bonne année et qu'elle est avant la date de fin
                    case 1:
                        if (horaireInscription.DateFin.Month > new DateTime(1, 5, 1).Month)
                        {
                            ModelState.AddModelError(string.Empty, Messages.DatesDansLaSession("d'hiver, janvier (01)", " à juin (06)"));
                        }
                        break;
                    //Si ete : de juin inclus jusqua aout inclus (si mois du début >= 6 et mois fin <= 8)
                    case 2:
                        if (new DateTime(1, 6, 1).Month > horaireInscription.DateDebut.Month || horaireInscription.DateFin.Month > new DateTime(1, 8, 1).Month)
                        {
                            ModelState.AddModelError(string.Empty, Messages.DatesDansLaSession("d'été, juin (06)", " à août (08)"));
                        }
                        break;
                    //si automne: de aout inclus jusqua decembre inclus (si mois du début >= 8 et mois fin <= 12)
                    //pas besoin de verif la date de fin car on est sur que c'est la bonne année et qu'elle est apres la date de début
                    case 3:
                        if (new DateTime(1, 8, 1).Month > horaireInscription.DateDebut.Month)
                        {
                            ModelState.AddModelError(string.Empty, Messages.DatesDansLaSession("d'hiver, août (08)", " à décembre (12)"));
                        }
                        break;
                }

                if (ModelState.IsValid)
                {
                    var sessionSurHi = _db.p_HoraireInscription.AsNoTracking().FirstOrDefault(x => x.id_Sess == session.id_Sess);

                    _db.Entry(horaireInscription).State = sessionSurHi == null ? EntityState.Added : EntityState.Modified;

                    TempData["Success"] = string.Format(Messages.HoraireMisAJour());
                    _db.SaveChanges();
                }
            }
            var idZero = _db.Session.OrderByDescending(y => y.id_Sess).FirstOrDefault();
            ViewBag.idSess = session.id_Sess;
            ViewBag.id_sess = Liste.ListeSession(session.id_Sess);
            if (idZero != null) ViewBag.idSessStable = idZero.id_Sess;

            return View(horaireInscription);
        }

        [HttpGet]
        [ValidationAcces.ValidationAccesSuper]
        public ActionResult EditCollege(string recherche, int? page)
        {
            var pageNumber = page ?? 1;
            ViewBag.Recherche = recherche;
            return View(Recherche(recherche).ToPagedList(pageNumber, 20));
        }

        [HttpPost]
        [ValidationAcces.ValidationAccesSuper]
        public void ModifCollege(string nomCollege, int? id)
        {
            if (_db.p_College.Any(r => r.id_College == id))
            {
                if (ModelState.IsValid)
                {
                    var college = _db.p_College.Find(id);
                    college.College = nomCollege;
                    _db.Entry(college).State = EntityState.Modified;
                    _db.SaveChanges();
                    TempData["Success"] = string.Format(Messages.CollegeModifie());
                }
                else
                {
                   TempData["Erreur"] = string.Format(Messages.CollegeDejaExistant());
                }
            }
        }

        [HttpPost]
        [ValidationAcces.ValidationAccesSuper]
        public void AddCollege(string nomCollege)
        {
            if (ModelState.IsValid)
            {
                var college = new p_College
                {
                    College = nomCollege
                };
                _db.p_College.Add(college);
                _db.SaveChanges();
                TempData["Success"] = string.Format(Messages.CollegeAjoute(nomCollege));
            }
            else
            {
                TempData["Erreur"] = string.Format(Messages.CollegeDejaExistant());
            }
        }

        [HttpPost]
        [ValidationAcces.ValidationAccesSuper]
        public void DeleteCollege(int? id)
        {
            var college = _db.p_College.Find(id);
            if (college != null)
            {
                _db.p_College.Remove(college);
                _db.SaveChanges();
                TempData["Success"] = string.Format(Messages.CollegeSupprime(college.College));
            }
        }

        private void ValiderContact([Bind(Include = "id_Contact,Nom,Prenom,Courriel,Telephone,Poste,Facebook,SiteWeb,Local")]p_Contact contact)
        {
            if (!_db.p_Contact.Any(r => r.id_Contact == contact.id_Contact))
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

            Session["DernRechCollege"] = recherche + ";" + NoPage;
            Session["DernRechCollegeUrl"] = Request.Url?.LocalPath;
            var college = from c in _db.p_College
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
            List<string> motsNonSignificatifs = new List<string> {
                "collège", "cégep", "collégial",
                "college", "cegep", "collegial",
                "de", "la", "du", "le", "les", "des" }; 
            var collegeFormater = new List<p_College>();
            int index;
            foreach(var element in college)
            {
                string construitPhraseEntreParentheses = "";
                string[] splitCollege = element.College.Split(' ');
                index = 0;
                while (index < splitCollege.Length-1 && 
                    motsNonSignificatifs.Exists(x => x.ToUpper().Equals(splitCollege[index].ToUpper())) && 
                    !(index > 0 && char.IsUpper(splitCollege[index][0])))
                {                 
                    construitPhraseEntreParentheses += splitCollege[index] + " ";
                    index++;
                }
                if (construitPhraseEntreParentheses.Length > 0)
                {
                    element.College = "";
                    for(int i=index;i<splitCollege.Length;i++)
                    {
                        element.College += splitCollege[i] + " ";
                    }
                    element.College = element.College.Remove(element.College.Length-1,1);
                    construitPhraseEntreParentheses = construitPhraseEntreParentheses.Remove(construitPhraseEntreParentheses.Length - 1);
                    construitPhraseEntreParentheses = char.ToUpper(construitPhraseEntreParentheses.First()) + construitPhraseEntreParentheses.Substring(1);
                    element.College += " (" + construitPhraseEntreParentheses + ")";
                    element.College = char.ToUpper(element.College.First()) + element.College.Substring(1);
                }
                collegeFormater.Add(element);
            }
            return collegeFormater.OrderBy(x => x.College).ToList();
        }
    }
}
