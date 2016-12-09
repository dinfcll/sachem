using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq.Expressions;
using sachem.Models;
using sachem.Models.DataAccess;
using System.Linq;
using System.Web.Mvc;

namespace sachemTests
{
    internal class TestRepository : IDataRepository
    {
        private readonly List<Cours> listeCours = new List<Cours>();
        private readonly List<Personne> listePersonne = new List<Personne>();
        private readonly List<Inscription> listeInscription = new List<Inscription>();
        private readonly List<Personne> listeSuperviseur = new List<Personne>();
        private readonly List<CoursSuivi> listeCoursSuivi = new List<CoursSuivi>();

        public IEnumerable GetSessions()
        {
            throw new NotImplementedException();
        }

        public bool AnyCoursWhere(Expression<Func<Cours, bool>> condition)
        {
            IQueryable<Cours> NouvelleListe = listeCours.AsQueryable<Cours>();
            return NouvelleListe.Any<Cours>(condition);
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
            listeCours.Add(cours);
        }

        public Cours FindCours(int id)
        {
            return listeCours.Find(x => x.id_Cours == id);
        }

        public void AddInscription(Inscription inscription)
        {
            listeInscription.Add(inscription);
        }

        public Inscription FindInscription(int id)
        {
            return listeInscription.Find(x => x.id_Inscription == id);
        }

        public void DeclareModified(Cours cours)
        {
            throw new NotImplementedException();
        }

        public void RemoveCours(Cours cours)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Personne> IndexPersonne()
        {
            throw new NotImplementedException();
        }
        public Personne FindPersonne(int id)
        {
            return listePersonne.Find(x => x.id_Pers == id);
        }
   
        public IEnumerable<p_Sexe> AllSexe()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<p_TypeUsag> AllTypeUsag()
        {
            throw new NotImplementedException();
        }

        public void DeclareModifiedPers(Personne pers)
        {
            throw new NotImplementedException();
        }

        public void AddPersonne(Personne pers)
        {
            listePersonne.Add(pers);
        }

        public void RemovePersonne(Personne pers)
        {
            throw new NotImplementedException();
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void AddCoursSuivi(CoursSuivi coursSuivi)
        {
            listeCoursSuivi.Add(coursSuivi);
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
            private readonly List<Personne> listeEnseignant = new List<Personne>();

            public void AddEnseignant(Personne enseignant)
            {
                listeEnseignant.Add(enseignant);
            }

            public IEnumerable<Personne> AllEnseignant()
            {
                throw new NotImplementedException();
            }

            public IEnumerable<Personne> AllEnseignantOrdered()
            {
                throw new NotImplementedException();
            }

            public IEnumerable<Personne> AllEnseignantResponsable(bool Actif, int id_resp, int id_ens)
            {
                throw new NotImplementedException();
            }

            public bool AnyEnseignantWhere(Expression<Func<Personne, bool>> condition, Personne personne)
            {
                var Enseignant = listeEnseignant.Find(x => x.NomUsager == personne.NomUsager && x.id_Pers != personne.id_Pers);
                if (Enseignant == null)
                    return false;
                else
                    return true;
            }

            public bool AnyjumelageWhere(Expression<Func<Jumelage, bool>> condition)
            {
                throw new NotImplementedException();
            }

            public void DeclareModified(Personne enseignant)
            {
                var index = listeEnseignant.FindIndex(a => a.id_Pers == enseignant.id_Pers);
                listeEnseignant[index] = enseignant;
            }

            public Personne FindEnseignant(int id)
            {
                return listeEnseignant.Find(x => x.id_Pers == id);
            }

            public SelectList liste_sexe()
            {
                throw new NotImplementedException();
            }

            public SelectList liste_sexe(Personne personne)
            {
                return new SelectList("femme");
            }

            public SelectList liste_usag(int id_resp, int id_ens)
            {
                throw new NotImplementedException();
            }

            public SelectList liste_usag(Personne personne, int id_resp, int id_ens)
            {
                return new SelectList("Enseignant");
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
            var index = listeEnseignant.FindIndex(a => a.id_Pers == enseignant.id_Pers);
            listeEnseignant[index] = enseignant;
        }
    }
}
