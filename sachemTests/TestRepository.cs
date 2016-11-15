using System;
using System.Collections.Generic;

using System.Collections;
using System.Linq.Expressions;

using sachem.Models;
using sachem.Models.DataAccess;

namespace sachemTests
{
    internal class TestRepository : IDataRepository
    {
        private readonly List<Cours> listeCours = new List<Cours>();
        private readonly List<Inscription> listeInscription = new List<Inscription>();
        private readonly List<Personne> listeSuperviseur = new List<Personne>();

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

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
