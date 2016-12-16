using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using sachem.Methodes_Communes;

namespace sachem.Models.DataAccess
{
    public class BdRepository : IDataRepository
    {
        private readonly SACHEMEntities _db = new SACHEMEntities();
        private const int Brouillon = 2;

        public IEnumerable GetSessions()
        {
            return _db.Session.AsNoTracking().OrderBy(s => s.Annee).ThenBy(s => s.p_Saison.Saison);
        }

        public IEnumerable GetCours()
        {
            return _db.Cours.AsNoTracking().OrderBy(c => c.Code);
        }

        public IEnumerable GetCollege()
        {
            return _db.p_College.AsNoTracking().OrderBy(n => n.College);
        }

        public IEnumerable GetStatut()
        {
            return _db.p_StatutCours.AsNoTracking().OrderBy(c => c.id_Statut);
        }

        public IQueryable<int> GetSpecificInscription(int id)
        {
            return from d in _db.Inscription where d.id_Pers == id select d.id_Inscription;
        }

        public bool AnyCoursWhere(Expression<Func<Cours, bool>> condition)
        {
            return _db.Cours.Any(condition);
        }

        public bool AnyGroupeWhere(Expression<Func<Groupe, bool>> condition)
        {
            return _db.Groupe.Any(condition);
        }


        public bool AnyCoursSuiviWhere(Expression<Func<CoursSuivi, bool>> condition)
        {
            return _db.CoursSuivi.Any(condition);
        }

        public int SessionEnCours()
        {
            return _db.Session.Max(s => s.id_Sess);
        }

        public IEnumerable<Cours> AllCours()
        {
            return _db.Cours;
        }

        public void AddCours(Cours cours)
        {
            _db.Cours.Add(cours);
            _db.SaveChanges();
        }

        public void AddCoursSuivi(CoursSuivi coursSuivi)
        {
            _db.CoursSuivi.Add(coursSuivi);
            _db.SaveChanges();
        }

        public Cours FindCours(int id)
        {
            return _db.Cours.Find(id);
        }

        public CoursSuivi FindCoursSuivi(int id)
        {
            return _db.CoursSuivi.Find(id);
        }

        public Personne FindPersonne(int id)
        {
            return _db.Personne.Find(id);
        }

        public void DeclareModified(Cours cours)
        {
            _db.Entry(cours).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void ModifyCoursSuivi(CoursSuivi coursSuivi)
        {
            _db.Entry(coursSuivi).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void RemoveCours(Cours cours)
        {
            _db.Cours.Remove(cours);
            _db.SaveChanges();
        }

        public IQueryable<Personne> IndexPersonne()
        {
            return _db.Personne.Include(p => p.p_Sexe).Include(p => p.p_TypeUsag);
        }

        public IEnumerable<p_Sexe> AllSexe()
        {
            return _db.p_Sexe;
        }

        public IEnumerable<p_TypeUsag> AllTypeUsag()
        {
            return _db.p_TypeUsag;
        }

        public void DeclareModifiedPers(Personne pers)
        {
            _db.Entry(pers).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void AddPersonne(Personne pers)
        {
            _db.Personne.Add(pers);
            _db.SaveChanges();
        }

        public void RemovePersonne(Personne pers)
        {
            _db.Personne.Remove(pers);
            _db.SaveChanges();
        }

        public void RemoveCoursSuivi(CoursSuivi coursSuivi)
        {
            _db.CoursSuivi.Remove(coursSuivi);
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void AddInscription(Inscription inscription)
        {
            _db.Inscription.Add(inscription);
            _db.SaveChanges();
        }

        public Inscription FindInscription(int id)
        {
            return _db.Inscription.Find(id);
        }

        public IEnumerable<Personne> AllEnseignantOrdered()
        {
            return _db.Personne.AsNoTracking().OrderBy(p => p.Nom).ThenBy(p => p.Prenom);
        }

        public bool AnyEnseignantWhere(Expression<Func<Personne, bool>> condition, Personne personne)
        {
            return _db.Personne.Any(condition);
        }

        public bool AnyjumelageWhere(Expression<Func<Jumelage, bool>> condition)
        {
            return _db.Jumelage.Any(condition);
        }

        public IEnumerable<Personne> AllEnseignant()
        {
            return _db.Personne.Where(c => c.id_TypeUsag == 2);
        }

        public IEnumerable<Personne> AllEnseignantResponsable(bool actif, int idResp, int idEns)
        {
            var enseignant = from c in _db.Personne
                where (c.id_TypeUsag == idEns || c.id_TypeUsag == idResp)
                      && c.Actif == actif
                orderby c.Nom, c.Prenom
                select c;
            return enseignant;
        }

        public void AddEnseignant(Personne enseignant)
        {
            _db.Personne.Add(enseignant);
            _db.SaveChanges();
        }

        public Personne FindEnseignant(int id)
        {
            return _db.Personne.Find(id);
        }

        public void DeclareModifiedEns(Personne enseignant)
        {
            _db.Entry(enseignant).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void RemoveEnseignant(int id)
        {
            var suppPersonne = _db.Personne.Where(x => x.id_Pers == id);
            _db.Personne.RemoveRange(suppPersonne);
            _db.SaveChanges();
        }

        public string FindMdp(int id)
        {
            var personne = _db.Personne.FirstOrDefault(x => x.id_Pers == id);
            if (personne == null) return "";
            var mdp = personne.MP;
            _db.Entry(personne).State = EntityState.Unchanged;
            _db.Entry(personne).State = EntityState.Detached;
            return mdp;
        }

        public SelectList ListeTypeUsager(int idResp, int idEns)
        {
            return new SelectList(_db.p_TypeUsag.Where(x => x.id_TypeUsag == idEns || x.id_TypeUsag == idResp),
                "id_TypeUsag", "TypeUsag");
        }

        public SelectList ListeSexe(int? sexe = 0)
        {
            if (sexe == null) sexe = 0;
            return new SelectList(_db.p_Sexe, "id_Sexe", "Sexe", sexe);
        }

        public SelectList ListeSession(int session = 0)
        {
            return new SelectList(GetSessions(), "id_Sess", "NomSession", session);
        }

        public SelectList ListePersonne(int idSession, int idPers)
        {
            var lPersonne = (from p in _db.Personne
                join c in _db.Groupe on p.id_Pers equals c.id_Enseignant
                where (p.id_TypeUsag == (int) TypeUsagers.Enseignant ||
                       p.id_TypeUsag == (int) TypeUsagers.Responsable) &&
                      p.Actif &&
                      c.id_Sess == (idSession == 0 ? c.id_Sess : idSession)
                orderby p.Nom, p.Prenom
                select p).Distinct();
            return new SelectList(lPersonne, "id_Pers", "NomPrenom", idPers);
        }

        public SelectList ListeCours(int cours = 0)
        {
            return new SelectList(GetCours(), "id_Cours", "CodeNom", cours);
        }

        public SelectList ListeCollege(int college = 0)
        {
            return new SelectList(GetCollege(), "id_College", "College", college);
        }

        public SelectList ListeStatutCours(int statut = 0)
        {
            return new SelectList(GetStatut(), "id_Statut", "Statut", statut);
        }

        public SelectList ListeEnseignant(int enseignant = 0)
        {
            return new SelectList(AllEnseignantOrdered(), "id_Pers", "Nom", enseignant);
        }

        public SelectList ListeSuperviseur(int superviseur = 0)
        {
            var lstEnseignant = from p in _db.Personne
                where p.id_TypeUsag == 2 && p.Actif
                orderby p.Nom, p.Prenom
                select p;
            return new SelectList(lstEnseignant, "id_Pers", "NomPrenom", superviseur);
        }

        public SelectList ListeTypeInscription(int typeInscription = 0)
        {
            return new SelectList(_db.p_TypeInscription.AsNoTracking().OrderBy(i => i.TypeInscription),
                "id_TypeInscription", "TypeInscription", typeInscription);
        }

        public SelectList ListeInscription(int inscription = 0)
        {
            var lInscription = from c in _db.Inscription select c;
            return new SelectList(lInscription, "id_Inscription", "Inscription", inscription);
        }

        public SelectList ListeStatutInscriptionSansBrouillon(int statut = 0)
        {
            var lStatut = from s in _db.p_StatutInscription where s.id_Statut != Brouillon select s;
            return new SelectList(lStatut, "id_Statut", "Statut", statut);
        }

        public List<string> ListeJours()
        {
            var jours = new List<string>();
            for (var i = (int) Semaine.Lundi; i < (int) Semaine.Samedi; i++)
            {
                jours.Add(((Semaine) i).ToString());
            }
            return jours.ToList();
        }

        public IEnumerable<Cours> ListeCoursSelonSession(int session)
        {
            return _db.Cours.AsNoTracking()
                .Where(c => c.Groupe.Any(g => (g.id_Sess == session || session == 0)))
                .OrderBy(c => c.Nom)
                .AsEnumerable();
        }

        public IEnumerable<Groupe> ListeGroupeSelonSessionEtCours(int cours, int session)
        {
            return _db.Groupe.AsNoTracking()
                .Where(p => (p.id_Sess == session || session == 0) && (p.id_Cours == cours || cours == 0))
                .OrderBy(p => p.NoGroupe);
        }

        public SelectList ListeStatutCours()
        {
            var lstStatut = from c in _db.p_StatutCours orderby c.id_Statut select c;
            return new SelectList(lstStatut, "id_Statut", "Statut");
        }

        public SelectList ListeTypesCourriels(int typeCourriel = 0)
        {
            var lCourriel = _db.p_TypeCourriel.AsNoTracking().OrderBy(i => i.id_TypeCourriel);
            return new SelectList(lCourriel, "id_TypeCourriel", "TypeCourriel", typeCourriel);
        }
    }
}
