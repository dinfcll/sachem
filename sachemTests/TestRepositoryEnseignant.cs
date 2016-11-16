using System;
using System.Collections.Generic;

using System.Collections;
using System.Linq.Expressions;

using sachem.Models;
using sachem.Models.DataAccess;
using System.Web.Mvc;

namespace sachemTests
{
    internal class TestRepositoryEnseignant : IDataRepositoryEnseignant
    {
        private readonly List<Personne> listeEnseignant = new List<Personne>();

        public bool AnyGroupeWhere(Expression<Func<Groupe, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyjumelageWhere(Expression<Func<Jumelage, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public bool AnyEnseignantWhere(Expression<Func<Personne, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Personne> AllEnseignant()
        {
            throw new NotImplementedException();
        }

        public void AddEnseignant(Personne enseignant)
        {
            listeEnseignant.Add(enseignant);
        }

        public Personne FindEnseignant(int id)
        {
            return listeEnseignant.Find(x => x.id_Pers == id);
        }

        public void DeclareModified(Personne cours)
        {
            throw new NotImplementedException();
        }

        public void RemoveEnseignant(Personne cours)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public SelectList liste_sexe()
        {
            throw new NotImplementedException();
        }

        public SelectList liste_sexe(Personne personne)
        {
            throw new NotImplementedException();
        }

        public SelectList liste_usag(int id_resp, int id_ens)
        {
            throw new NotImplementedException();
        }

        public SelectList liste_usag(Personne personne, int id_resp, int id_ens)
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
    }
}
