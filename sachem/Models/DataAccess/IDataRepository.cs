using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace sachem.Models.DataAccess
{
    public interface IDataRepository
    {
        IEnumerable GetSessions();

        IEnumerable GetCours();

        IEnumerable GetCollege();

        IEnumerable GetStatut();

        System.Linq.IQueryable<int> GetSpecificInscription(int id);

        bool AnyCoursWhere(Expression<Func<Cours, bool>> condition);

        bool AnyGroupeWhere(Expression<Func<Groupe, bool>> condition);

        bool AnyCoursSuiviWhere(Expression<Func<CoursSuivi, bool>> condition);

        int SessionEnCours();

        IEnumerable<Cours> AllCours();

        void AddCours(Cours cours);

        void AddCoursSuivi(CoursSuivi coursSuivi);

        void AddInscription(Inscription inscription);

        Cours FindCours(int id);

        CoursSuivi FindCoursSuivi(int id);

        Personne FindPersonne(int id);

        Inscription FindInscription(int id);

        void DeclareModified(Cours cours);

        void ModifyCoursSuivi(CoursSuivi coursSuivi);

        void RemoveCours(Cours cours);

        IQueryable<Personne> IndexPersonne();

        IEnumerable<p_Sexe> AllSexe();

        IEnumerable<p_TypeUsag> AllTypeUsag();

        void DeclareModifiedPers(Personne pers);

        void AddPersonne(Personne pers);

        void RemovePersonne(Personne pers);
        void RemoveCoursSuivi(CoursSuivi coursSuivi);

        bool AnyEnseignantWhere(Expression<Func<Personne, bool>> condition, Personne personne);

        bool AnyjumelageWhere(Expression<Func<Jumelage, bool>> condition);

        IEnumerable<Personne> AllEnseignant();

        SelectList liste_sexe();

        SelectList liste_sexe(Personne personne);

        SelectList liste_usag(int id_resp, int id_ens);

        SelectList liste_usag(Personne personne, int id_resp, int id_ens);

        IEnumerable<Personne> AllEnseignantOrdered();

        IEnumerable<Personne> AllEnseignantResponsable(bool Actif, int id_resp, int id_ens);

        void AddEnseignant(Personne enseignant);

        Personne FindEnseignant(int id);

        void DeclareModified(Personne enseignant);

        void RemoveEnseignant(int id);

        void Dispose();       

    }
}
