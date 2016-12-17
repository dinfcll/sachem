using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace sachem.Models.DataAccess
{public interface IDataRepository
    {
        int SessionEnCours();
        string FindMdp(int id);

        //Debut Any
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
        //Fin Any

        //Debut Where
        IEnumerable<ChoixReponse> GetChoixReponse(Expression<Func<ChoixReponse, bool>> condition);
        IEnumerable<Courriel> GetCourriel(Expression<Func<Courriel, bool>> condition);
        IEnumerable<Cours> GetCours(Expression<Func<Cours, bool>> condition);
        IEnumerable<CoursInteret> GetCoursInteret(Expression<Func<CoursInteret, bool>> condition);
        IEnumerable<CoursSuivi> GetCoursSuivi(Expression<Func<CoursSuivi, bool>> condition);
        IEnumerable<Disponibilite> GetDisponibilite(Expression<Func<Disponibilite, bool>> condition);
        IEnumerable<EtuProgEtude> GetEtuProgEtude(Expression<Func<EtuProgEtude, bool>> condition);
        IEnumerable<Evaluation> GetEvaluation(Expression<Func<Evaluation, bool>> condition);
        IEnumerable<Formulaire> GetFormulaire(Expression<Func<Formulaire, bool>> condition);
        IEnumerable<Groupe> GetGroupe(Expression<Func<Groupe, bool>> condition);
        IEnumerable<GroupeEtudiant> GetGroupeEtudiant(Expression<Func<GroupeEtudiant, bool>> condition);
        IEnumerable<Inscription> GetInscription(Expression<Func<Inscription, bool>> condition);
        IEnumerable<Jumelage> GetJumelage(Expression<Func<Jumelage, bool>> condition);
        IEnumerable<Personne> GetPersonne(Expression<Func<Personne, bool>> condition);
        IEnumerable<ProgrammeEtude> GetProgrammeEtude(Expression<Func<ProgrammeEtude, bool>> condition);
        IEnumerable<Question> GetQuestion(Expression<Func<Question, bool>> condition);
        IEnumerable<ReponseQuestion> GetReponseQuestion(Expression<Func<ReponseQuestion, bool>> condition);
        IEnumerable<Section> GetSection(Expression<Func<Section, bool>> condition);
        IEnumerable<Session> GetSession(Expression<Func<Session, bool>> condition);
        IEnumerable<Suivi> GetSuivi(Expression<Func<Suivi, bool>> condition);
            //Get - sur table parametres
        IEnumerable<p_College> GetCollege(Expression<Func<p_College, bool>> condition);
        IEnumerable<p_Contact> GetContact(Expression<Func<p_Contact, bool>> condition);
        IEnumerable<p_HoraireInscription> GetHoraireInscription(Expression<Func<p_HoraireInscription, bool>> condition);
        IEnumerable<p_Jour> GetJour(Expression<Func<p_Jour, bool>> condition);
        IEnumerable<p_Saison> GetSaison(Expression<Func<p_Saison, bool>> condition);
        IEnumerable<p_Sexe> GetSexe(Expression<Func<p_Sexe, bool>> condition);
        IEnumerable<p_StatutCours> GetStatutCours(Expression<Func<p_StatutCours, bool>> condition);
        IEnumerable<p_StatutInscription> GetStatutInscription(Expression<Func<p_StatutInscription, bool>> condition);
        IEnumerable<p_TypeCourriel> GetTypeCourriel(Expression<Func<p_TypeCourriel, bool>> condition);
        IEnumerable<p_TypeFormulaire> GetTypeFormulaire(Expression<Func<p_TypeFormulaire, bool>> condition);
        IEnumerable<p_TypeInscription> GetTypeInscription(Expression<Func<p_TypeInscription, bool>> condition);
        IEnumerable<p_TypeResultat> GetTypeResultat(Expression<Func<p_TypeResultat, bool>> condition);
        IEnumerable<p_TypeUsag> GetTypeUsag(Expression<Func<p_TypeUsag, bool>> condition);
        //Fin Where

        //Debut All
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
        //Fin All

        //Debut Find
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
        //Fin Find

        //Debut Add
        void AddChoixReponse(ChoixReponse itemToAdd);
        void AddCourriel(Courriel itemToAdd);
        void AddCours(Cours itemToAdd);
        void AddCoursInteret(CoursInteret itemToAdd);
        void AddCoursSuivi(CoursSuivi itemToAdd);
        void AddDisponibilite(Disponibilite itemToAdd);
        void AddEtuProgEtude(EtuProgEtude itemToAdd);
        void AddEvaluation(Evaluation itemToAdd);
        void AddFormulaire(Formulaire itemToAdd);
        void AddGroupe(Groupe itemToAdd);
        void AddGroupeEtudiant(GroupeEtudiant itemToAdd);
        void AddInscription(Inscription itemToAdd);
        void AddJumelage(Jumelage itemToAdd);
        void AddPersonne(Personne itemToAdd);
        void AddProgrammeEtude(ProgrammeEtude itemToAdd);
        void AddQuestion(Question itemToAdd);
        void AddReponseQuestion(ReponseQuestion itemToAdd);
        void AddSection(Section itemToAdd);
        void AddSession(Session itemToAdd);
        void AddSuivi(Suivi itemToAdd);
        //Fin Add

        //Debut Edit
        void EditChoixReponse(ChoixReponse itemToEdit);
        void EditCourriel(Courriel itemToEdit);
        void EditCours(Cours itemToEdit);
        void EditCoursInteret(CoursInteret itemToEdit);
        void EditCoursSuivi(CoursSuivi itemToEdit);
        void EditDisponibilite(Disponibilite itemToEdit);
        void EditEtuProgEtude(EtuProgEtude itemToEdit);
        void EditEvaluation(Evaluation itemToEdit);
        void EditFormulaire(Formulaire itemToEdit);
        void EditGroupe(Groupe itemToEdit);
        void EditGroupeEtudiant(GroupeEtudiant itemToEdit);
        void EditInscription(Inscription itemToEdit);
        void EditJumelage(Jumelage itemToEdit);
        void EditPersonne(Personne itemToEdit);
        void EditProgrammeEtude(ProgrammeEtude itemToEdit);
        void EditQuestion(Question itemToEdit);
        void EditReponseQuestion(ReponseQuestion itemToEdit);
        void EditSection(Section itemToEdit);
        void EditSession(Session itemToEdit);
        void EditSuivi(Suivi itemToEdit);
        //Fin Edit

        //Debut Remove
        void RemoveChoixReponse(ChoixReponse itemToRemove);
        void RemoveCourriel(Courriel itemToRemove);
        void RemoveCours(Cours itemToRemove);
        void RemoveCoursInteret(CoursInteret itemToRemove);
        void RemoveCoursSuivi(CoursSuivi itemToRemove);
        void RemoveDisponibilite(Disponibilite itemToRemove);
        void RemoveEtuProgEtude(EtuProgEtude itemToRemove);
        void RemoveEvaluation(Evaluation itemToRemove);
        void RemoveFormulaire(Formulaire itemToRemove);
        void RemoveGroupe(Groupe itemToRemove);
        void RemoveGroupeEtudiant(GroupeEtudiant itemToRemove);
        void RemoveInscription(Inscription itemToRemove);
        void RemoveJumelage(Jumelage itemToRemove);
        void RemovePersonne(Personne itemToRemove);
        void RemoveProgrammeEtude(ProgrammeEtude itemToRemove);
        void RemoveQuestion(Question itemToRemove);
        void RemoveReponseQuestion(ReponseQuestion itemToRemove);
        void RemoveSection(Section itemToRemove);
        void RemoveSession(Session itemToRemove);
        void RemoveSuivi(Suivi itemToRemove);
        //Fin Remove

        //Debut Liste
        SelectList ListeTypeUsager(int idTypeUsager = 0);
        SelectList ListeSexe(int? sexe = 0);
        SelectList ListeSession(int session = 0);
        SelectList ListeCours(int cours = 0);
        SelectList ListeCollege(int college = 0);
        SelectList ListeStatutCours(int statut = 0);
        SelectList ListeProgrammmeCode(bool actif = true);
        SelectList ListeEtudiants(int id = 0);
        SelectList ListeEnseignant(int id = 0);
        SelectList ListeEnseignantEtResponsable(int id = 0);
        SelectList ListeTypeInscription(int typeInscription = 0);
        SelectList ListeInscription(int inscription = 0);
        SelectList ListeStatutInscriptionSansBrouillon(int statut = 0);
        SelectList ListeStatutCours();
        SelectList ListeTypesCourriels(int typeCourriel = 0);
        List<string> ListeJours();
        //Fin Liste

        void Dispose();
    }
}
