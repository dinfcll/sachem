using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace sachem.Models.DataAccess
{public interface IDataRepository
    {

        int SessionEnCours();
        string FindMdp(int id);
        void BeLazy(bool set = true);
        #region Any
        bool AnyChoixReponse(Expression<Func<ChoixReponse, bool>> condition);
        bool AnyCourriel(Expression<Func<Courriel, bool>> condition);
        bool AnyCours(Expression<Func<Cours, bool>> condition);
        bool AnyCoursInteret(Expression<Func<CoursInteret, bool>> condition);
        bool AnyCoursSuivi(Expression<Func<CoursSuivi, bool>> condition);
        bool AnyDisponibilite(Expression<Func<Disponibilite, bool>> condition);
        bool AnyEtuProgEtude(Expression<Func<EtuProgEtude, bool>> condition);
        bool AnyEvaluation(Expression<Func<Evaluation, bool>> condition);
        bool AnyFormulaire(Expression<Func<Formulaire, bool>> condition);
        bool AnyGroupe(Expression<Func<Groupe, bool>> condition);
        bool AnyGroupeEtudiant(Expression<Func<GroupeEtudiant, bool>> condition);
        bool AnyInscription(Expression<Func<Inscription, bool>> condition);
        bool AnyJumelage(Expression<Func<Jumelage, bool>> condition);
        bool AnyPersonne(Expression<Func<Personne, bool>> condition);
        bool AnyProgrammeEtude(Expression<Func<ProgrammeEtude, bool>> condition);
        bool AnyQuestion(Expression<Func<Question, bool>> condition);
        bool AnyReponseQuestion(Expression<Func<ReponseQuestion, bool>> condition);
        bool AnySection(Expression<Func<Section, bool>> condition);
        bool AnySession(Expression<Func<Session, bool>> condition);
        bool AnySuivi(Expression<Func<Suivi, bool>> condition);
            //Any - sur table parametres
        bool AnyCollege(Expression<Func<p_College, bool>> condition);
        bool AnyContact(Expression<Func<p_Contact, bool>> condition);
        bool AnyHoraireInscription(Expression<Func<p_HoraireInscription, bool>> condition);
        bool AnyJour(Expression<Func<p_Jour, bool>> condition);
        bool AnySaison(Expression<Func<p_Saison, bool>> condition);
        bool AnySexe(Expression<Func<p_Sexe, bool>> condition);
        bool AnyStatutCours(Expression<Func<p_StatutCours, bool>> condition);
        bool AnyStatutInscription(Expression<Func<p_StatutInscription, bool>> condition);
        bool AnyTypeCourriel(Expression<Func<p_TypeCourriel, bool>> condition);
        bool AnyTypeFormulaire(Expression<Func<p_TypeFormulaire, bool>> condition);
        bool AnyTypeInscription(Expression<Func<p_TypeInscription, bool>> condition);
        bool AnyTypeResultat(Expression<Func<p_TypeResultat, bool>> condition);
        bool AnyTypeUsag(Expression<Func<p_TypeUsag, bool>> condition);
        #endregion

        #region Where
        IEnumerable<ChoixReponse> WhereChoixReponse(Expression<Func<ChoixReponse, bool>> condition, bool asNoTracking = false);
        IEnumerable<Courriel> WhereCourriel(Expression<Func<Courriel, bool>> condition, bool asNoTracking = false);
        IEnumerable<Cours> WhereCours(Expression<Func<Cours, bool>> condition, bool asNoTracking = false);
        IEnumerable<CoursInteret> WhereCoursInteret(Expression<Func<CoursInteret, bool>> condition, bool asNoTracking = false);
        IEnumerable<CoursSuivi> WhereCoursSuivi(Expression<Func<CoursSuivi, bool>> condition, bool asNoTracking = false);
        IEnumerable<Disponibilite> WhereDisponibilite(Expression<Func<Disponibilite, bool>> condition, bool asNoTracking = false);
        IEnumerable<EtuProgEtude> WhereEtuProgEtude(Expression<Func<EtuProgEtude, bool>> condition, bool asNoTracking = false);
        IEnumerable<Evaluation> WhereEvaluation(Expression<Func<Evaluation, bool>> condition, bool asNoTracking = false);
        IEnumerable<Formulaire> WhereFormulaire(Expression<Func<Formulaire, bool>> condition, bool asNoTracking = false);
        IEnumerable<Groupe> WhereGroupe(Expression<Func<Groupe, bool>> condition, bool asNoTracking = false);
        IEnumerable<GroupeEtudiant> WhereGroupeEtudiant(Expression<Func<GroupeEtudiant, bool>> condition, bool asNoTracking = false);
        IEnumerable<Inscription> WhereInscription(Expression<Func<Inscription, bool>> condition, bool asNoTracking = false);
        IEnumerable<Jumelage> WhereJumelage(Expression<Func<Jumelage, bool>> condition, bool asNoTracking = false);
        IEnumerable<Personne> WherePersonne(Expression<Func<Personne, bool>> condition, bool asNoTracking = false);
        IEnumerable<ProgrammeEtude> WhereProgrammeEtude(Expression<Func<ProgrammeEtude, bool>> condition, bool asNoTracking = false);
        IEnumerable<Question> WhereQuestion(Expression<Func<Question, bool>> condition, bool asNoTracking = false);
        IEnumerable<ReponseQuestion> WhereReponseQuestion(Expression<Func<ReponseQuestion, bool>> condition, bool asNoTracking = false);
        IEnumerable<Section> WhereSection(Expression<Func<Section, bool>> condition, bool asNoTracking = false);
        IEnumerable<Session> WhereSession(Expression<Func<Session, bool>> condition, bool asNoTracking = false);
        IEnumerable<Suivi> WhereSuivi(Expression<Func<Suivi, bool>> condition, bool asNoTracking = false);
            //Where - sur table parametres
        IEnumerable<p_College> WhereCollege(Expression<Func<p_College, bool>> condition, bool asNoTracking = false);
        IEnumerable<p_Contact> WhereContact(Expression<Func<p_Contact, bool>> condition, bool asNoTracking = false);
        IEnumerable<p_HoraireInscription> WhereHoraireInscription(Expression<Func<p_HoraireInscription, bool>> condition, bool asNoTracking = false);
        IEnumerable<p_Jour> WhereJour(Expression<Func<p_Jour, bool>> condition, bool asNoTracking = false);
        IEnumerable<p_Saison> WhereSaison(Expression<Func<p_Saison, bool>> condition, bool asNoTracking = false);
        IEnumerable<p_Sexe> WhereSexe(Expression<Func<p_Sexe, bool>> condition, bool asNoTracking = false);
        IEnumerable<p_StatutCours> WhereStatutCours(Expression<Func<p_StatutCours, bool>> condition, bool asNoTracking = false);
        IEnumerable<p_StatutInscription> WhereStatutInscription(Expression<Func<p_StatutInscription, bool>> condition, bool asNoTracking = false);
        IEnumerable<p_TypeCourriel> WhereTypeCourriel(Expression<Func<p_TypeCourriel, bool>> condition, bool asNoTracking = false);
        IEnumerable<p_TypeFormulaire> WhereTypeFormulaire(Expression<Func<p_TypeFormulaire, bool>> condition, bool asNoTracking = false);
        IEnumerable<p_TypeInscription> WhereTypeInscription(Expression<Func<p_TypeInscription, bool>> condition, bool asNoTracking = false);
        IEnumerable<p_TypeResultat> WhereTypeResultat(Expression<Func<p_TypeResultat, bool>> condition, bool asNoTracking = false);
        IEnumerable<p_TypeUsag> WhereTypeUsag(Expression<Func<p_TypeUsag, bool>> condition, bool asNoTracking = false);
        #endregion

        #region All
        IEnumerable<ChoixReponse> AllChoixReponse();
        IEnumerable<Courriel> AllCourriel();
        IEnumerable<Cours> AllCours();
        IEnumerable<CoursInteret> AllCoursInteret();
        IEnumerable<CoursSuivi> AllCoursSuivi();
        IEnumerable<Disponibilite> AllDisponibilite();
        IEnumerable<EtuProgEtude> AllEtuProgEtude();
        IEnumerable<Evaluation> AllEvaluation();
        IEnumerable<Formulaire> AllFormulaire();
        IEnumerable<Groupe> AllGroupe();
        IEnumerable<GroupeEtudiant> AllGroupeEtudiant();
        IEnumerable<Inscription> AllInscription();
        IEnumerable<Jumelage> AllJumelage();
        IEnumerable<Personne> AllPersonne();
        IEnumerable<ProgrammeEtude> AllProgrammeEtude();
        IEnumerable<Question> AllQuestion();
        IEnumerable<ReponseQuestion> AllReponseQuestion();
        IEnumerable<Section> AllSection();
        IEnumerable<Session> AllSession();
        IEnumerable<Suivi> AllSuivi();
            //All - sur table parametres
        IEnumerable<p_College> AllCollege();
        IEnumerable<p_Contact> AllContact();
        IEnumerable<p_HoraireInscription> AllHoraireInscription();
        IEnumerable<p_Jour> AllJour();
        IEnumerable<p_Saison> AllSaison();
        IEnumerable<p_Sexe> AllSexe();
        IEnumerable<p_StatutCours> AllStatutCours();
        IEnumerable<p_StatutInscription> AllStatutInscription();
        IEnumerable<p_TypeCourriel> AllTypeCourriel();
        IEnumerable<p_TypeFormulaire> AllTypeFormulaire();
        IEnumerable<p_TypeInscription> AllTypeInscription();
        IEnumerable<p_TypeResultat> AllTypeResultat();
        IEnumerable<p_TypeUsag> AllTypeUsag();
        #endregion

        #region Find
        ChoixReponse FindChoixReponse(int id);
        Courriel FindCourriel(int id);
        Cours FindCours(int id);
        CoursInteret FindCoursInteret(int id);
        CoursSuivi FindCoursSuivi(int id);
        Disponibilite FindDisponibilite(int id);
        EtuProgEtude FindEtuProgEtude(int id);
        Evaluation FindEvaluation(int id);
        Formulaire FindFormulaire(int id);
        Groupe FindGroupe(int id);
        GroupeEtudiant FindGroupeEtudiant(int id);
        Inscription FindInscription(int id);
        Jumelage FindJumelage(int id);
        Personne FindPersonne(int id);
        ProgrammeEtude FindProgrammeEtude(int id);
        Question FindQuestion(int id);
        ReponseQuestion FindReponseQuestion(int id);
        Section FindSection(int id);
        Session FindSession(int id);
        Suivi FindSuivi(int id);
        #endregion

        #region Add
        void AddChoixReponse(ChoixReponse itemToAdd, bool saveChanges = true);
        void AddCourriel(Courriel itemToAdd, bool saveChanges = true);
        void AddCours(Cours itemToAdd, bool saveChanges = true);
        void AddCoursInteret(CoursInteret itemToAdd, bool saveChanges = true);
        void AddCoursSuivi(CoursSuivi itemToAdd, bool saveChanges = true);
        void AddDisponibilite(Disponibilite itemToAdd, bool saveChanges = true);
        void AddEtuProgEtude(EtuProgEtude itemToAdd, bool saveChanges = true);
        void AddEvaluation(Evaluation itemToAdd, bool saveChanges = true);
        void AddFormulaire(Formulaire itemToAdd, bool saveChanges = true);
        void AddGroupe(Groupe itemToAdd, bool saveChanges = true);
        void AddGroupeEtudiant(GroupeEtudiant itemToAdd, bool saveChanges = true);
        void AddInscription(Inscription itemToAdd, bool saveChanges = true);
        void AddJumelage(Jumelage itemToAdd, bool saveChanges = true);
        void AddPersonne(Personne itemToAdd, bool saveChanges = true);
        void AddProgrammeEtude(ProgrammeEtude itemToAdd, bool saveChanges = true);
        void AddQuestion(Question itemToAdd, bool saveChanges = true);
        void AddReponseQuestion(ReponseQuestion itemToAdd, bool saveChanges = true);
        void AddSection(Section itemToAdd, bool saveChanges = true);
        void AddSession(Session itemToAdd, bool saveChanges = true);
        void AddSuivi(Suivi itemToAdd, bool saveChanges = true);
        #endregion

        #region AddRange
        void AddRangeChoixReponse(IEnumerable<ChoixReponse> itemsToAdd, bool saveChanges = true);
        void AddRangeCourriel(IEnumerable<Courriel> itemsToAdd, bool saveChanges = true);
        void AddRangeCours(IEnumerable<Cours> itemsToAdd, bool saveChanges = true);
        void AddRangeCoursInteret(IEnumerable<CoursInteret> itemsToAdd, bool saveChanges = true);
        void AddRangeCoursSuivi(IEnumerable<CoursSuivi> itemsToAdd, bool saveChanges = true);
        void AddRangeDisponibilite(IEnumerable<Disponibilite> itemsToAdd, bool saveChanges = true);
        void AddRangeEtuProgEtude(IEnumerable<EtuProgEtude> itemsToAdd, bool saveChanges = true);
        void AddRangeEvaluation(IEnumerable<Evaluation> itemsToAdd, bool saveChanges = true);
        void AddRangeFormulaire(IEnumerable<Formulaire> itemsToAdd, bool saveChanges = true);
        void AddRangeGroupe(IEnumerable<Groupe> itemsToAdd, bool saveChanges = true);
        void AddRangeGroupeEtudiant(IEnumerable<GroupeEtudiant> itemsToAdd, bool saveChanges = true);
        void AddRangeInscription(IEnumerable<Inscription> itemsToAdd, bool saveChanges = true);
        void AddRangeJumelage(IEnumerable<Jumelage> itemsToAdd, bool saveChanges = true);
        void AddRangePersonne(IEnumerable<Personne> itemsToAdd, bool saveChanges = true);
        void AddRangeProgrammeEtude(IEnumerable<ProgrammeEtude> itemsToAdd, bool saveChanges = true);
        void AddRangeQuestion(IEnumerable<Question> itemsToAdd, bool saveChanges = true);
        void AddRangeReponseQuestion(IEnumerable<ReponseQuestion> itemsToAdd, bool saveChanges = true);
        void AddRangeSection(IEnumerable<Section> itemsToAdd, bool saveChanges = true);
        void AddRangeSession(IEnumerable<Session> itemsToAdd, bool saveChanges = true);
        void AddRangeSuivi(IEnumerable<Suivi> itemsToAdd, bool saveChanges = true);
        #endregion

        #region Edit
        void EditChoixReponse(ChoixReponse itemToEdit, bool saveChanges = true);
        void EditCourriel(Courriel itemToEdit, bool saveChanges = true);
        void EditCours(Cours itemToEdit, bool saveChanges = true);
        void EditCoursInteret(CoursInteret itemToEdit, bool saveChanges = true);
        void EditCoursSuivi(CoursSuivi itemToEdit, bool saveChanges = true);
        void EditDisponibilite(Disponibilite itemToEdit, bool saveChanges = true);
        void EditEtuProgEtude(EtuProgEtude itemToEdit, bool saveChanges = true);
        void EditEvaluation(Evaluation itemToEdit, bool saveChanges = true);
        void EditFormulaire(Formulaire itemToEdit, bool saveChanges = true);
        void EditGroupe(Groupe itemToEdit, bool saveChanges = true);
        void EditGroupeEtudiant(GroupeEtudiant itemToEdit, bool saveChanges = true);
        void EditInscription(Inscription itemToEdit, bool saveChanges = true);
        void EditJumelage(Jumelage itemToEdit, bool saveChanges = true);
        void EditPersonne(Personne itemToEdit, bool saveChanges = true);
        void EditProgrammeEtude(ProgrammeEtude itemToEdit, bool saveChanges = true);
        void EditQuestion(Question itemToEdit, bool saveChanges = true);
        void EditReponseQuestion(ReponseQuestion itemToEdit, bool saveChanges = true);
        void EditSection(Section itemToEdit, bool saveChanges = true);
        void EditSession(Session itemToEdit, bool saveChanges = true);
        void EditSuivi(Suivi itemToEdit, bool saveChanges = true);
        #endregion

        #region Remove
        void RemoveChoixReponse(ChoixReponse itemToRemove, bool saveChanges = true);
        void RemoveCourriel(Courriel itemToRemove, bool saveChanges = true);
        void RemoveCours(Cours itemToRemove, bool saveChanges = true);
        void RemoveCoursInteret(CoursInteret itemToRemove, bool saveChanges = true);
        void RemoveCoursSuivi(CoursSuivi itemToRemove, bool saveChanges = true);
        void RemoveDisponibilite(Disponibilite itemToRemove, bool saveChanges = true);
        void RemoveEtuProgEtude(EtuProgEtude itemToRemove, bool saveChanges = true);
        void RemoveEvaluation(Evaluation itemToRemove, bool saveChanges = true);
        void RemoveFormulaire(Formulaire itemToRemove, bool saveChanges = true);
        void RemoveGroupe(Groupe itemToRemove, bool saveChanges = true);
        void RemoveGroupeEtudiant(GroupeEtudiant itemToRemove, bool saveChanges = true);
        void RemoveInscription(Inscription itemToRemove, bool saveChanges = true);
        void RemoveJumelage(Jumelage itemToRemove, bool saveChanges = true);
        void RemovePersonne(Personne itemToRemove, bool saveChanges = true);
        void RemoveProgrammeEtude(ProgrammeEtude itemToRemove, bool saveChanges = true);
        void RemoveQuestion(Question itemToRemove, bool saveChanges = true);
        void RemoveReponseQuestion(ReponseQuestion itemToRemove, bool saveChanges = true);
        void RemoveSection(Section itemToRemove, bool saveChanges = true);
        void RemoveSession(Session itemToRemove, bool saveChanges = true);
        void RemoveSuivi(Suivi itemToRemove, bool saveChanges = true);
        #endregion

        #region RemoveRange
        void RemoveRangeChoixReponse(IEnumerable<ChoixReponse> itemsToRemove, bool saveChanges = true);
        void RemoveRangeCourriel(IEnumerable<Courriel> itemsToRemove, bool saveChanges = true);
        void RemoveRangeCours(IEnumerable<Cours> itemsToRemove, bool saveChanges = true);
        void RemoveRangeCoursInteret(IEnumerable<CoursInteret> itemsToRemove, bool saveChanges = true);
        void RemoveRangeCoursSuivi(IEnumerable<CoursSuivi> itemsToRemove, bool saveChanges = true);
        void RemoveRangeDisponibilite(IEnumerable<Disponibilite> itemsToRemove, bool saveChanges = true);
        void RemoveRangeEtuProgEtude(IEnumerable<EtuProgEtude> itemsToRemove, bool saveChanges = true);
        void RemoveRangeEvaluation(IEnumerable<Evaluation> itemsToRemove, bool saveChanges = true);
        void RemoveRangeFormulaire(IEnumerable<Formulaire> itemsToRemove, bool saveChanges = true);
        void RemoveRangeGroupe(IEnumerable<Groupe> itemsToRemove, bool saveChanges = true);
        void RemoveRangeGroupeEtudiant(IEnumerable<GroupeEtudiant> itemsToRemove, bool saveChanges = true);
        void RemoveRangeInscription(IEnumerable<Inscription> itemsToRemove, bool saveChanges = true);
        void RemoveRangeJumelage(IEnumerable<Jumelage> itemsToRemove, bool saveChanges = true);
        void RemoveRangePersonne(IEnumerable<Personne> itemsToRemove, bool saveChanges = true);
        void RemoveRangeProgrammeEtude(IEnumerable<ProgrammeEtude> itemsToRemove, bool saveChanges = true);
        void RemoveRangeQuestion(IEnumerable<Question> itemsToRemove, bool saveChanges = true);
        void RemoveRangeReponseQuestion(IEnumerable<ReponseQuestion> itemsToRemove, bool saveChanges = true);
        void RemoveRangeSection(IEnumerable<Section> itemsToRemove, bool saveChanges = true);
        void RemoveRangeSession(IEnumerable<Session> itemsToRemove, bool saveChanges = true);
        void RemoveRangeSuivi(IEnumerable<Suivi> itemsToRemove, bool saveChanges = true);
        #endregion

        #region Liste
        SelectList ListeTypeUsager(int idTypeUsager = 0);
        SelectList ListeTypeUsagerDuPersonnel(int idTypeUsager = 0);
        SelectList ListeSexe(int? sexe = 0);
        SelectList ListeSession(int session = 0);
        SelectList ListeCours(int cours = 0);
        SelectList ListeCollege(int college = 0);
        SelectList ListeStatutCours(int statut = 0);
        SelectList ListeProgrammmeEtude(bool actif = true);
        SelectList ListeEtudiants(int id = 0);
        SelectList ListeEnseignant(int id = 0);
        SelectList ListeEnseignantEtResponsable(int id = 0);
        SelectList ListeTypeInscription(int typeInscription = 0);
        SelectList ListeInscription(int inscription = 0);
        SelectList ListeStatutInscriptionSansBrouillon(int statut = 0);
        SelectList ListeStatutCours();
        SelectList ListeTypesCourriels(int typeCourriel = 0);
        List<string> ListeJours();
        #endregion

        void Dispose();
    }
}
