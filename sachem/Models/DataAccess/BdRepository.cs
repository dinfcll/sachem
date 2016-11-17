using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace sachem.Models.DataAccess
{
    public class BdRepository : IDataRepository
    {
        private readonly SACHEMEntities db = new SACHEMEntities();

        public IEnumerable GetSessions()
        {
            return db.Session.AsNoTracking().OrderBy(s => s.Annee).ThenBy(s => s.p_Saison.Saison);
        }

        public bool AnyCoursWhere(Expression<Func<Cours, bool>> condition)
        {
            return db.Cours.Any(condition);
        }

        public bool AnyGroupeWhere(Expression<Func<Groupe, bool>> condition)
        {
            return db.Groupe.Any(condition);
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

        public Cours FindCours(int id)
        {
            return db.Cours.Find(id);
        }

        public void DeclareModified(Cours cours)
        {
            db.Entry(cours).State = EntityState.Modified;
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

        public Personne FindPersonne(int id)
        {
            return db.Personne.Find(id);
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
    }
}
