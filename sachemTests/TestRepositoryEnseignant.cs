using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using sachem.Models;
using sachem.Models.DataAccess;
using System.Web.Mvc;

namespace sachemTests
{
    internal class TestRepositoryEnseignant : IDataRepositoryEnseignant
    {
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

        public bool AnyEnseignantWhere(Expression<Func<Personne, bool>> condition,Personne personne)
        {
            var Enseignant = listeEnseignant.Find(x => x.NomUsager == personne.NomUsager && x.id_Pers != personne.id_Pers);
            if (Enseignant == null)
                return false;
            else
                return true;
        }

        public bool AnyGroupeWhere(Expression<Func<Groupe, bool>> condition)
        {
            throw new NotImplementedException();
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

        public void Dispose()
        {
            throw new NotImplementedException();
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
    }
}
