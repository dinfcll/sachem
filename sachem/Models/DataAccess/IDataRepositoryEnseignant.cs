using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace sachem.Models.DataAccess
{
    public interface IDataRepositoryEnseignant
    {
        bool AnyEnseignantWhere(Expression<Func<Personne, bool>> condition, Personne personne);

        bool AnyGroupeWhere(Expression<Func<Groupe, bool>> condition);

        bool AnyjumelageWhere(Expression<Func<Jumelage, bool>> condition);

        IEnumerable<Personne> AllEnseignant();

        SelectList liste_sexe();

        SelectList liste_sexe(Personne personne);

        SelectList liste_usag(int id_resp, int id_ens);

        SelectList liste_usag(Personne personne, int id_resp, int id_ens);

        IEnumerable<Personne> AllEnseignantOrdered();

        IEnumerable<Personne> AllEnseignantResponsable(bool Actif,int id_resp, int id_ens);

        void AddEnseignant(Personne enseignant);

       Personne FindEnseignant(int id);

        void DeclareModified(Personne enseignant);

        void RemoveEnseignant(int id);

        void Dispose();
    }
}
