using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace sachem.Models.DataAccess
{
    public interface IDataRepository
    {
        IEnumerable GetSessions();

        bool AnyCoursWhere(Expression<Func<Cours, bool>> condition);

        bool AnyGroupeWhere(Expression<Func<Groupe, bool>> condition);

        int SessionEnCours();

        IEnumerable<Cours> AllCours();

        void AddCours(Cours cours);

        Cours FindCours(int id);

        void DeclareModified(Cours cours);

        void RemoveCours(Cours cours);

        void Dispose();
    }
}
