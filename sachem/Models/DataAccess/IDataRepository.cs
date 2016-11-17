using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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

        Personne FindPersonne(int id);

        IEnumerable<p_Sexe> AllSexe();

        IEnumerable<p_TypeUsag> AllTypeUsag();

        void DeclareModifiedPers(Personne pers);

        void AddPersonne(Personne pers);

        void RemovePersonne(Personne pers);
        void RemoveCoursSuivi(CoursSuivi coursSuivi);

        void Dispose();

        

    }
}
