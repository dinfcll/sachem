using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using sachem.Models;
using sachem.Models.DataAccess;

namespace sachemTests
{
    internal class TestRepository : IDataRepository
    {
        public int SessionEnCours()
        {
            throw new NotImplementedException();
        }
        public string FindMdp(int id)
        {
            throw new NotImplementedException();
        }

        #region Any
        public bool AnyChoixReponse(Expression<Func<ChoixReponse, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyCourriel(Expression<Func<Courriel, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyCours(Expression<Func<Cours, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyCoursInteret(Expression<Func<CoursInteret, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyCoursSuivi(Expression<Func<CoursSuivi, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyDisponibilite(Expression<Func<Disponibilite, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyEtuProgEtude(Expression<Func<EtuProgEtude, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyEvaluation(Expression<Func<Evaluation, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyFormulaire(Expression<Func<Formulaire, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyGroupe(Expression<Func<Groupe, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyGroupeEtudiant(Expression<Func<GroupeEtudiant, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyInscription(Expression<Func<Inscription, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyJumelage(Expression<Func<Jumelage, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyPersonne(Expression<Func<Personne, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyProgrammeEtude(Expression<Func<ProgrammeEtude, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyQuestion(Expression<Func<Question, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyReponseQuestion(Expression<Func<ReponseQuestion, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnySection(Expression<Func<Section, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnySession(Expression<Func<Session, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnySuivi(Expression<Func<Suivi, bool>> condition)
        {
            throw new NotImplementedException();
        }
        //Any - sur table parametres
        public bool AnyCollege(Expression<Func<p_College, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyContact(Expression<Func<p_Contact, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyHoraireInscription(Expression<Func<p_HoraireInscription, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyJour(Expression<Func<p_Jour, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnySaison(Expression<Func<p_Saison, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnySexe(Expression<Func<p_Sexe, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyStatutCours(Expression<Func<p_StatutCours, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyStatutInscription(Expression<Func<p_StatutInscription, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyTypeCourriel(Expression<Func<p_TypeCourriel, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyTypeFormulaire(Expression<Func<p_TypeFormulaire, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyTypeInscription(Expression<Func<p_TypeInscription, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyTypeResultat(Expression<Func<p_TypeResultat, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public bool AnyTypeUsag(Expression<Func<p_TypeUsag, bool>> condition)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Where
        public IEnumerable<ChoixReponse> WhereChoixReponse(Expression<Func<ChoixReponse, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Courriel> WhereCourriel(Expression<Func<Courriel, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Cours> WhereCours(Expression<Func<Cours, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<CoursInteret> WhereCoursInteret(Expression<Func<CoursInteret, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<CoursSuivi> WhereCoursSuivi(Expression<Func<CoursSuivi, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Disponibilite> WhereDisponibilite(Expression<Func<Disponibilite, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<EtuProgEtude> WhereEtuProgEtude(Expression<Func<EtuProgEtude, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Evaluation> WhereEvaluation(Expression<Func<Evaluation, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Formulaire> WhereFormulaire(Expression<Func<Formulaire, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Groupe> WhereGroupe(Expression<Func<Groupe, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<GroupeEtudiant> WhereGroupeEtudiant(Expression<Func<GroupeEtudiant, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Inscription> WhereInscription(Expression<Func<Inscription, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Jumelage> WhereJumelage(Expression<Func<Jumelage, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Personne> WherePersonne(Expression<Func<Personne, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<ProgrammeEtude> WhereProgrammeEtude(Expression<Func<ProgrammeEtude, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Question> WhereQuestion(Expression<Func<Question, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<ReponseQuestion> WhereReponseQuestion(Expression<Func<ReponseQuestion, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Section> WhereSection(Expression<Func<Section, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Session> WhereSession(Expression<Func<Session, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Suivi> WhereSuivi(Expression<Func<Suivi, bool>> condition)
        {
            throw new NotImplementedException();
        }
            //Where - sur table parametres
        public IEnumerable<p_College> WhereCollege(Expression<Func<p_College, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<p_Contact> WhereContact(Expression<Func<p_Contact, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<p_HoraireInscription> WhereHoraireInscription(Expression<Func<p_HoraireInscription, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<p_Jour> WhereJour(Expression<Func<p_Jour, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<p_Saison> WhereSaison(Expression<Func<p_Saison, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<p_Sexe> WhereSexe(Expression<Func<p_Sexe, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<p_StatutCours> WhereStatutCours(Expression<Func<p_StatutCours, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<p_StatutInscription> WhereStatutInscription(Expression<Func<p_StatutInscription, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<p_TypeCourriel> WhereTypeCourriel(Expression<Func<p_TypeCourriel, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<p_TypeFormulaire> WhereTypeFormulaire(Expression<Func<p_TypeFormulaire, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<p_TypeInscription> WhereTypeInscription(Expression<Func<p_TypeInscription, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<p_TypeResultat> WhereTypeResultat(Expression<Func<p_TypeResultat, bool>> condition)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<p_TypeUsag> WhereTypeUsag(Expression<Func<p_TypeUsag, bool>> condition)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region All
        public IEnumerable<ChoixReponse> AllChoixReponse()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Courriel> AllCourriel()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Cours> AllCours()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<CoursInteret> AllCoursInteret()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<CoursSuivi> AllCoursSuivi()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Disponibilite> AllDisponibilite()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<EtuProgEtude> AllEtuProgEtude()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Evaluation> AllEvaluation()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Formulaire> AllFormulaire()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Groupe> AllGroupe()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<GroupeEtudiant> AllGroupeEtudiant()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Inscription> AllInscription()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Jumelage> AllJumelage()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Personne> AllPersonne()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<ProgrammeEtude> AllProgrammeEtude()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Question> AllQuestion()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<ReponseQuestion> AllReponseQuestion()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Section> AllSection()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Session> AllSession()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Suivi> AllSuivi()
        {
            throw new NotImplementedException();
        }
            //All - sur table parametres
        public IEnumerable<p_College> AllCollege()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<p_Contact> AllContact()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<p_HoraireInscription> AllHoraireInscription()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<p_Jour> AllJour()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<p_Saison> AllSaison()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<p_Sexe> AllSexe()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<p_StatutCours> AllStatutCours()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<p_StatutInscription> AllStatutInscription()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<p_TypeCourriel> AllTypeCourriel()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<p_TypeFormulaire> AllTypeFormulaire()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<p_TypeInscription> AllTypeInscription()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<p_TypeResultat> AllTypeResultat()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<p_TypeUsag> AllTypeUsag()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Find
        public ChoixReponse FindChoixReponse(int id)
        {
            throw new NotImplementedException();
        }
        public Courriel FindCourriel(int id)
        {
            throw new NotImplementedException();
        }
        public Cours FindCours(int id)
        {
            throw new NotImplementedException();
        }
        public CoursInteret FindCoursInteret(int id)
        {
            throw new NotImplementedException();
        }
        public CoursSuivi FindCoursSuivi(int id)
        {
            throw new NotImplementedException();
        }
        public Disponibilite FindDisponibilite(int id)
        {
            throw new NotImplementedException();
        }
        public EtuProgEtude FindEtuProgEtude(int id)
        {
            throw new NotImplementedException();
        }
        public Evaluation FindEvaluation(int id)
        {
            throw new NotImplementedException();
        }
        public Formulaire FindFormulaire(int id)
        {
            throw new NotImplementedException();
        }
        public Groupe FindGroupe(int id)
        {
            throw new NotImplementedException();
        }
        public GroupeEtudiant FindGroupeEtudiant(int id)
        {
            throw new NotImplementedException();
        }
        public Inscription FindInscription(int id)
        {
            throw new NotImplementedException();
        }
        public Jumelage FindJumelage(int id)
        {
            throw new NotImplementedException();
        }
        public Personne FindPersonne(int id)
        {
            throw new NotImplementedException();
        }
        public ProgrammeEtude FindProgrammeEtude(int id)
        {
            throw new NotImplementedException();
        }
        public Question FindQuestion(int id)
        {
            throw new NotImplementedException();
        }
        public ReponseQuestion FindReponseQuestion(int id)
        {
            throw new NotImplementedException();
        }
        public Section FindSection(int id)
        {
            throw new NotImplementedException();
        }
        public Session FindSession(int id)
        {
            throw new NotImplementedException();
        }
        public Suivi FindSuivi(int id)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Add
        public void AddChoixReponse(ChoixReponse itemToAdd, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void AddCourriel(Courriel itemToAdd, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void AddCours(Cours itemToAdd, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void AddCoursInteret(CoursInteret itemToAdd, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void AddCoursSuivi(CoursSuivi itemToAdd, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void AddDisponibilite(Disponibilite itemToAdd, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void AddEtuProgEtude(EtuProgEtude itemToAdd, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void AddEvaluation(Evaluation itemToAdd, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void AddFormulaire(Formulaire itemToAdd, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void AddGroupe(Groupe itemToAdd, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void AddGroupeEtudiant(GroupeEtudiant itemToAdd, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void AddInscription(Inscription itemToAdd, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void AddJumelage(Jumelage itemToAdd, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void AddPersonne(Personne itemToAdd, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void AddProgrammeEtude(ProgrammeEtude itemToAdd, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void AddQuestion(Question itemToAdd, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void AddReponseQuestion(ReponseQuestion itemToAdd, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void AddSection(Section itemToAdd, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void AddSession(Session itemToAdd, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void AddSuivi(Suivi itemToAdd, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region AddRange
        public void AddRangeChoixReponse(IEnumerable<ChoixReponse> itemsToAdd, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void AddRangeCourriel(IEnumerable<Courriel> itemsToAdd, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void AddRangeCours(IEnumerable<Cours> itemsToAdd, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void AddRangeCoursInteret(IEnumerable<CoursInteret> itemsToAdd, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void AddRangeCoursSuivi(IEnumerable<CoursSuivi> itemsToAdd, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void AddRangeDisponibilite(IEnumerable<Disponibilite> itemsToAdd, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void AddRangeEtuProgEtude(IEnumerable<EtuProgEtude> itemsToAdd, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void AddRangeEvaluation(IEnumerable<Evaluation> itemsToAdd, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void AddRangeFormulaire(IEnumerable<Formulaire> itemsToAdd, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void AddRangeGroupe(IEnumerable<Groupe> itemsToAdd, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void AddRangeGroupeEtudiant(IEnumerable<GroupeEtudiant> itemsToAdd, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void AddRangeInscription(IEnumerable<Inscription> itemsToAdd, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void AddRangeJumelage(IEnumerable<Jumelage> itemsToAdd, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void AddRangePersonne(IEnumerable<Personne> itemsToAdd, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void AddRangeProgrammeEtude(IEnumerable<ProgrammeEtude> itemsToAdd, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void AddRangeQuestion(IEnumerable<Question> itemsToAdd, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void AddRangeReponseQuestion(IEnumerable<ReponseQuestion> itemsToAdd, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void AddRangeSection(IEnumerable<Section> itemsToAdd, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void AddRangeSession(IEnumerable<Session> itemsToAdd, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void AddRangeSuivi(IEnumerable<Suivi> itemsToAdd, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Edit
        public void EditChoixReponse(ChoixReponse itemToEdit, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void EditCourriel(Courriel itemToEdit, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void EditCours(Cours itemToEdit, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void EditCoursInteret(CoursInteret itemToEdit, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void EditCoursSuivi(CoursSuivi itemToEdit, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void EditDisponibilite(Disponibilite itemToEdit, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void EditEtuProgEtude(EtuProgEtude itemToEdit, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void EditEvaluation(Evaluation itemToEdit, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void EditFormulaire(Formulaire itemToEdit, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void EditGroupe(Groupe itemToEdit, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void EditGroupeEtudiant(GroupeEtudiant itemToEdit, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void EditInscription(Inscription itemToEdit, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void EditJumelage(Jumelage itemToEdit, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void EditPersonne(Personne itemToEdit, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void EditProgrammeEtude(ProgrammeEtude itemToEdit, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void EditQuestion(Question itemToEdit, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void EditReponseQuestion(ReponseQuestion itemToEdit, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void EditSection(Section itemToEdit, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void EditSession(Session itemToEdit, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void EditSuivi(Suivi itemToEdit, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Remove
        public void RemoveChoixReponse(ChoixReponse itemToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveCourriel(Courriel itemToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveCours(Cours itemToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveCoursInteret(CoursInteret itemToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveCoursSuivi(CoursSuivi itemToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveDisponibilite(Disponibilite itemToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveEtuProgEtude(EtuProgEtude itemToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveEvaluation(Evaluation itemToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveFormulaire(Formulaire itemToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveGroupe(Groupe itemToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveGroupeEtudiant(GroupeEtudiant itemToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveInscription(Inscription itemToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveJumelage(Jumelage itemToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemovePersonne(Personne itemToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveProgrammeEtude(ProgrammeEtude itemToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveQuestion(Question itemToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveReponseQuestion(ReponseQuestion itemToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveSection(Section itemToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveSession(Session itemToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveSuivi(Suivi itemToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region RemoveRange
        public void RemoveRangeChoixReponse(IEnumerable<ChoixReponse> itemsToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveRangeCourriel(IEnumerable<Courriel> itemsToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveRangeCours(IEnumerable<Cours> itemsToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveRangeCoursInteret(IEnumerable<CoursInteret> itemsToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveRangeCoursSuivi(IEnumerable<CoursSuivi> itemsToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveRangeDisponibilite(IEnumerable<Disponibilite> itemsToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveRangeEtuProgEtude(IEnumerable<EtuProgEtude> itemsToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveRangeEvaluation(IEnumerable<Evaluation> itemsToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveRangeFormulaire(IEnumerable<Formulaire> itemsToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveRangeGroupe(IEnumerable<Groupe> itemsToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveRangeGroupeEtudiant(IEnumerable<GroupeEtudiant> itemsToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveRangeInscription(IEnumerable<Inscription> itemsToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveRangeJumelage(IEnumerable<Jumelage> itemsToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveRangePersonne(IEnumerable<Personne> itemsToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveRangeProgrammeEtude(IEnumerable<ProgrammeEtude> itemsToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveRangeQuestion(IEnumerable<Question> itemsToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveRangeReponseQuestion(IEnumerable<ReponseQuestion> itemsToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveRangeSection(IEnumerable<Section> itemsToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveRangeSession(IEnumerable<Session> itemsToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        public void RemoveRangeSuivi(IEnumerable<Suivi> itemsToRemove, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Liste
        public SelectList ListeTypeUsager(int idTypeUsager = 0)
        {
            throw new NotImplementedException();
        }
        public SelectList ListeSexe(int? sexe = 0)
        {
            throw new NotImplementedException();
        }
        public SelectList ListeSession(int session = 0)
        {
            throw new NotImplementedException();
        }
        public SelectList ListeCours(int cours = 0)
        {
            throw new NotImplementedException();
        }
        public SelectList ListeCollege(int college = 0)
        {
            throw new NotImplementedException();
        }
        public SelectList ListeStatutCours(int statut = 0)
        {
            throw new NotImplementedException();
        }
        public SelectList ListeProgrammmeCode(bool actif = true)
        {
            throw new NotImplementedException();
        }
        public SelectList ListeEtudiants(int id = 0)
        {
            throw new NotImplementedException();
        }
        public SelectList ListeEnseignant(int id = 0)
        {
            throw new NotImplementedException();
        }
        public SelectList ListeEnseignantEtResponsable(int id = 0)
        {
            throw new NotImplementedException();
        }
        public SelectList ListeTypeInscription(int typeInscription = 0)
        {
            throw new NotImplementedException();
        }
        public SelectList ListeInscription(int inscription = 0)
        {
            throw new NotImplementedException();
        }
        public SelectList ListeStatutInscriptionSansBrouillon(int statut = 0)
        {
            throw new NotImplementedException();
        }
        public SelectList ListeStatutCours()
        {
            throw new NotImplementedException();
        }
        public SelectList ListeTypesCourriels(int typeCourriel = 0)
        {
            throw new NotImplementedException();
        }
        public List<string> ListeJours()
        {
            throw new NotImplementedException();
        }
        #endregion

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}