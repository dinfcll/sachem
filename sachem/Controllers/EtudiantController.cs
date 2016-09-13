using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using sachem.Models;
using PagedList;
using System.Web.Services;

namespace sachem.Controllers
{
    public class EtudiantController : RechercheEtudiantController
    {
       


        public string ProgEtude()
        {

            return "allo";
        }

        #region enCommentaire
        // GET: Etudiant
        //public ActionResult Index()
        //{
        //    List<object> lpersonne = new List<object>();

        //    //var test = from d in db.Personne
        //    //           where 

        //    //var test = from d in db.EtuProgEtude
        //    //    where d.id_Etu == d.Personne.id_Pers && d.id_EtuProgEtude == d.ProgrammeEtude.id_ProgEtu
        //    //    select d;

        //    var personne = from c in db.Personne
        //                   where c.Actif == true && c.id_TypeUsag == 1
        //                   select c;
        //    int nbpers = personne.Count();

        //    //afficher tous les étudiants
        //    //foreach (var pers in personne)
        //    //{
        //    //    var pEtu = (from p in db.EtuProgEtude
        //    //               where pers.id_Pers == p.id_Etu
        //    //                orderby p.id_Sess descending
        //    //                select p).FirstOrDefault();

        //    //}


        //    //pers.ProgEtu = ProgEtu.ToString();
        //    //// PROBLEME A REGLER, olivier filion a un programme qui n'existe pas dans BD programma etude





        //    //var id = db.EtuProgEtude.Find(pers.id_Pers);
        //    //    if (id != null)
        //    //        {
        //    //            pers.idpEtu = id.id_ProgEtu;
        //    //            var pEtu = db.ProgrammeEtude.Find(id.id_ProgEtu);
        //    //            string pe = pEtu.NomProg;
        //    //            pers.ProgEtu = pe;
        //    //            i = i + 1;
        //    //        }


        //    //i = i + 0;
        //    //    var pEtu = (from p in db.EtuProgEtude
        //    //                where
        //    //                orderby p.id_Sess descending//FirstOrDefault
        //    //                select p).FirstOrDefault();
        //    //string pe = pEtu.NomProg;
        //    //pers.ProgEtu = pe;
        //    //var pEtu = from d in db.ProgrammeEtude
        //    //           where d.id_ProgEtu == id.id_ProgEtu
        //    //           orderby d.Annee descending
        //    //           select d;
        //    //var pEtu = db.ProgrammeEtude.Find();
        //    //string pe = pEtu.FirstOrDefault().ToString();
        //    //pers.ProgEtu = pe;
        //    //(From p In context.Persons Select p Order By age Descending).FirstOrDefault
        //    //}

        //    ListeSession();
        //    ListeCours();
        //    ListeGroupe();


        //    return View(personne.ToList());
        //}

        #endregion

        public ActionResult Index(int? page)
        {
            noPage = (page ?? noPage);
           
            var personne = from c in db.Personne   
                           where c.Actif == true && c.id_TypeUsag == 1
                           select c;

            return View(Rechercher().ToPagedList(noPage, 20));
        }

        #region enCommentaire
        //private IEnumerable<Personne> Rechercher()
        //{
        //    var personne = from c in db.Personne
        //                   where c.Actif == true && c.id_TypeUsag == 1
        //                   select c;
        //        if (!String.IsNullOrEmpty(recherche))
        //        {
        //            personne = personne.Where(c => c.Matricule7.Contains() || c.NomProg.Contains(recherche)) as IOrderedQueryable<ProgrammeEtude>;
        //        }
        //}

        //    //var personne = from c in db.Personne
        //    //               where c.Actif == true && c.id_TypeUsag == 1
        //    //               select c;
        //    //if (Request.RequestType == "POST")
        //    //{
        //    //    string m = ViewBag.Mat;
        //    //    if (!String.IsNullOrEmpty(m))
        //    //    {
        //    //        //personne = null;
        //    //        personne = from c in db.Personne
        //    //                   where c.Actif == true && c.id_TypeUsag == 1
        //    //                   && c.Matricule7.Contains(m)
        //    //                   select c;
        //    //    }
        //    //}

        //    //foreach (var pers in personne)
        //    //{
        //    //    var pidEtu = (from p in db.EtuProgEtude
        //    //                  where pers.id_Pers == p.id_Etu
        //    //                  orderby p.id_Sess descending
        //    //                  select p).FirstOrDefault();

        //    //    var pEtu = db.ProgrammeEtude.Find(pidEtu.id_ProgEtu);
        //    //    pers.ProgEtu = pEtu.NomProg.ToString();

        //    //}

        //    //on enregistre la recherche
        //    //Session["DernRechCours"] = sess + ";" + actif;
        //    //Session["DernRechCoursUrl"] = Request.Url?.LocalPath;


        //    ListeSession();
        //    ListeCours();
        //    ListeGroupe();
        //    return personne.ToList();
        //}


        //private IEnumerable<Cours> Rechercher()
        //{
        //    var pers = 0;
        //    var actif = true;

        //    //Pour accéder à la valeur de cle envoyée en GET dans le formulaire
        //    //Request.QueryString["cle"]
        //    //Pour accéder à la valeur cle envoyée en POST dans le formulaire
        //    //Request.Form["cle"]
        //    //Cette méthode fonctionnera dans les 2 cas
        //    //Request["cle"]

        //    if (Request.RequestType == "GET" && personne["DernRechCours"] != null && (string)personne["DernRechCoursUrl"] == Request.Url?.LocalPath)
        //    {
        //        var anciennerech = (string)personne["DernRechCours"];
        //        var tanciennerech = anciennerech.Split(';');

        //        if (tanciennerech[0] != "")
        //        {
        //            pers = int.Parse(tanciennerech[0]);
        //        }
        //        if (tanciennerech[1] != "")
        //        {
        //            actif = bool.Parse(tanciennerech[1]);
        //        }

        //    }
        //    else
        //    {
        //        //La méthode String.IsNullOrEmpty permet à la fois de vérifier si la chaine est NULL (lors du premier affichage de la page ou vide, lorsque le paramètre n'est pas appliquée 
        //        if (!string.IsNullOrEmpty(Request.Form["personne"]))
        //            pers = Convert.ToInt32(Request.Form["personne"]);
        //        //si la variable est null c'est que la page est chargée pour la première fois, donc il faut assigner la session à la session en cours, la plus grande dans la base de données
        //        else if (Request.Form["Session"] == null)
        //            pers = db.Personne.Max(s => s.id_Sess);

        //        //la méthode Html.checkbox crée automatiquement un champ hidden du même nom que la case à cocher, lorsque la case n'est pas cochée une seule valeur sera soumise, par contre lorsqu'elle est cochée
        //        //2 valeurs sont soumises, il faut alors vérifier que l'une des valeurs est à true pour vérifier si elle est cochée
        //        if (!string.IsNullOrEmpty(Request.Form["Actif"]))
        //            actif = Request.Form["Actif"].Contains("true");
        //    }

        //    ViewBag.Actif = actif;

        //    ListeSession(pers);

        //    //var personne = from c in db.Cours
        //    //            where (db.Groupe.Any(r => r.id_Cours == c.id_Cours && r.id_Sess == sess) || sess == 0)
        //    //            && c.Actif == actif
        //    //            orderby c.Code
        //    //            select c;

        //    var personne = from c in db.Personne
        //                   where c.Actif == true && c.id_TypeUsag == 1
        //                   && c.id
        //                   select c;


        //    //on enregistre la recherche
        //    personne["DernRechCours"] = pers + ";" + actif;
        //    personne["DernRechCoursUrl"] = Request.Url?.LocalPath;

        //    return personne.ToList();
        //}
        #endregion

      
        // GET: Etudiant/Details/5

        // GET: Etudiant/Create
        public ActionResult Create()
        {
            ViewBag.id_Sexe = new SelectList(db.p_Sexe, "id_Sexe", "Sexe");
            ViewBag.id_TypeUsag = new SelectList(db.p_TypeUsag, "id_TypeUsag", "TypeUsag");
            return View();
        }

        // POST: Etudiant/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_Pers,id_Sexe,id_TypeUsag,Nom,Prenom,NomUsager,Matricule,MP,Courriel,Telephone,DateNais,Actif")] Personne personne)
        {
            if (ModelState.IsValid)
            {
                db.Personne.Add(personne);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_Sexe = new SelectList(db.p_Sexe, "id_Sexe", "Sexe", personne.id_Sexe);
            ViewBag.id_TypeUsag = new SelectList(db.p_TypeUsag, "id_TypeUsag", "TypeUsag", personne.id_TypeUsag);
            return View(personne);
        }

        // GET: Etudiant/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personne personne = db.Personne.Find(id);
            if (personne == null)
            {
                return HttpNotFound();
            }
            //var Prog = from d in db.ProgrammeEtude
              //         where personne.ProgEtu == d.NomProg
                //       select d;

            ViewBag.id_Sexe = new SelectList(db.p_Sexe, "id_Sexe", "Sexe", personne.id_Sexe);
            ViewBag.id_TypeUsag = new SelectList(db.p_TypeUsag, "id_TypeUsag", "TypeUsag", personne.id_TypeUsag);
            //ViewBag.id_Programme = new SelectList(db.ProgrammeEtude, "id_ProgEtu", "nomProg", personne.idProgEtu);
            //ViewBag.id_Session = new SelectList(db.Session, "id_Sess", "NomSession",)
            //return View(Tuple.Create(personne,Prog));
            return View(personne);
            //tuple a faire
            //ou faire une liste de prog dans la classe personne
            //faire des viewbag pour voir les données
        }

        public void FillDropDownlist()
        {
            
        }
        // POST: Etudiant/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_Pers,id_Sexe,id_TypeUsag,Nom,Prenom,NomUsager,Matricule,MP,Courriel,Telephone,DateNais,Actif")] Personne personne)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personne).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_Sexe = new SelectList(db.p_Sexe, "id_Sexe", "Sexe", personne.id_Sexe);
            ViewBag.id_TypeUsag = new SelectList(db.p_TypeUsag, "id_TypeUsag", "TypeUsag", personne.id_TypeUsag);
            return View(personne);
        }

        // GET: Etudiant/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personne personne = db.Personne.Find(id);
            if (personne == null)
            {
                return HttpNotFound();
            }
            
            return View(personne);
        }

        // POST: Etudiant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Personne personne = db.Personne.Find(id);

            var etuProgEtu = db.EtuProgEtude.Where(x => x.id_Etu == personne.id_Pers);
            db.EtuProgEtude.RemoveRange(etuProgEtu);
            var groupeEtu = db.GroupeEtudiant.Where(y => y.id_Etudiant == personne.id_Pers);
            db.GroupeEtudiant.RemoveRange(groupeEtu);
            var Jumul = db.Jumelage.Where(z => z.id_InscEleve == personne.id_Pers);
            db.Jumelage.RemoveRange(Jumul);
            var Inscri = db.Inscription.Where(a => a.id_Pers == personne.id_Pers);
            db.Inscription.RemoveRange(Inscri);
            var CoursSuiv = db.CoursSuivi.Where(b => b.id_Pers == personne.id_Pers);
            db.CoursSuivi.RemoveRange(CoursSuiv);



            db.Personne.Remove(personne);
            
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
