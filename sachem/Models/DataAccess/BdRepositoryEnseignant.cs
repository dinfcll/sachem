using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace sachem.Models.DataAccess
{
    public class BdRepositoryEnseignant : IDataRepositoryEnseignant
    {

        private readonly SACHEMEntities db = new SACHEMEntities();

        public IEnumerable<Personne> AllEnseignantOrdered()
        {
            return db.Personne.AsNoTracking().OrderBy(p => p.Nom).ThenBy(p => p.Prenom);
        }
        public bool AnyEnseignantWhere(Expression<Func<Personne, bool>> condition)
        {
            return db.Personne.Any(condition);
        }
        public bool AnyGroupeWhere(Expression<Func<Groupe, bool>> condition)
        {
            return db.Groupe.Any(condition);
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

        public void DeclareModified(Personne enseignant)
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


        public void Dispose()
        {
            db.Dispose();
        }

        public SelectList liste_sexe()
        {
            return new SelectList(db.p_Sexe, "id_Sexe", "Sexe");
        }
        public SelectList liste_sexe(Personne personne)
        {
            return new SelectList(db.p_Sexe, "id_Sexe", "Sexe", personne.id_Sexe);
        }
        public SelectList liste_usag(int id_resp,int id_ens)
        {
            return new SelectList(db.p_TypeUsag.Where(x => x.id_TypeUsag == id_ens || x.id_TypeUsag == id_resp), "id_TypeUsag", "TypeUsag");
        }
        public SelectList liste_usag(Personne personne,int id_resp, int id_ens)
        {
            return new SelectList(db.p_TypeUsag.Where(x => x.id_TypeUsag == id_ens || x.id_TypeUsag == id_resp), "id_TypeUsag", "TypeUsag");
        }
    }
}
