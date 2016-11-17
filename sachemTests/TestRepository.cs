using System;
using System.Collections.Generic;

using System.Collections;
using System.Linq.Expressions;

using sachem.Models;
using sachem.Models.DataAccess;
using System.Linq;

namespace sachemTests
{
    internal class TestRepository : IDataRepository
    {
        private readonly List<Cours> listeCours = new List<Cours>();
        private readonly List<Personne> listePersonne = new List<Personne>();
        private readonly List<CoursSuivi> listeCoursSuivi = new List<CoursSuivi>();

        public IEnumerable GetSessions()
        {
            throw new NotImplementedException();
        }

        public bool AnyCoursWhere(Expression<Func<Cours, bool>> condition)
        {
            throw new NotImplementedException();
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

        public void AddCours(Cours cours)
        {
            listeCours.Add(cours);
        }

        public Cours FindCours(int id)
        {
            return listeCours.Find(x => x.id_Cours == id);
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

        public Personne FindPersonne(int id)
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
    }
}
