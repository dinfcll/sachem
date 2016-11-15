using System;
using System.Collections;
using System.Collections.Generic;
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

        Cours FindCours(int id);

        CoursSuivi FindCoursSuivi(int id);

        Personne FindPersonne(int id);

        void DeclareModified(Cours cours);

        void ModifyCoursSuivi(CoursSuivi coursSuivi);

        void RemoveCours(Cours cours);

        void RemoveCoursSuivi(CoursSuivi coursSuivi);

        void Dispose();

        

    }
}
