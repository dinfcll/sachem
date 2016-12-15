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

        string FindMdp(int id);

        void RemoveCoursSuivi(CoursSuivi coursSuivi);

        bool AnyEnseignantWhere(Expression<Func<Personne, bool>> condition, Personne personne);

        bool AnyjumelageWhere(Expression<Func<Jumelage, bool>> condition);

        IEnumerable<Personne> AllEnseignant();

        IEnumerable<Personne> AllEnseignantOrdered();

        IEnumerable<Personne> AllEnseignantResponsable(bool Actif, int id_resp, int id_ens);

        void AddEnseignant(Personne enseignant);

        Personne FindEnseignant(int id);

        void DeclareModifiedEns(Personne enseignant);

        void RemoveEnseignant(int id);

        SelectList ListeTypeUsager(int idResp, int idEns);

        SelectList ListeSexe(int sexe = 0);

        SelectList ListeSession(int session = 0);

        SelectList ListePersonne(int idSession, int idPers);

        SelectList ListeCours(int cours = 0);

        SelectList ListeCollege(int college = 0);

        SelectList ListeStatutCours(int statut = 0);

        SelectList ListeEnseignant(int enseignant = 0);

        SelectList ListeSuperviseur(int superviseur = 0);

        SelectList ListeTypeInscription(int typeInscription = 0);

        SelectList ListeInscription(int inscription = 0);

        SelectList ListeStatutInscriptionSansBrouillon(int statut = 0);

        List<string> ListeJours();

        IEnumerable<Cours> ListeCoursSelonSession(int session);

        IEnumerable<Groupe> ListeGroupeSelonSessionEtCours(int cours, int session);

        SelectList ListeStatutCours();


        SelectList ListeTypesCourriels(int typeCourriel = 0);

        void Dispose();       

    }
}
