using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace sachem.Models.DataAccess
{
    public class BdRepository : IDataRepository
    {
        private readonly SACHEMEntities db = new SACHEMEntities();

        public IEnumerable GetSessions()
        {
            return db.Session.AsNoTracking().OrderBy(s => s.Annee).ThenBy(s => s.p_Saison.Saison);
        }

        public IEnumerable GetCours()
        {
            return db.Cours.AsNoTracking().OrderBy(c => c.Code);
        }

        public IEnumerable GetCollege()
        {
            return db.p_College.AsNoTracking().OrderBy(n => n.College);
        }

        public IEnumerable GetStatut()
        {
            return db.p_StatutCours.AsNoTracking();
        }

        public System.Linq.IQueryable<int> GetSpecificInscription(int id)
        {
            return from d in db.Inscription where d.id_Pers == id select d.id_Inscription;
        }

        public bool AnyCoursWhere(Expression<Func<Cours, bool>> condition)
        {
            return db.Cours.Any(condition);
        }

        public bool AnyGroupeWhere(Expression<Func<Groupe, bool>> condition)
        {
            return db.Groupe.Any(condition);
        }


        public bool AnyCoursSuiviWhere(Expression<Func<CoursSuivi, bool>> condition)
        {
            return db.CoursSuivi.Any(condition);
        }

        public int SessionEnCours()
        {
            return db.Session.Max(s => s.id_Sess);
        }

        public IEnumerable<Cours> AllCours()
        {
            return db.Cours;
        }

        public void AddCours(Cours cours)
        {
            db.Cours.Add(cours);
            db.SaveChanges();
        }

        public void AddCoursSuivi(CoursSuivi coursSuivi)
        {
            db.CoursSuivi.Add(coursSuivi);
            db.SaveChanges();
        }

        public Cours FindCours(int id)
        {
            return db.Cours.Find(id);
        }

        public CoursSuivi FindCoursSuivi(int id)
        {
            return db.CoursSuivi.Find(id);
        }

        public Personne FindPersonne(int id)
        {
            return db.Personne.Find(id);
        }

        public void DeclareModified(Cours cours)
        {
            db.Entry(cours).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void ModifyCoursSuivi(CoursSuivi coursSuivi)
        {
            db.Entry(coursSuivi).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void RemoveCours(Cours cours)
        {
            db.Cours.Remove(cours);
            db.SaveChanges();
        }

        public IQueryable<Personne> IndexPersonne()
        {
            return db.Personne.Include(p => p.p_Sexe).Include(p => p.p_TypeUsag);
        }

        public IEnumerable<p_Sexe> AllSexe()
        {
            return db.p_Sexe;
        }

        public IEnumerable<p_TypeUsag> AllTypeUsag()
        {
            return db.p_TypeUsag;
        }

        public void DeclareModifiedPers(Personne pers)
        {
            db.Entry(pers).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void AddPersonne(Personne pers)
        {
            db.Personne.Add(pers);
            db.SaveChanges();
        }

        public void RemovePersonne(Personne pers)
        {
            db.Personne.Remove(pers);
            db.SaveChanges();
        }
        public void RemoveCoursSuivi(CoursSuivi coursSuivi)
        {
            db.CoursSuivi.Remove(coursSuivi);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public void AddInscription(Inscription inscription)
        {
            db.Inscription.Add(inscription);
            db.SaveChanges();
        }

        public Inscription FindInscription(int id)
        {
            return db.Inscription.Find(id);
        }

        public IEnumerable<Personne> AllEnseignantOrdered()
        {
            return db.Personne.AsNoTracking().OrderBy(p => p.Nom).ThenBy(p => p.Prenom);
        }
        public bool AnyEnseignantWhere(Expression<Func<Personne, bool>> condition, Personne personne)
        {
            return db.Personne.Any(condition);
        }

        public bool AnyjumelageWhere(Expression<Func<Jumelage, bool>> condition)
        {
            return db.Jumelage.Any(condition);
        }

        public IEnumerable<Personne> AllEnseignant()
        {
            return db.Personne.Where(c => c.id_TypeUsag == 2);
        }

        public IEnumerable<Personne> AllEnseignantResponsable(bool actif, int id_resp, int id_ens)
        {
            var Enseignant = from c in db.Personne
                             where (c.id_TypeUsag == id_ens || c.id_TypeUsag == id_resp)
                             && c.Actif == actif
                             orderby c.Nom, c.Prenom
                             select c;
            return Enseignant;
        }

        public void AddEnseignant(Personne enseignant)
        {
            db.Personne.Add(enseignant);
            db.SaveChanges();
        }

        public Personne FindEnseignant(int id)
        {
            return db.Personne.Find(id);
        }

        public void DeclareModifiedEns(Personne enseignant)
        {
            db.Entry(enseignant).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void RemoveEnseignant(int id)
        {
            var SuppPersonne = db.Personne.Where(x => x.id_Pers == id); // rechercher l'enseignant
            db.Personne.RemoveRange(SuppPersonne); // retirer toute les occurences de l'enseignant  
            db.SaveChanges();
            var lEnseignant = db.Personne.AsNoTracking().OrderBy(p => p.Nom).ThenBy(p => p.Prenom);
        }

        public SelectList liste_sexe()
        {
            return new SelectList(db.p_Sexe, "id_Sexe", "Sexe");
        }
        public SelectList liste_sexe(Personne personne)
        {
            return new SelectList(db.p_Sexe, "id_Sexe", "Sexe", personne.id_Sexe);
        }
        public SelectList liste_usag(int id_resp, int id_ens)
        {
            return new SelectList(db.p_TypeUsag.Where(x => x.id_TypeUsag == id_ens || x.id_TypeUsag == id_resp), "id_TypeUsag", "TypeUsag");
        }
        public SelectList liste_usag(Personne personne, int id_resp, int id_ens)
        {
            return new SelectList(db.p_TypeUsag.Where(x => x.id_TypeUsag == id_ens || x.id_TypeUsag == id_resp), "id_TypeUsag", "TypeUsag");
        }

        public string FindMdp(int id)
        {
            string mdp = db.Personne.Where(x => x.id_Pers == id).FirstOrDefault().MP;

            return mdp;
        }
    }
}
