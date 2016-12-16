using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using sachem.Models;
using sachem.Models.DataAccess;

namespace sachemTests
{
    internal class TestRepository : IDataRepository
    {
        private readonly List<Cours> _listeCours = new List<Cours>();
        private readonly List<Inscription> _listeInscription = new List<Inscription>();

        public IEnumerable GetSessions()
        {
            throw new NotImplementedException();
        }

        public bool AnyCoursWhere(Expression<Func<Cours, bool>> condition)
        {
            var nouvelleListe = _listeCours.AsQueryable();
            return nouvelleListe.Any(condition);
        }

        public bool AnyGroupeWhere(Expression<Func<Groupe, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public int SessionEnCours()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cours> AllCours()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Inscription> AllInscriptions()
        {
            throw new NotImplementedException();
        }

        public void AddCours(Cours cours)
        {
            _listeCours.Add(cours);
        }

        public Cours FindCours(int id)
        {
            return _listeCours.Find(x => x.id_Cours == id);
        }

        public void AddInscription(Inscription inscription)
        {
            _listeInscription.Add(inscription);
        }

        public Inscription FindInscription(int id)
        {
            return _listeInscription.Find(x => x.id_Inscription == id);
        }

        public void DeclareModified(Cours cours)
        {
            throw new NotImplementedException();
        }

        public void RemoveCours(Cours cours)
        {
            throw new NotImplementedException();
        }

        public Personne FindPersonne(int id)
        {
            return new Personne();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void AddCoursSuivi(CoursSuivi coursSuivi)
        {
            //Nothing
        }

        public IEnumerable GetCours()
        {
            throw new NotImplementedException();
        }

        public IEnumerable GetCollege()
        {
            throw new NotImplementedException();
        }

        public IEnumerable GetStatut()
        {
            throw new NotImplementedException();
        }

        public IQueryable<int> GetSpecificInscription(int id)
        {
            throw new NotImplementedException();
        }

        public CoursSuivi FindCoursSuivi(int id)
        {
            throw new NotImplementedException();
        }

        public void ModifyCoursSuivi(CoursSuivi coursSuivi)
        {
            throw new NotImplementedException();
        }

        public void RemoveCoursSuivi(CoursSuivi coursSuivi)
        {
            throw new NotImplementedException();
        }

        public bool AnyCoursSuiviWhere(Expression<Func<CoursSuivi, bool>> condition)
        {
            throw new NotImplementedException();
        }

        private readonly List<Personne> _listeEnseignant = new List<Personne>();

        public void AddEnseignant(Personne enseignant)
        {
            _listeEnseignant.Add(enseignant);
        }

        public IEnumerable<Personne> AllEnseignant()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Personne> AllEnseignantOrdered()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Personne> AllEnseignantResponsable(bool actif, int idResp, int idEns)
        {
            throw new NotImplementedException();
        }

        public bool AnyEnseignantWhere(Expression<Func<Personne, bool>> condition, Personne personne)
        {
            var enseignant =
                _listeEnseignant.Find(x => x.NomUsager == personne.NomUsager && x.id_Pers != personne.id_Pers);
            return enseignant != null;
        }

        public bool AnyjumelageWhere(Expression<Func<Jumelage, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public Personne FindEnseignant(int id)
        {
            return _listeEnseignant.Find(x => x.id_Pers == id);
        }

        public void RemoveEnseignant(int id)
        {
            throw new NotImplementedException();
        }

        public string FindMdp(int id)
        {
            throw new NotImplementedException();
        }

        public void DeclareModifiedEns(Personne enseignant)
        {
            var index = _listeEnseignant.FindIndex(a => a.id_Pers == enseignant.id_Pers);
            _listeEnseignant[index] = enseignant;
        }

        public SelectList ListeTypeUsager(int idResp, int idEns)
        {
            throw new NotImplementedException();
        }

        public SelectList ListeSexe(int? sexe = 0)
        {
            throw new NotImplementedException();
        }

        public SelectList ListeSession(int session = 0)
        {
            throw new NotImplementedException();
        }

        public SelectList ListePersonne(int idSession, int idPers)
        {
            throw new NotImplementedException();
        }

        public SelectList ListeCours(int cours = 0)
        {
            throw new NotImplementedException();
        }

        public SelectList ListeCollege(int college = 0)
        {
            throw new NotImplementedException();
        }

        public SelectList ListeStatutCours(int statut = 0)
        {
            throw new NotImplementedException();
        }

        public SelectList ListeEnseignant(int enseignant = 0)
        {
            throw new NotImplementedException();
        }

        public SelectList ListeSuperviseur(int superviseur = 0)
        {
            throw new NotImplementedException();
        }

        public SelectList ListeTypeInscription(int typeInscription = 0)
        {
            throw new NotImplementedException();
        }

        public SelectList ListeInscription(int inscription = 0)
        {
            throw new NotImplementedException();
        }

        public SelectList ListeStatutInscriptionSansBrouillon(int statut = 0)
        {
            throw new NotImplementedException();
        }

        public List<string> ListeJours()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cours> ListeCoursSelonSession(int session)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Groupe> ListeGroupeSelonSessionEtCours(int cours, int session)
        {
            throw new NotImplementedException();
        }

        public SelectList ListeStatutCours()
        {
            throw new NotImplementedException();
        }

        public SelectList ListeTypesCourriels(int typeCourriel = 0)
        {
            throw new NotImplementedException();
        }
    }
}