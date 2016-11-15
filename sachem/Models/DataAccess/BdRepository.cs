﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace sachem.Models.DataAccess
{
    public class BdRepository : IDataRepository
    {
        private readonly SACHEMEntities db = new SACHEMEntities();

        public IEnumerable GetSessions()
        {
            return db.Session.AsNoTracking().OrderBy(s => s.Annee).ThenBy(s => s.p_Saison.Saison);
        }

        public bool AnyCoursWhere(Expression<Func<Cours, bool>> condition)
        {
            return db.Cours.Any(condition);
        }

        public bool AnyGroupeWhere(Expression<Func<Groupe, bool>> condition)
        {
            return db.Groupe.Any(condition);
        }

        public int SessionEnCours()
        {
            return db.Session.Max(s => s.id_Sess);
        }

        public IEnumerable<Cours> AllCours()
        {
            return db.Cours;
        }

        public void AddCours(Cours cours)
        {
            db.Cours.Add(cours);
            db.SaveChanges();
        }

        public Cours FindCours(int id)
        {
            return db.Cours.Find(id);
        }

        public void DeclareModified(Cours cours)
        {
            db.Entry(cours).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void RemoveCours(Cours cours)
        {
            db.Cours.Remove(cours);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public void AddInscription(Inscription inscription)
        {
            throw new NotImplementedException();
        }

        public Inscription FindInscription(int id)
        {
            throw new NotImplementedException();
        }
    }
}
