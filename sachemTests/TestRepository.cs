﻿using System;
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

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}